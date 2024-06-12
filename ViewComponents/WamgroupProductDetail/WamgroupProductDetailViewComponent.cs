using Elogic.Sitefinity.Infrastructure.Images;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNews;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupProductDetail;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupProductDetail;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Globalization;
using System.Reflection;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupProductDetail
{
    [SitefinityWidget(Title = "Wamgroup Product Detail", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Product Detail", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/images.jpg")]
    [ScriptFile("/lib/swiper/swiper-bundle.min.js", false, true, true)]
    [ScriptFile("/js/widgets/generalslider.min.js", false, true, true)]
    [ScriptFile("/css/widgets/generalslider.min.css", true, true, true)]
    [ScriptFile("/css/widgets/productListDetail.min.css", true, true, true)]
    [ScriptFile("/js/widgets/productdetail.min.js", false, true, true)]
    public class WamgroupProductDetailViewComponent : ViewComponent
    {

        private readonly RestClientService restClientService;
        private readonly IImagesService imagesService;

        public WamgroupProductDetailViewComponent(RestClientService restClientService, IImagesService imagesService, CustomScriptService customScriptService)
        {
            this.restClientService = restClientService;
            this.imagesService = imagesService;
            customScriptService.RegisterType(this.GetType());
        }

        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupProductDetailEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));

            }
            var model = new WamgroupProductDetailViewModel();

            var action = context.State.TryGetValue(RequestPreparation.SelectedAction, out object objAction) ? (objAction as string) ?? string.Empty : string.Empty;
            if (action == "detail")
            {
                if (!context.State.TryGetValue(RequestPreparation.SelectedItem, out object objItem))
                {
                    throw new Exception("Null item");
                }
                else
                {
                    var item = (objItem as string) ?? "";

                    var product = await GetSingleproduct(item);


                    model = new WamgroupProductDetailViewModel
                    {
                        Title = product?.Title,
                        Subtitle = product?.Subtitle,
                        Presentation = product?.Presentation,
                        Description = product?.Description,
                        Function = product?.Function,
                        Features = product?.Features,
                        Benefits = product?.Benefits,
                        Options = product?.Options,

                    };

                    var images = await GetProductImages(product?.Id ?? "");
                    var visualSlides = new List<WamgroupProductDetailVisualiSlide>();
                    foreach (var image in images)
                    {
                        visualSlides.Add(new WamgroupProductDetailVisualiSlide
                        {

                            Title = image?.Title ?? "",
                            URL = image?.Image?.Length > 0 ? image?.Image?[0]?.Url ?? "" : "",
                        });
                    }

                    model.VisualiSlides = visualSlides;

                    var multimedia = await GetProductMultimedia(product?.Id ?? "");
                    var multimediaItems = new List<WamgroupProductDetailMultimedia>();
                    foreach (var itemMultimedia in multimedia.Where(x => !(x.IsMainImage ?? false)))
                    {
                        multimediaItems.Add(new WamgroupProductDetailMultimedia
                        {
                            Title = itemMultimedia?.Title ?? "",
                            ImageUrl = itemMultimedia?.Image?.Length > 0 ? itemMultimedia?.Image?[0]?.Url ?? "" : "",
                            VideoTeaserUrl = itemMultimedia?.VideoTeaser?.Length > 0 ? itemMultimedia?.VideoTeaser?[0]?.Url ?? "" : "",
                            VideoEmbedCode = itemMultimedia?.Video ?? ""
                        });
                    }

                    model.Multimedia = multimediaItems;

                    var mainImage = multimedia.FirstOrDefault(x => x.IsMainImage == true);
                    if (mainImage != null)
                    {
                        model.MainImage = new WamgroupProductDetailMultimedia
                        {
                            Title = mainImage.Title ?? "",
                            ImageUrl = mainImage?.Image?.Length > 0 ? mainImage.Image?[0]?.Url ?? "" : "",
                            VideoTeaserUrl = mainImage?.VideoTeaser?.Length > 0 ? mainImage.VideoTeaser?[0]?.Url ?? "" : "",
                            VideoEmbedCode = mainImage?.Video ?? ""
                        };
                    }

                    var spotboxes = await GetProductSpotBoxes(product?.Id ?? "");
                    var spotboxesItems = new List<WamgroupProductDetailSpotBox>();
                    foreach (var spotbox in spotboxes)
                    {
                        spotboxesItems.Add(new WamgroupProductDetailSpotBox
                        {
                            Title = spotbox?.Title ?? "",
                            ImageUrl = spotbox?.Image?.Length > 0 ? spotbox?.Image?[0]?.Url ?? "" : "",
                        });
                    }

                    model.SpotBoxes = spotboxesItems;

                    var solutionbyindustry = new List<string>();
                    foreach (var sbi in product?.solutionbyindustry ?? new string[] { })
                    {
                        var solution = await restClientService.GetItem<ClassificationSolutionByIndustry>(sbi);
                        if (solution != null)
                        {
                            solutionbyindustry.Add(solution?.Title ?? "");
                        }
                    }
                    model.solutionbyindustry = solutionbyindustry.ToArray();

                    var solutionbyneed = new List<string>();
                    foreach (var sbn in product?.solutionbyneed ?? new string[] { })
                    {
                        var solution = await restClientService.GetItem<ClassificationSolutionByNeed>(sbn);
                        if (solution != null)
                        {
                            solutionbyneed.Add(solution?.Title ?? "");
                        }
                    }
                    model.solutionbyneed = solutionbyneed.ToArray();

                    model.Documents = new List<WamgroupProductDetailDocument>();
                    var documents = product?.Documents ?? new List<WamgroupDocumentItem>();
                    var allDocumentLanguages = new List<string>();

                    
                    foreach (var document in documents)
                    {
                        var doc = await restClientService.GetItem<WamgroupDocumentItem>(document.Id, new List<string> { "*" });
                        var arrayLanguages = (doc?.Languages ?? "").Split(',').Select(x => x.Trim()).ToList();
                        var newLanguages = arrayLanguages.Except(allDocumentLanguages).ToList();
                        allDocumentLanguages.AddRange(newLanguages);

                        model.Documents.Add(new WamgroupProductDetailDocument
                        {
                            Title = doc?.Title ?? "",
                            Type = doc?.Type ?? "",
                            RefDate = doc?.RefDate,
                            Code = doc?.Code ?? "",
                            Languages = doc?.Languages ?? "",
                            ListLanguages = arrayLanguages.Select(x => new WamgroupProductDetailLanguage { Code = LangCode(x), DisplayName = x }).ToList(),
                            Issue = doc?.Issue ?? "",
                            URL = doc?.Document?.Length > 0 ? doc?.Document?[0].Url ?? "" : "",
                            Id = doc?.Document?.Length > 0 ? doc?.Document?[0].Id ?? "" : "",
                        });
                    }
                    model.DocumentsLanguages = allDocumentLanguages.Select(x => new WamgroupProductDetailLanguage { Code = LangCode(x), DisplayName = x }).ToList();

                    model.LinkedProducts = new List<WamgroupProductDetailLinkedProduct>();
                    var linkedProducts = product?.LinkedProducts ?? new List<WamgroupProduct>();
                    foreach (var linkedProduct in linkedProducts)
                    {
                        var linkedMultimedia = await GetProductMultimedia(linkedProduct?.Id ?? "");

                        var linkedMainImage = linkedMultimedia.FirstOrDefault(x => x.IsMainImage == true);

                        model.LinkedProducts.Add(new WamgroupProductDetailLinkedProduct
                        {
                            Title = linkedProduct?.Title ?? "",
                            ImageUrl = linkedMainImage?.Image?.Length > 0 ? linkedMainImage.Image?[0]?.Url ?? "" : "",
                        });
                    }
                }
            }

            /* ViewComponentContext<WamgroupNewsEntity> viewModel = new ViewComponentContext<WamgroupNewsEntity>();
             viewModel.Entity = new WamgroupNewsEntity();
             ViewBag.NewsViewModel = viewModel;*/

            return View("Default", model);
        }

        private async Task<WamgroupProduct> GetSingleproduct(string url)
        {
            var parUrl = url.StartsWith("/") ? url : $"/{url}";
            var products = await restClientService.GetItems<WamgroupProduct>(new List<string> { "*" }, 0, parUrl);
            return products.FirstOrDefault() ?? new WamgroupProduct();
        }

        private async Task<IList<WamgroupVisualSlide>> GetProductImages(string id)
        {
            var productImages = await restClientService.GetItems<WamgroupVisualSlide>(new List<string> { "*" }, 0, null, id);

            return productImages;
        }

        private async Task<IList<WamgroupProductMultimedia>> GetProductMultimedia(string id)
        {
            var productMultimedia = await restClientService.GetItems<WamgroupProductMultimedia>(new List<string> { "*" }, 0, null, id);

            return productMultimedia;
        }

        private async Task<IList<WamgroupSpotBox>> GetProductSpotBoxes(string id)
        {
            var productSpotbox = await restClientService.GetItems<WamgroupSpotBox>(new List<string> { "*" }, 0, null, id);

            return productSpotbox;
        }

        private async Task<IList<WamgroupProduct>> GetProductLinkedProducts(string id)
        {
            var productSpotbox = await restClientService.GetItems<WamgroupProduct>(new List<string> { "*" }, 0, null, id);

            return productSpotbox;
        }

        private string LangCode(string displayName) //da sistemare
        {
            // Get all available cultures on the current system.
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var code = cultures.FirstOrDefault(x => x.DisplayName == displayName)?.TwoLetterISOLanguageName;

            if (code != null)
                return code;

            return displayName;
            
        }
    }
}