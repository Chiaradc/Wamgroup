using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSection;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSection;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System.Drawing;
using Telerik.Sitefinity.Versioning.Model;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupSection
{
   [SitefinityWidget(Title = "Wamgroup section", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIconText = "Modifica Section", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/section.jpg")]
/*   [ScriptFile("/content/js/slickwaifix.js", false, true, true)]*/
   public class WamgroupSectionViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;
      private readonly IRenderContext renderContext;

      public WamgroupSectionViewComponent(RestClientService restClientService, CustomScriptService customScriptService, IRenderContext renderContext)
      {
         this.restClientService = restClientService;
         this.renderContext = renderContext;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<WamgroupSectionEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }
         WamgroupSectionViewModel sectionViewModel = new WamgroupSectionViewModel
         {
            Context = context
         };

         sectionViewModel.FullWidth = context.Entity.FullWidth;

         var sectionStyle = string.Empty;
         var sectionClasses = "position-relative";

         var txtColor = string.Empty;
         var colorsHelper = new ColorsHelper();
         if (colorsHelper.ParseTextColorFromHEX(context.Entity.TextColor, out txtColor))
         {
            sectionClasses += $" {txtColor}";
         }
         else
            sectionStyle += $"color: {context.Entity.TextColor};";

         if (!renderContext.IsEdit)
         {
            if (context.Entity.DisplayOnDesktop)
               sectionClasses += " d-xl-block";
            else
               sectionClasses += " d-xl-none";

            if (context.Entity.DisplayOnTablet)
               sectionClasses += " d-md-block";
            else
               sectionClasses += " d-md-none";

            if (context.Entity.DisplayOnMobile)
               sectionClasses += " d-block";
            else
               sectionClasses += " d-none";
         }

         if (context.Entity.BackgroundType == SectionBackgroundType.Color)
         {

            string bgColor = string.Empty;
            if (colorsHelper.ParseBackgroundColorFromHEX(context.Entity.BackgroundColor, out bgColor))
            {
               sectionClasses += $" {bgColor}";
            }
            else
               sectionStyle += $"background-color: {context.Entity.BackgroundColor};";
         }
         else if (context.Entity.BackgroundType == SectionBackgroundType.Image)
         {
            sectionViewModel.BackgroundIsImage = true;

            var backImageClasses = "z-n1 object-fit-cover w-100 h-100 position-absolute";
            switch (context.Entity.BackgroundImagePosition)
            {
               case SectionBackgroundImagePosition.Top:
                  backImageClasses = "z-n1 w-100 h-auto position-absolute top-0 z-n1";
                  break;
               case SectionBackgroundImagePosition.Centered:
                  backImageClasses = "z-n1 w-100 h-auto position-absolute top-50 z-n1 translate-middle-y";
                  break;
               case SectionBackgroundImagePosition.Bottom:
                  backImageClasses = "z-n1 w-100 h-auto position-absolute bottom-0 z-n1";
                  break;
            }
            sectionViewModel.BackgroundImageClasses = backImageClasses;

            var id = context.Entity.BackgroundDesktopImage?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
               var mediaFile = await restClientService.GetItem<ImageDto>(id);
               if (string.IsNullOrWhiteSpace(sectionViewModel.BackgroundImageAlternativeText))
                  sectionViewModel.BackgroundImageAlternativeText = mediaFile.UrlName;
               sectionViewModel.BackgroundDestktopImageUrl = mediaFile.Url;
               sectionViewModel.BackgroundTabletImageUrl = $"{mediaFile.Url}?size=768";
               sectionViewModel.BackgroundMobileImageUrl = $"{mediaFile.Url}?size=600";
            }

            id = context.Entity.BackgroundTabletImage?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
               var mediaFile = await restClientService.GetItem<ImageDto>(id);
               sectionViewModel.BackgroundTabletImageUrl = mediaFile.Url;
               sectionViewModel.BackgroundMobileImageUrl = $"{mediaFile.Url}?size=600";
            }

            id = context.Entity.BackgroundMobileImage?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
               var mediaFile = await restClientService.GetItem<ImageDto>(id);
               sectionViewModel.BackgroundMobileImageUrl = mediaFile.Url;
            }


           
         }

         if (context.Entity.CornersType == SectionCornersType.LeftToRight)
         {
            sectionClasses += " corners-ltr";
         }
         else if (context.Entity.CornersType == SectionCornersType.RightToLeft)
         {
            sectionClasses += " corners-rtl";
            }

            if (!string.IsNullOrEmpty(context.Entity.Link?.Href))
            {
                sectionViewModel.Link = context.Entity.Link.Href;
                sectionClasses += " linked";
            }

            sectionViewModel.SectionClasses = sectionClasses;
         sectionViewModel.SectionStyle = sectionStyle;

         return View("Default", sectionViewModel);
      }
   }
}
