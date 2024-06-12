using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupGallery;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupGallery;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupGallery
{
    [SitefinityWidget(Title = "Wamgroup Gallery", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Gallery", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/gallery.jpg")]
   [ScriptFile("/js/widgets/video.js", false, true, true)]
   [ScriptFile("/lib/slick/slick.min.js", false, true)]
   [ScriptFile("/js/widgets/gallery.js", false, true, true)]
   [ScriptFile("/lib/slick/slick.css", true, true)]
   [ScriptFile("/lib/slick/slick-theme.css", true, true)]
   [ScriptFile("/css/widgets/gallery.css", true, true, true)]
   [ScriptFile("/js/slickwaifix.js", false, true, true)]
   public class WamgroupGalleryViewComponent : ViewComponent
   {
      
      private readonly RestClientService restClientService;

      public WamgroupGalleryViewComponent(CustomScriptService customScriptService, RestClientService restClientService)
      {
         
         customScriptService.RegisterType(this.GetType());
         this.restClientService = restClientService;
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupGalleryEntity> context)
      {
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         //var items = context.Entity.PagePaths;
         var items = context.Entity.Elementi?.ItemIdsOrdered;
         WamgroupGalleryViewModel model = new WamgroupGalleryViewModel();
         model.SelettorePerNascondereElemento = context.Entity.HideElementSelector;

         List<GalleryWidgetItemModel> models = new List<GalleryWidgetItemModel>();
         if (items != null)
         {
            foreach (var item in items)
            {

               
               var galleryItem = await restClientService.GetItem<WamgroupGalleryItem>(item, new List<string> { "DesktopImage", "TabletImage", "MobileImage", "CodiceVideo", "TipoVideo" });

               var galleryModel = new GalleryWidgetItemModel
               {
                 
                  CodiceVideo = galleryItem.CodiceVideo ?? string.Empty,
                  TipoVideo = galleryItem.TipoVideo ?? string.Empty
               };

               if (galleryItem.DesktopImage?.Length > 0)
               {
                  var mediaFile = galleryItem.DesktopImage[0];
                  galleryModel.Width = mediaFile.Width;
                  galleryModel.Height = mediaFile.Height;
                  if (string.IsNullOrWhiteSpace(galleryModel.ImageAlt))
                     galleryModel.ImageAlt = mediaFile.UrlName;
                  galleryModel.DesktopImageUrl = mediaFile.Url;
                  galleryModel.TabletImageUrl = mediaFile.Url;
                  galleryModel.MobileImageUrl = mediaFile.Url;
               }

               if (galleryItem.TabletImage?.Length > 0)
               {
                  var mediaFile = galleryItem.TabletImage[0];
                  galleryModel.TabletImageUrl = mediaFile.Url;
                  galleryModel.MobileImageUrl = mediaFile.Url;
               }

               if (galleryItem.MobileImage?.Length > 0)
               {
                  var mediaFile = galleryItem.MobileImage[0];
                  galleryModel.MobileImageUrl = mediaFile.Url;
               }



               models.Add(galleryModel);
            }
         }

         model.Items = models;

         return View("Default", model);
      }
   }
}
