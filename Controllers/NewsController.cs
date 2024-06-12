using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupNews;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.RestSdk;
using Microsoft.Net.Http.Headers;

namespace Elogic.Wamgroup.Sito2023.NetCore.Controllers
{
   public class NewsController : Controller
   {
      private readonly RestClientService restClientService;
      private readonly NewsService newsService;

      public NewsController(RestClientService restClientService, NewsService newsService)
      {
         this.restClientService = restClientService;
         this.newsService = newsService;
      }

      public IActionResult Index()
      {
         return View();
      }

      public async Task<IActionResult> ReadMoreAsync()
      {
         var args = new RequestArgs();
         var requestCookie = HttpContext.Request.Headers[HeaderNames.Cookie];
         if (!string.IsNullOrEmpty(requestCookie))
            args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);

         await restClientService.Init(args);

         int.TryParse(Request.Query["skip"], out int skip);
         int.TryParse(Request.Query["take"], out int take);
         var urldettaglio = Request.Query["urldettaglio"].ToString();
         List<WamgroupNewsItemViewModel> newsModel = await newsService.GetNewsModelAsync(skip, take, urldettaglio);

         return View(newsModel);
      }

      private async Task<IEnumerable<WamgroupCustomNewsItem>> GetNews(
      int topN = 0)
      {

         var news = await restClientService.GetItems<WamgroupCustomNewsItem>(new List<string> { "Id", "Title", "Intro", "Content", "ThumbnailImage", "DateCreated", "ItemDefaultUrl", "UrlName", "Type", "Date", "DetailImage", "Highlighted", "Category" }, topN);
         return news.ToList();
      }
   }
}
