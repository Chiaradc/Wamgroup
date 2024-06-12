using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNumbers;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupNumbers;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupNumbers
{
   [SitefinityWidget(Title = "Wamgroup Numbers", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Numbers", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/numbers.jpg")]
   [ScriptFile("/css/widgets/numbers.min.css", true, true, false)]
   [ScriptFile("/js/widgets/numbers.min.js", false, true, false)]
   public class WamgroupNumbersViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupNumbersViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupNumbersEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         var model = new WamgroupNumbersViewModel();

         model.ImageAlt = context.Entity.ImageAlt ?? "";
         model.Title = context.Entity.Title;
         model.Number1 = context.Entity.Number1;
         model.Number1Small = context.Entity.Number1Small;
         model.Caption1 = context.Entity.Caption1?.Replace(Environment.NewLine, "<br />") ?? "";
         model.Number2 = context.Entity.Number2;
         model.Number2Small = context.Entity.Number2Small;
         model.Caption2 = context.Entity.Caption2?.Replace(Environment.NewLine, "<br />") ?? "";
         model.Number3 = context.Entity.Number3;
         model.Number3Small = context.Entity.Number3Small;
         model.Caption3 = context.Entity.Caption3?.Replace(Environment.NewLine, "<br />") ?? "";
         model.Number4 = context.Entity.Number4;
         model.Number4Small = context.Entity.Number4Small;
         model.Caption4 = context.Entity.Caption4?.Replace(Environment.NewLine, "<br />") ?? "";
         model.Number5 = context.Entity.Number5;
         model.Number5Small = context.Entity.Number5Small;
         model.Caption5 = context.Entity.Caption5?.Replace(Environment.NewLine, "<br />") ?? "";
         model.Number6 = context.Entity.Number6;
         model.Number6Small = context.Entity.Number6Small;
         model.Caption6 = context.Entity.Caption6?.Replace(Environment.NewLine, "<br />") ?? "";
         model.FinalText = context.Entity.FinalText;
         model.KeyNumberView = context.Entity.KeyNumberView;

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


         return View("Default", model);
      }

   }
}
