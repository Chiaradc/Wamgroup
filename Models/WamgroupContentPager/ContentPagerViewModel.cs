﻿using Microsoft.Extensions.Primitives;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupContentPager
{
    /// <summary>
    /// The view model for the content pager.
    /// </summary>
    public class ContentPagerViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPagerViewModel"/> class.
        /// </summary>
        /// <param name="currentPage">The number of current page.</param>
        /// <param name="totalItemsCount">The count of total items.</param>
        /// <param name="itemsPerPage">The count of items per page.</param>
        /// <param name="pagerSegmentTemplate">The template for paging URL segment.</param>
        /// <param name="pagerQueryParamTemplate">The template for paging query parameter.</param>
        /// <param name="pagerMode">The mode indicating whether the pager works with URL segments ot with query parameters.</param>
        public ContentPagerViewModel(int currentPage, int totalItemsCount, int itemsPerPage, string pagerSegmentTemplate, string pagerQueryParamTemplate, PagerMode pagerMode)
        {
            ProcessedUrlSegments = new List<string>();
            CurrentPage = currentPage;

            var pagesCount = (double)totalItemsCount / itemsPerPage;
            var totalPagesCount = (int)Math.Ceiling(pagesCount);

            TotalPagesCount = totalPagesCount == 0 ? 1 : totalPagesCount;

            StartPageIndex = 1;
            EndPageIndex = 1;
            DisplayPagesCount = 2;

            if (CurrentPage > DisplayPagesCount)
            {
                StartPageIndex = (int)Math.Floor((double)(CurrentPage - 1) / DisplayPagesCount) * DisplayPagesCount + 1;
            }

            EndPageIndex = Math.Min(TotalPagesCount, StartPageIndex + DisplayPagesCount - 1);

            // previous button
            IsPreviousButtonVisible = StartPageIndex > DisplayPagesCount ? true : false;
            PreviousPageIndex = StartPageIndex - 1;

            // next button
            IsNextButtonVisible = EndPageIndex < TotalPagesCount ? true : false;
            NextPageIndex = EndPageIndex + 1;

            PagerMode = pagerMode;
            PagerQueryParameterTemplate = pagerQueryParamTemplate;
            PagerSegmentTemplate = pagerSegmentTemplate;
            if (!PagerSegmentTemplate.StartsWith("/", StringComparison.InvariantCulture))
                PagerSegmentTemplate = "/" + PagerSegmentTemplate;
        }

        /// <summary>
        /// Validate if the requested page number is valid.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>If the requested page number is valid.</returns>
        public bool IsPageValid(int pageNumber)
        {
            return pageNumber >= 1 && pageNumber <= EndPageIndex;
        }

        /// <summary>
        /// Gets or sets a value indicating the total pages count.
        /// </summary>
        public int TotalPagesCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the current page of the content items.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the visible pages count.
        /// </summary>
        public int DisplayPagesCount { get; set; }

        /// <summary>
        /// Gets or sets the first visible page.
        /// </summary>
        public int StartPageIndex { get; set; }

        /// <summary>
        /// Gets or sets the last visible page.
        /// </summary>
        public int EndPageIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the previous button is visible in the pager.
        /// </summary>
        public bool IsPreviousButtonVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the next button is visible in the pager.
        /// </summary>
        public bool IsNextButtonVisible { get; set; }

        /// <summary>
        /// Gets or sets the index of the page for the next button.
        /// </summary>
        public int NextPageIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the page for the previous button.
        /// </summary>
        public int PreviousPageIndex { get; set; }

        /// <summary>
        /// Gets or sets the template for paging URL segment.
        /// </summary>
        public string PagerSegmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for paging query parameter.
        /// </summary>
        public string PagerQueryParameterTemplate { get; set; }

        /// <summary>
        /// Gets or sets the pager mode - whether it is working with URL segments or with query parameters.
        /// </summary>
        public PagerMode PagerMode { get; set; }

        /// <summary>
        /// Gets or sets the processed url segments by the widget's pager.
        /// </summary>
        public IEnumerable<string> ProcessedUrlSegments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the page number is valid.
        /// </summary>
        public bool IsPageNumberValid { get; set; }

        /// <summary>
        /// Gets or sets the page view url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "ViewModel")]
        public string ViewUrl { get; set; }

        /// <summary>
        /// Gets the pages url for the pager.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// /// <param name="context">The http context.</param>
        /// <returns>Page's url with the new page number.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1055:URI-like return values should not be strings", Justification = "Usage in the view.")]
        public string GetPagerUrl(int pageNumber, HttpContext context)
        {
            string path = context?.Request.Path;

            // in case we are accessing it from home page
            if (path == "/" && !string.IsNullOrEmpty(ViewUrl))
            {
                if (ViewUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    var uri = new Uri(ViewUrl, UriKind.Absolute);
                    path = $"/{uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped)}";
                }
                else
                {
                    path = ViewUrl;
                }
            }

            string queryString = context?.Request.QueryString.ToString();

            if (PagerMode == PagerMode.URLSegments)
            {
                var desiredPage = PagerSegmentTemplate.Replace(PageNumberSlot, pageNumber.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCulture);

                if (IsSegmentMatch(path, out string pattern))
                {
                    return Regex.Replace(path, pattern, desiredPage) + queryString;
                }

                return path + desiredPage + queryString;
            }
            else
            {
                var template = PagerQueryParameterTemplate;
                var queryPattern = $"{template}=(\\d{{1,}})";
                var queryDesiredPage = $"{template}={pageNumber}";
                StringValues value;
                context.Request.Query.TryGetValue(template, out value);
                if (value.Count > 0 && int.TryParse(value, out int page))
                {
                    return path + Regex.Replace(queryString, queryPattern, queryDesiredPage);
                }

                if (!string.IsNullOrEmpty(queryString))
                    return path + queryString + "&" + queryDesiredPage;

                return path + "?" + queryDesiredPage;
            }
        }

        internal bool IsSegmentMatch(string url, out string pattern)
        {
            pattern = PagerSegmentTemplate.Replace(PageNumberSlot, "(\\d{1,})", StringComparison.InvariantCulture);
            var regex = new Regex(pattern, RegexOptions.Compiled);
            var pagingMatch = regex.Match(url);

            return pagingMatch.Success;
        }

        internal const string PageNumberSlot = "{{pageNumber}}";
        internal const string PageNumberDefaultTemplate = "-page-{{pageNumber}}-";
        internal const string PageNumberDefaultQueryTemplate = "page";
    }
}