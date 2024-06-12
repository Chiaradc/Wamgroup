using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupDownload;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk;
using Microsoft.Net.Http.Headers;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Microsoft.AspNetCore.Http;

namespace Elogic.Wamgroup.Sito2023.NetCore.Controllers
{
   public class DownloadController : Controller
   {
      private readonly IRestClient restClient;
      private DocumentsService? documentsService;

      public DownloadController(IRestClient restClient)
      {
         this.restClient = restClient;
         
      }

      public string Index()
      {
         return "Ciao";
      }

      public async Task<IActionResult> ReadMoreAsync()
      {
         var args = new RequestArgs();
         var requestCookie = HttpContext.Request.Headers[HeaderNames.Cookie];
         if (!string.IsNullOrEmpty(requestCookie))
            args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);

         await this.restClient.Init(args);

         documentsService = new DocumentsService(this.restClient);

         int.TryParse(Request.Query["page"], out int currentPage);
         int.TryParse(Request.Query["pageSize"], out int pageSize);
         int.TryParse(Request.Query["year"], out int year);
         string category = Request.Query["category"].ToString() ?? "";
         string folder = Request.Query["folder"].ToString() ?? "";
         

         var model = await documentsService.GetDownlodableDocuments(currentPage, pageSize, year, category, folder);
         
         return View(model);
         
      }

      private async Task<List<DownloadableDocument>> GetDownlodableDocuments(int currentPage, int pageSize, int anno, string categoria, string libraryId)
      {
         var getAllArgs = new GetAllArgs
         {
            //Culture = CultureInfo.CurrentCulture.Name,
            Fields = new List<string> { "Title", "PublicationDate", "Url", "Category" }
         };
         if (currentPage > 0)
         {
            getAllArgs.Skip = currentPage * pageSize;
            getAllArgs.Take = pageSize;
         }

         IList<object> filters = new List<object>
         {
            new FilterClause
            {
               FieldName = nameof(DocumentDto.FolderId),
               Operator = FilterClause.Operators.Equal,

               FieldValue = libraryId
            }
         };

         if (anno > 0)
         {
            filters.Add(new FilterClause
            {
               FieldName = nameof(DocumentDto.DateCreated),
               Operator = FilterClause.Operators.GreaterThanOrEqual,

               FieldValue = new DateTime(anno, 1, 1)
            });

            filters.Add(new FilterClause
            {
               FieldName = nameof(DocumentDto.DateCreated),
               Operator = FilterClause.Operators.LessThanOrEqual,

               FieldValue = new DateTime(anno, 12, 31)
            });
         }

         if (categoria != string.Empty)
         {
            filters.Add(new FilterClause
            {
               FieldName = nameof(DocumentDto.Category),
               Operator = FilterClause.Operators.ContainsOr,

               FieldValue = categoria
            });
         }

         try
         {
            getAllArgs.Filter = new CombinedFilter
            {
               Operator = CombinedFilter.LogicalOperators.And,
               ChildFilters = filters
            };
         }
         catch (Exception ex)
         {

         }

         var data = await restClient.GetItems<DocumentDto>(getAllArgs);
         var docs = data.Items;
         return docs.Select(d => new DownloadableDocument
         {
            Date = d.PublicationDate,
            Description = d.Title,
            Files = new Dictionary<string, DownloadableFile> { { "", new DownloadableFile { DocumentCulture = CultureInfo.CurrentCulture.Name, DocumentDescription = d.Title, FileUrl = d.Url, IsDefault = true } } }
         }).ToList();
      }
   }
}
