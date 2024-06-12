using Elogic.Wamgroup.Sito2023.Models.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupNews;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using System.Globalization;

namespace Elogic.Wamgroup.Sito2023.NetCore.Services
{
   public class NewsService
   {
      private readonly RestClientService restClientService;

      public NewsService(RestClientService restClientService)
      {
         this.restClientService = restClientService;

      }

      public async Task<List<WamgroupNewsItemViewModel>> GetNewsModelAsync(int Skip, int Take, string UrlDettaglioNews)
      {
         List<WamgroupNewsItemViewModel> newsModel = new List<WamgroupNewsItemViewModel>();

         var news = await GetNews(Skip, Take);
         var goOn = true;
         var index = 0;
         while (goOn)
         {
            var tripletta = news.Skip(index * 3).Take(3).ToList();
            if (!tripletta.Any())
            {
               goOn = false;
            }
            index++;
            var innerNews = new List<WamgroupCustomNewsItem>();
            WamgroupCustomNewsItem higlighted = new WamgroupCustomNewsItem();
            if (tripletta.Count(x => x.Highlighted.HasValue && x.Highlighted.Value) >= 1)
            {
               higlighted = tripletta.FirstOrDefault(x => x.Highlighted.HasValue && x.Highlighted.Value);
            }
            innerNews.AddRange(tripletta.Where(x => x.Id != higlighted.Id));
            if (higlighted.Id != null)
            {
               innerNews.Add(higlighted);
            }

            foreach (var n in innerNews)
            {
               var imgLista = "";
               var widthLista = 0;
               var heightLista = 0;

               var imgDettaglio = "";
               var widthDettaglio = 0;
               var heightDettaglio = 0;
               if (n.ThumbnailImage?.Length > 0)
               {
                  var mediaFile = n.ThumbnailImage[0];
                  imgLista = mediaFile.Url;
                  widthLista = mediaFile.Width;
                  heightLista = mediaFile.Height;


               }
               if (n.DetailImage?.Length > 0)
               {
                  var mediaFile = n.DetailImage[0];
                  imgDettaglio = mediaFile.Url;
                  widthDettaglio = mediaFile.Width;
                  heightDettaglio = mediaFile.Height;


               }



               var newModel = new WamgroupNewsItemViewModel
               {
                  ImmagineLista = imgLista,
                  //AltImmagine = alt,
                  DataStringa = n.DateCreated?.ToString(CultureInfo.CurrentCulture.Name == "it-IT" ? Constants.ItalianDateFormat : Constants.EnglishDateFormat) ?? "",
                  Titolo = n.Title ?? string.Empty,
                  Intro = n.Intro ?? string.Empty,
                  WidthLista = widthLista,
                  HeightLista = heightLista,
                  ImmagineDettaglio = imgDettaglio,
                  WidthDettaglio = widthDettaglio,
                  HeightDettaglio = heightDettaglio,
                  Url = Path.Combine(UrlDettaglioNews, n.UrlName ?? ""),
                  //Url = $"{model.LinkDettaglioNews}?selectednews={n.ItemDefaultUrl}",
                  Type = n.Type ?? string.Empty,
                  Date = n.Date ?? DateTime.MinValue,
                  Highlighted = n.Highlighted ?? false,
                  Category = n.Category ?? string.Empty,


               };
               newsModel.Add(newModel);
            }
         }

         return newsModel;
      }

      private async Task<IEnumerable<WamgroupCustomNewsItem>> GetNews(
      int Skip = 0, int Take = 0)
      {

         var news = await restClientService.GetItems<WamgroupCustomNewsItem>(new List<string> { "Id", "Title", "Intro", "Content", "ThumbnailImage", "DateCreated", "ItemDefaultUrl", "UrlName", "Type", "Date", "DetailImage", "Highlighted", "Category" }, Take, null, null, Skip);
         return news.ToList();
      }
   }
}
