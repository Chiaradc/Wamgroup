using Elogic.Wamgroup.Sito2023.Caching.Interfaces;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupBanner;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupBanner;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk.Dto;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupBanner
{
    [SitefinityWidget(Title = "Wamgroup Banner", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Banner", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/banner.jpg")]
   public class WamgroupBannerViewComponent :ViewComponent
   {
      
      private readonly ICacheManager cacheManager;
      private readonly RestClientService restClientService;

      public WamgroupBannerViewComponent(ICacheManager cacheManager, RestClientService restClientService)
      {
         this.cacheManager = cacheManager;
         this.restClientService = restClientService;
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupBannerEntity> context)
      {
         
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }


         WamgroupBannerViewModel model = new WamgroupBannerViewModel
         {
            Testo = context.Entity.Testo ?? ""
         };


         var id = context.Entity.ColoreSfondo?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            
            var coloreSfondo = await restClientService.GetItem<WamgroupColorSchemaItem>(id);

            model.BackgroundColorStyle = coloreSfondo?.BackgroundStyle ?? string.Empty;
            model.BackgroundColorClass = coloreSfondo?.BackgroundClass ?? string.Empty;

         }

         id = context.Entity.ColoreTesto?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var coloreTesto = await restClientService.GetItem<WamgroupColorSchemaItem>(id);
            model.TextColorStyle = coloreTesto?.TextStyle ?? string.Empty;
            model.TextColorClass = coloreTesto?.TextClass ?? string.Empty;

         }

         ////carico le immagini
         id = context.Entity.Images?.ItemIdsOrdered?.FirstOrDefault();
         if (id != null)
         {
            var mediaFile = await restClientService.GetItem<ImageDto>(id);
            if (string.IsNullOrWhiteSpace(model.ImageAlt))
               model.ImageAlt = mediaFile.UrlName;
            model.DesktopImageUrl = mediaFile.Url;
            model.TabletImageUrl = mediaFile.Url;
            model.MobileImageUrl = mediaFile.Url;
         }


         //Guid guid = viewModel.Properties.Images.FirstOrDefault()?.FileGuid ?? Guid.Empty;
         //MediaFileInfo mediaFile = MediaFileInfo.Provider.Get(guid, siteService.CurrentSite.SiteID);
         //if (mediaFile != null)
         //{
         //   if (string.IsNullOrWhiteSpace(model.ImageAlt))
         //      model.ImageAlt = mediaFile.FileName + mediaFile.FileExtension;
         //   model.DesktopImageUrl = mediaFileUrlRetriever.Retrieve(mediaFile).WithSizeConstraint(SizeConstraint.Width(1400)).RelativePath.TrimStart('~');
         //   model.TabletImageUrl = mediaFileUrlRetriever.Retrieve(mediaFile).WithSizeConstraint(SizeConstraint.Width(800)).RelativePath.TrimStart('~');
         //   model.MobileImageUrl = mediaFileUrlRetriever.Retrieve(mediaFile).WithSizeConstraint(SizeConstraint.Width(400)).RelativePath.TrimStart('~');
         //}


         return View("Default", model);
      }
   }
}
