
namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults
{
   /// <summary>
   /// Defines model for the Search results widget.
   /// </summary>
   public interface ISearchResultsModel
   {
      /// <summary>
      /// Initializes the view model.
      /// </summary>
      /// <param name="entity">The search results entity.</param>
      /// <param name="searchParamsModel">The search params modeul.</param>
      /// <param name="httpContext">The current http context.</param>
      /// <returns>The view model of the widget.</returns>
      Task<SearchResultsViewModel> InitializeViewModel(SearchResultsEntity entity, SearchParamsModel searchParamsModel, HttpContext httpContext);
   }
}
