using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.Web;

using Progress.Sitefinity.RestSdk.OData;
using Progress.Sitefinity.RestSdk;
using System.Globalization;
using System.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupContentPager;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults
{
    public class SearchResultsModel : ISearchResultsModel
   {
      private readonly IODataRestClient restClient;
      private readonly IRequestContext requestContext;
      private readonly IStyleClassesProvider styles;
      private IStringLocalizer<SearchResultsModel> localizer;
      private readonly DynamicTypeHelper DynamicTypeHelper;

      /// <summary>
      /// Initializes a new instance of the <see cref="SearchResultsModel"/> class.
      /// </summary>
      /// <param name="requestContext">The request context.</param>
      /// <param name="restClient">The rest client.</param>
      /// <param name="localizer">The localization provider.</param>
      /// <param name="styles">The styles provider.</param>
      public SearchResultsModel(
          IRequestContext requestContext,
          IODataRestClient restClient,
          IStringLocalizer<SearchResultsModel> localizer,
          IStyleClassesProvider styles,
          DynamicTypeHelper DynamicTypeHelper)
      {
         this.requestContext = requestContext;
         this.restClient = restClient;
         this.localizer = localizer;
         this.styles = styles;
         this.DynamicTypeHelper = DynamicTypeHelper;
      }

      /// <inheritdoc/>
      public async Task<SearchResultsViewModel> InitializeViewModel(SearchResultsEntity entity, SearchParamsModel searchParamsModel, HttpContext httpContext)
      {
         if (entity == null)
            throw new ArgumentNullException(nameof(entity));
         if (searchParamsModel == null)
            throw new ArgumentNullException(nameof(searchParamsModel));
         if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

         httpContext.AddVaryByQueryParams(new[] { "searchQuery", "page", "sf_culture", "orderBy", "filter" });

         var currentSite = this.requestContext.Site;
         //var languages = currentSite.Cultures.Select(x => new Sitefinity.RestSdk.Clients.Sites.Dto.SiteDto.CultureDto()
         //{
         //   Name = x.Name,
         //   DisplayName = CultureInfo.GetCultureInfo(x.Name).DisplayName,
         //});
         var languages = currentSite.Cultures;
         foreach (var culture in languages)
         {
            culture.DisplayName = CultureInfo.GetCultureInfo(culture.Name).DisplayName;
         }

         var margins = this.styles.GetMarginsClasses(entity);
         var cssClass = (entity.CssClass + " " + margins).Trim();

         var searchResultsViewModel = new SearchResultsViewModel()
         {
            SearchResults = new List<SearchResultDocumentDto>(),
            ResultsHeader = string.Format(CultureInfo.InvariantCulture, entity.NoResultsHeader, searchParamsModel.SearchQuery),
            LanguagesLabel = entity.LanguagesLabel,
            ResultsNumberLabel = entity.ResultsNumberLabel,
            Attributes = entity.Attributes,
            CssClass = cssClass,
            Languages = languages,
            AllowUsersToSortResults = entity.AllowUsersToSortResults == 1,
            Sorting = entity.Sorting,
            SoryByLabel = this.localizer.GetString("Sort by"),
         };

         if (string.IsNullOrEmpty(searchParamsModel.SearchQuery))
         {
            return searchResultsViewModel;
         }
         else
         {
            JObject response = await this.PerformSearch(entity, searchParamsModel);
            //int totalCount = response[nameof(SearchResultsViewModel.TotalCount)].ToObject<int>();
            IList<SearchResultDocumentDto> searchResults = response[nameof(SearchResultsViewModel.SearchResults)].ToObject<List<SearchResultDocumentDto>>();
                //searchResultsViewModel.TotalCount = totalCount;
                searchResultsViewModel.SearchResults = searchResults.Where(x => !string.IsNullOrEmpty(x.Link)).ToList();

            if (httpContext.Request.Query.ContainsKey("selectedType") && httpContext.Request.Query["selectedType"] != "" && httpContext.Request.Query["selectedType"] != "All")
            {
               searchResultsViewModel.SearchResults = searchResultsViewModel.SearchResults.Where(x => x.ContentType == httpContext.Request.Query["selectedType"]).ToList();
            }

            searchResultsViewModel.TotalCount = searchResultsViewModel.SearchResults.Count;

            if (entity.ListSettings.DisplayMode == ListDisplayMode.Paging)
            {
               int pagesCount = (int)Math.Ceiling((double)searchResultsViewModel.TotalCount / (double)entity.ListSettings.ItemsPerPage);
               searchResultsViewModel.PagesCount = pagesCount;
               searchResultsViewModel.CurrentPage = searchParamsModel.Page;
               searchResultsViewModel.Pager = new ContentPagerViewModel(searchParamsModel.Page, searchResultsViewModel.TotalCount, entity.ListSettings.ItemsPerPage, ContentPagerViewModel.PageNumberDefaultTemplate, ContentPagerViewModel.PageNumberDefaultQueryTemplate, PagerMode.QueryParameter);
            }

            if (searchResultsViewModel.SearchResults.Count > 0)
            {
               searchResultsViewModel.ResultsHeader = string.Format(CultureInfo.InvariantCulture, entity.SearchResultsHeader, searchParamsModel.SearchQuery);
               searchResultsViewModel.DistinctTypes = searchResults?.Select(x => x.ContentType).Distinct().Select(x => DynamicTypeHelper.GetDynamicType(x)).ToList() ?? new List<DynamicTypeHelper.DynamicTypeMap>();
            }

            //manage pagination
            int skip = 0;
            int take = 20;
            if (entity.ListSettings.DisplayMode == ListDisplayMode.Paging)
            {
               take = entity.ListSettings.ItemsPerPage;
               skip = (searchParamsModel.Page - 1) * take;
            }
            else if (entity.ListSettings.DisplayMode == ListDisplayMode.Limit)
            {
               take = entity.ListSettings.LimitItemsCount;
            }
            else if (entity.ListSettings.DisplayMode == ListDisplayMode.All)
            {
               take = int.MaxValue;
            }
            searchResultsViewModel.SearchResults = searchResultsViewModel.SearchResults.Skip(skip).Take(take).ToList();
         }

         return searchResultsViewModel;
      }

      private async Task<JObject> PerformSearch(SearchResultsEntity entity, SearchParamsModel searchParamsModel)
      {
         string orderByClause = string.IsNullOrEmpty(searchParamsModel.OrderBy) ? entity.Sorting.ToString() : searchParamsModel.OrderBy;

         if (orderByClause == SearchResultsSorting.NewestFirst.ToString())
         {
            orderByClause = "PublicationDate desc";
         }
         else if (orderByClause == SearchResultsSorting.OldestFirst.ToString())
         {
            orderByClause = "PublicationDate";
         }
         else
         {
            orderByClause = string.Empty;
         }

         //int skip = 0;
         //int take = 20;
         //if (entity.ListSettings.DisplayMode == ListDisplayMode.Paging)
         //{
         //   take = entity.ListSettings.ItemsPerPage;
         //   skip = (searchParamsModel.Page - 1) * take;
         //}
         //else if (entity.ListSettings.DisplayMode == ListDisplayMode.Limit)
         //{
         //   take = entity.ListSettings.LimitItemsCount;
         //}
         //else if (entity.ListSettings.DisplayMode == ListDisplayMode.All)
         //{
         //   take = int.MaxValue;
         //}
         int skip = 0;
         int take = int.MaxValue;

         var response = await this.restClient.ExecuteUnboundFunction<JObject>(new BoundFunctionArgs()
         {
            Name = "Default.PerformSearch",
            AdditionalQueryParams = new Dictionary<string, string>()
            {
               ["indexCatalogue"] = searchParamsModel.IndexCatalogue,
               ["searchQuery"] = HttpUtility.UrlEncode(searchParamsModel.SearchQuery),
               ["wordsMode"] = searchParamsModel.WordsMode,
               ["$orderBy"] = orderByClause,
               ["sf_culture"] = searchParamsModel.Culture,
               ["$skip"] = skip.ToString(CultureInfo.InvariantCulture),
               ["$top"] = take.ToString(CultureInfo.InvariantCulture),
               ["searchFields"] = entity.SearchFields,
               ["highlightedFields"] = entity.HighlightedFields,
               ["scoringInfo"] = searchParamsModel.ScroingInfo,
               ["resultsForAllSites"] = searchParamsModel.ShowResultsForAllIndexedSites.ToString("F0", CultureInfo.InvariantCulture),
               ["filter"] = searchParamsModel.Filter,
            },
         });

         return response;
      }

     
   }
}
