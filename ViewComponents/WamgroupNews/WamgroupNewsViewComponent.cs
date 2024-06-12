using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.Models.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNews;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupNews;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Globalization;
using System.Reflection;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupNews
{
    [SitefinityWidget(Title = "Wamgroup News", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica News", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/news.jpg")]
    [ScriptFile("/lib/swiper/swiper-bundle.min.js", false, true, true)]
    [ScriptFile("/lib/masonry/masonry.pkgd.min.js", false, true, true)]
    [ScriptFile("/js/widgets/news.min.js", false, true, true)]
    [ScriptFile("/css/widgets/news.min.css", true, true, true)]
    public class WamgroupNewsViewComponent : ViewComponent
    {
        private readonly RestClientService restClientService;
        private readonly NewsService newsService;

        public WamgroupNewsViewComponent(RestClientService restClientService, CustomScriptService customScriptService, NewsService newsService)
        {
            this.restClientService = restClientService;
            this.newsService = newsService;
            customScriptService.RegisterType(this.GetType());
        }

        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupNewsEntity> context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var action = context.State.TryGetValue(RequestPreparation.SelectedAction, out object objAction) ? (objAction as string) ?? string.Empty : string.Empty;

            if (action == string.Empty || action == "list")
            {

                var topN = 0;
                int anno = 0;
                int categoria = 0;
                var titleSection = string.Empty;
                WamgroupNewsViewModel model = new WamgroupNewsViewModel();

                // TODO: Widget News, Configurare il model

                if (context != null)
                {
                    topN = context.Entity.TopN;
                  /*  titleSection = context.Entity.TitleSection ?? string.Empty;*/
                    model.SelettorePerNascondereElemento = context.Entity.HideElementSelector ?? string.Empty;
                    model.MostraReadMore = context.Entity.MostraReadMore;
                    model.TipoPresentazione = context.Entity.TipoPresentazione ?? "slider";
                    if(!string.IsNullOrEmpty(context.Entity.TitleSection))
                    {
                        model.TitleSection = context.Entity.TitleSection;
                    }
                    if (context.Entity.MostraReadMore && !string.IsNullOrEmpty(context.Entity.LinkReadMore?.Href))
                    {
                        model.LinkDiscoverMore = context.Entity.LinkReadMore.Href;
                        model.TestoLinkDiscoverMore = context.Entity.LinkReadMore.Text;
                        model.TargetLinkDiscoverMore = context.Entity.LinkReadMore.Target;
                    }
                    if (!string.IsNullOrEmpty(context.Entity.LinkDettaglioNews?.Href))
                    {
                        model.LinkDettaglioNews = context.Entity.LinkDettaglioNews.Href;
                        model.TestoLinkDettaglioNews = context.Entity.LinkDettaglioNews.Text;
                        model.TargetLinkDettaglioNews = context.Entity.LinkDettaglioNews.Target;
                    }

                }
                model.TopN = topN;

                model.News = await newsService.GetNewsModelAsync(0, topN, model.LinkDettaglioNews);


                return View($"Default", model);
            }
            else if (action == "detail")
            {
                if (!context.State.TryGetValue(RequestPreparation.SelectedItem, out object objItem))
                {
                    throw new Exception("Null item");
                }
                else
                {
                    var item = (objItem as string) ?? "";

                    var news = await GetSingleNews(item);

                    var imgLista = "";
                    var widthLista = 0;
                    var heightLista = 0;

                    var imgDettaglio = "";
                    var widthDettaglio = 0;
                    var heightDettaglio = 0;

                    if (news.ThumbnailImage?.Length > 0)
                    {
                        var mediaFile = news.ThumbnailImage[0];
                        imgLista = mediaFile.Url;
                        widthLista = mediaFile.Width;
                        heightLista = mediaFile.Height;
                    

                    }
                    if (news.DetailImage?.Length > 0)
                    {
                        var mediaFile = news.DetailImage[0];
                        imgDettaglio = mediaFile.Url;
                        widthDettaglio = mediaFile.Width;
                        heightDettaglio = mediaFile.Height;


                    }

                    WamgroupNewsItemViewModel model = new WamgroupNewsItemViewModel
                    {
                        ImmagineLista = imgLista,
                        //AltImmagine = alt,
                        DataStringa = news.DateCreated?.ToString(CultureInfo.CurrentCulture.Name == "it-IT" ? Constants.ItalianDateFormat : Constants.EnglishDateFormat) ?? "",
                        Titolo = news.Title ?? string.Empty,
                        Intro = news.Intro ?? string.Empty,
                        WidthLista = widthLista,
                        HeightLista = heightLista,
                        ImmagineDettaglio = imgDettaglio,
                        WidthDettaglio = widthDettaglio,
                        HeightDettaglio = heightDettaglio,
                        //Url = context.GetDefaultUrl(),
                        Type = news.Type ?? string.Empty,
                        Date = news.Date ?? DateTime.MinValue,
                        Highlighted = news.Highlighted ?? false,
                        Category = news.Category ?? string.Empty
                       
                };
                    return View($"Detail", model);
                }


            }
            else
            {
                throw new Exception("Wrong action");
            }

        }

        private async Task<WamgroupCustomNewsItem> GetSingleNews(string url)
        {
            var parUrl = url.StartsWith("/") ? url : $"/{url}";
            var news = await restClientService.GetItems<WamgroupCustomNewsItem>(new List<string> { "Id", "Title", "Intro", "Content", "ThumbnailImage", "DateCreated", "ItemDefaultUrl", "UrlName", "Type", "Date", "DetailImage", "Highlighted", "Category" }, 0, parUrl);
            return news.FirstOrDefault() ?? new WamgroupCustomNewsItem();
        }

        private async Task<IEnumerable<WamgroupCustomNewsItem>> GetNews(
              int topN = 0)
        {

            var news = await restClientService.GetItems<WamgroupCustomNewsItem>(new List<string> { "Id", "Title", "Intro", "Content", "ThumbnailImage", "DateCreated", "ItemDefaultUrl", "UrlName", "Type", "Date", "DetailImage", "Highlighted", "Category" }, topN);
            return news.ToList();
        }
    }
}
