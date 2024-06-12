using System.IO.Compression;
using System.Net;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupCatalog;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Filters;

namespace Elogic.Wamgroup.Sito2023.NetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly RestClientService restClientService;
        private DocumentsService? documentsService;

        public ProductController(RestClientService restClientService)
        {
            this.restClientService = restClientService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductCatalogAsync()
        {
            var args = new RequestArgs();
            var requestCookie = HttpContext.Request.Headers[HeaderNames.Cookie];
            if (!string.IsNullOrEmpty(requestCookie))
                args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);

            await restClientService.Init(args);

            var solutionByIndustry = Request.Query["solutionbyindustry"].ToString()
               .Split("|")
               .SkipWhile(x => string.IsNullOrEmpty(x)).ToArray() ?? new string[] { };
            var solutionByTecnology = Request.Query["solutionbytecnology"].ToString()
               .Split("|")
               .SkipWhile(x => string.IsNullOrEmpty(x)).ToArray() ?? new string[] { };
            var brand = Request.Query["brand"].ToString()
               .Split("|")
               .SkipWhile(x => string.IsNullOrEmpty(x)).ToArray() ?? new string[] { };
            var family = Request.Query["family"].ToString()
               .Split("|")
               .SkipWhile(x => string.IsNullOrEmpty(x)).ToArray() ?? new string[] { };
            int.TryParse(Request.Query["page"], out int currentpage);
            int.TryParse(Request.Query["pagesize"], out int pagesize);
            var detailUrl = Request.Query["detailurl"].ToString();

            var model = new WamgroupCatalogViewModel();

            model.ProductURL = detailUrl;

            var argsProduct = new GetAllArgs
            {
                Count = true,
                Skip = (currentpage - 1) * pagesize,
                Take = pagesize,
                Fields = new List<string> { "*" }
            };

            IList<object> filters = new List<object>();

            if (solutionByIndustry.Length > 0)
            {
                filters.Add(new FilterClause
                {
                    FieldName = nameof(WamgroupProduct.solutionbyindustry),
                    Operator = FilterClause.Operators.ContainsOr,
                    FieldValue = solutionByIndustry

                });

            }
            if (solutionByTecnology.Length > 0)
            {
                filters.Add(new FilterClause
                {
                    FieldName = nameof(WamgroupProduct.solutionbyneed),
                    Operator = FilterClause.Operators.ContainsOr,
                    FieldValue = solutionByTecnology
                });
            }
            if (brand.Length > 0)
            {
                filters.Add(new FilterClause
                {
                    FieldName = nameof(WamgroupProduct.brands),
                    Operator = FilterClause.Operators.ContainsOr,
                    FieldValue = brand
                });
            }
            if (family.Length > 0)
            {
                filters.Add(new FilterClause
                {
                    FieldName = nameof(WamgroupProduct.families),
                    Operator = FilterClause.Operators.ContainsOr,
                    FieldValue = family
                });
            }

            var filter = new CombinedFilter
            {

                ChildFilters = filters,
                Operator = CombinedFilter.LogicalOperators.And
            };
            argsProduct.Filter = filter;

            var productsResponse = await restClientService.GetResponseItems<WamgroupProduct>(argsProduct);
            foreach (var product in productsResponse.Items)
            {
                var p = new WamgroupCatalogProductItem
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Url = string.IsNullOrEmpty(detailUrl) ? "#" : Path.Combine(detailUrl ?? "", product.UrlName ?? "")
                };

                var productImages = await restClientService.GetItems<WamgroupVisualSlide>(new List<string> { "Id", "Image" }, 0, null, product.Id);
                p.ImageUrl = productImages.FirstOrDefault()?.Image?.FirstOrDefault()?.Url;

                model.Products.Add(p);
            }

            model.TotalProducts = productsResponse.TotalCount ?? 0;
            model.CurrentPage = currentpage;
            model.PageSize = pagesize;
            model.PageCount = (int)Math.Ceiling((double)model.TotalProducts / pagesize);

            return View("ProductCatalog", model);

        }

        public async Task<FileResult> DownloadDocumentAsync()
        {
            var documentArray = Request.Query["documents"].ToString()
               .Split("|")
               .SkipWhile(x => string.IsNullOrEmpty(x)).ToArray() ?? new string[] { };

            if (documentArray.Length == 0)
            {
                return null;
            }

            var client = new HttpClient();
            var zipToOpen = new MemoryStream();
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create, true))
            {
                foreach (var documentId in documentArray)
                {
                    var document = await restClientService.GetItem<DocumentDto>(documentId, new List<string> { "*" });
                    if (document != null)
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntry(document.UrlName + document.Extension);
                        using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                        {
                            var response = await client.GetAsync($"{Request.Scheme}://{Request.Host.Value}/{document.Url ?? ""}").ConfigureAwait(false);
                            byte[] zipDoc = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                            writer.BaseStream.Write(zipDoc, 0, zipDoc.Length);
                        }
                    }
                }
                
            }

            zipToOpen.Position = 0;

            byte[] zipData = new byte[zipToOpen.Length];
            int brZip = zipToOpen.Read(zipData, 0, zipData.Length);

            var file = File(zipData, System.Net.Mime.MediaTypeNames.Application.Octet, "Documents.zip");
            return file;

        }
    }
}
