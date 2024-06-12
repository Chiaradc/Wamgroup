using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
//using Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupSearchResults
{
   [SitefinityWidget(Title = "Wamgroup Search Results", Category = WidgetCategory.NavigationAndSearch, Section = "Wamgroup", EmptyIconText = "Modifica Search Results", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/row.jpg")]
    [ScriptFile("/js/Widgets/search.min.js", false, true, true)]
    [ScriptFile("/css/Widgets/search.min.css", true, true, true)]
    public class WamgroupSearchResultsViewComponent : ViewComponent
   {
      private ISearchResultsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="ISearchResultsModel"/> model.</param>
        /// 
     
        public WamgroupSearchResultsViewComponent(ISearchResultsModel model, CustomScriptService customScriptService)
      {
         this.model = model;
            customScriptService.RegisterType(this.GetType());
        }

      /// <summary>
      /// Invokes the search results widget creation.
      /// </summary>
      /// <param name="context">The context.</param>
      /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
      public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SearchResultsEntity> context)
      {
         if (context == null)
            throw new ArgumentNullException(nameof(context));

         var searchParamsModel = this.GetSearchParams();
         var viewModel = await this.model.InitializeViewModel(context.Entity, searchParamsModel, this.HttpContext);
         return this.View(context.Entity.SfViewName, viewModel);
      }

      /// <summary>
      /// Convert the value of showResultsForAllIndexedSites property form string to int.
      /// </summary>
      /// <param name="showResultsForAllIndexedSites">The showResultsForAllIndexedSites string value.</param>
      /// <returns>Returns the value of the property as int.</returns>
      private static int ConvertResultsSetting(string showResultsForAllIndexedSites)
      {
         switch (showResultsForAllIndexedSites)
         {
            case "":
               return 0;
            case "True":
               return 1;
            case "False":
               return 2;
            default:
               return 0;
         }
      }

      private SearchParamsModel GetSearchParams()
      {
         string indexCatalogue = this.HttpContext.Request.Query["indexCatalogue"];
         string searchQuery = this.HttpContext.Request.Query["searchQuery"];
         string wordsMode = this.HttpContext.Request.Query["wordsMode"];
         string orderBy = this.HttpContext.Request.Query["orderBy"];
         string culture = this.HttpContext.Request.Query["sf_culture"];
         string pageParam = this.HttpContext.Request.Query["page"];
         string scoringProfile = this.HttpContext.Request.Query["scoringInfo"];
         string showResultsForAllIndexedSites = this.HttpContext.Request.Query["resultsForAllSites"];
         string filter = this.HttpContext.Request.Query["filter"];
         int page = 1;
         var resultsForAllSites = ConvertResultsSetting(showResultsForAllIndexedSites);
         if (!string.IsNullOrEmpty(pageParam) && int.TryParse(pageParam, out int pageNumber) && pageNumber > 0)
         {
            page = pageNumber;
         }

         var searchParams = new SearchParamsModel()
         {
            IndexCatalogue = indexCatalogue,
            SearchQuery = searchQuery,
            WordsMode = wordsMode,
            OrderBy = orderBy,
            Culture = culture,
            Page = page,
            ScroingInfo = scoringProfile,
            ShowResultsForAllIndexedSites = resultsForAllSites,
            Filter = filter,
         };

         return searchParams;
      }
   }
}
