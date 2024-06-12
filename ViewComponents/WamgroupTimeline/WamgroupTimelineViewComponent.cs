using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.Models.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTimeline;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTimeline;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupTimeline
{
    [SitefinityWidget(Title = "Wamgroup Timeline", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Timeline", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/generalslider.jpg")]
    [ScriptFile("/lib/swiper/swiper-bundle.min.js", false, true, true)]
    [ScriptFile("/js/Widgets/timeline.min.js", false, true, true)]
    [ScriptFile("/css/Widgets/timeline.min.css", true, true, true)]
    public class WamgroupTimelineViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupTimelineViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupTimelineEntity> context)
      {
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         var items = context.Entity.Elementi?.ItemIdsOrdered;
         List<WamgroupTimelineViewModelItem> models = new List<WamgroupTimelineViewModelItem>();
         WamgroupTimelineViewModel model = new WamgroupTimelineViewModel();

         if (items != null)
         {
            foreach (var item in items)
            {
               var timelineItem = await restClientService.GetItem<WamgroupTimelineItem>(item,
                                   new List<string> { "Title", "Text", "Image", "ImageAlt" },
                                                    Constants.DynamicTypeTimelineItem).ConfigureAwait(true);
               var timelineModel = new WamgroupTimelineViewModelItem
               {
                  Title = timelineItem.Title ?? string.Empty,
                  Text = timelineItem.Text ?? string.Empty,
                  
                  ImageAlt = timelineItem.ImageAlt ?? string.Empty
               };

               if (timelineItem.Image?.Length > 0)
               {
                  var mediaFile = timelineItem.Image[0];
                  timelineModel.Width = mediaFile.Width;
                  timelineModel.Height = mediaFile.Height;
                  if (string.IsNullOrWhiteSpace(timelineItem.ImageAlt))
                     timelineModel.ImageAlt = mediaFile.UrlName;
                  timelineModel.ImageUrl = mediaFile.Url;
                  
               }

               models.Add(timelineModel);
            }
         }

         model.Items = models;
         return View($"Default", model);
      }
   }
}
