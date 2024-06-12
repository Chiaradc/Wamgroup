using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSlidingTitle;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSlidingTitle;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Telerik.Sitefinity.Web.UI.Fields;
using static System.Collections.Specialized.BitVector32;


using System.Drawing;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Elogic.Wamgroup.Sito2023.NetCore.Services;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.ElogicSlidingTitle
{
   [SitefinityWidget(Title = "Wamgroup Sliding Title", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Sliding Title", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/slidingtitle.jpg")]
   [ScriptFile("/js/Widgets/slidingtitle.min.js", false, true, false)]
   [ScriptFile("/css/Widgets/slidingtitle.min.css", true, true, false)]
   public class WamgroupSlidingTitleViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupSlidingTitleViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupSlidingTitleEntity> context)
      {
         var model = new WamgroupSlidingTitleViewModel();
         model.ImageAlt = context.Entity.ImageAlt ?? "";
         model.Title = context.Entity.Title.Replace(Environment.NewLine, "<br/>");

         var id = context.Entity.DesktopImages?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var mediaFile = await restClientService.GetItem<ImageDto>(id);
            model.Width = mediaFile.Width;
            model.Height = mediaFile.Height;
            if (string.IsNullOrWhiteSpace(model.ImageAlt))
               model.ImageAlt = mediaFile.UrlName;
            model.DesktopImageUrl = mediaFile.Url;
            model.TabletImageUrl = mediaFile.Url;
            model.MobileImageUrl = mediaFile.Url;
         }

         id = context.Entity.TabletImages?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var mediaFile = await restClientService.GetItem<ImageDto>(id);
            model.TabletImageUrl = mediaFile.Url;
            model.MobileImageUrl = mediaFile.Url;
         }

         id = context.Entity.MobileImages?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var mediaFile = await restClientService.GetItem<ImageDto>(id);
            model.MobileImageUrl = mediaFile.Url;
         }

         id = context.Entity.CTALinkPath?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var nodeDto = await restClientService.GetItem<PageNodeDto>(id);
            model.CTALink = nodeDto.ViewUrl;
            model.CTAText = !string.IsNullOrWhiteSpace(context.Entity.CTALinkText) ? context.Entity.CTALinkText : "Esplora altro";
         }
         return View("Default", model);

      }
   }
}
