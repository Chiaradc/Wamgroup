using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupDownload;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Elogic.Wamgroup.Sito2023.NetCore.Services
{
   public class DocumentsService
   {
      private readonly IRestClient restClient;
      
      public DocumentsService(IRestClient restClient)
      {
         this.restClient = restClient;
         
         
      }

      public async Task<List<DownloadableDocument>> GetDownlodableDocuments(int currentPage, int pageSize, int anno, string categoria, string libraryId)
      {
         

         var getAllArgs = new GetAllArgs
         {
            //Culture = CultureInfo.CurrentCulture.Name,
            Fields = new List<string> { "Title", "PublicationDate", "Url", "Category" }
         };
         //if (currentPage > 0)
         //{
         //   getAllArgs.Skip = currentPage * pageSize;
         //   getAllArgs.Take = pageSize;
         //}
         getAllArgs.Skip = currentPage * pageSize;
         getAllArgs.Take = pageSize;

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

      public async Task<List<string>> GetAnniPerDocumenti(string libraryId)
      {
         

         var data = await restClient.GetItems<DocumentDto>(new GetAllArgs
         {
            //Culture = CultureInfo.CurrentCulture.Name,
            Filter = new FilterClause
            {
               FieldName = nameof(DocumentDto.FolderId),
               Operator = FilterClause.Operators.Equal,

               FieldValue = libraryId

            }
         });
         var docs = data.Items;
         return docs.Select(d => d.PublicationDate.Year.ToString()).Distinct().ToList();
      }

      public async Task<Dictionary<string, string>> GetCategoriePerDocumenti(string libraryId)
      {
         

         var data = await restClient.GetItems<DocumentDto>(new GetAllArgs
         {
            //Culture = CultureInfo.CurrentCulture.Name,
            Filter = new FilterClause
            {
               FieldName = nameof(DocumentDto.FolderId),
               Operator = FilterClause.Operators.Equal,

               FieldValue = libraryId

            },
            Fields = new List<string> { "Category" }
         });
         var docs = data.Items;

         var dataCats = await restClient.GetItems<CategoryDto>(new GetAllArgs
         {
            Culture = CultureInfo.CurrentCulture.Name,
            Fields = new List<string> { "Id", "Title" }
         });
         var allCats = dataCats.Items;

         Dictionary<string, string> cats = new Dictionary<string, string>();

         docs?.ForEach(d =>
         {
            d?.Category.ForEach(c =>
            {
               if (!cats.Keys.Any(x => x == c) && allCats.Any(x => x.Id == c))
               {
                  cats.Add(c, allCats.FirstOrDefault(x => x.Id == c)?.Title ?? string.Empty);

               }

            });
         });

         return cats;
      }
   }
}
