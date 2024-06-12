using AngleSharp.Dom;
using Elogic.Sitefinity.Infrastructure.Images;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupImages;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupImages;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupImages
{
    [SitefinityWidget(Title = "Wamgroup Images", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Immagini", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/images.jpg")]
    public class WamgroupImagesViewComponent : ViewComponent
    {
        private readonly RestClientService restClientService;
        private readonly IImagesService imagesService;

        public WamgroupImagesViewComponent(RestClientService restClientService, IImagesService imagesService)
        {
            this.restClientService = restClientService;
            this.imagesService = imagesService;
        }


        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupImagesEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var model = new WamgroupImagesViewModel();
            model.CssClass = context.Entity.CssClass ?? "";

            var id = context.Entity.DesktopImages?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
                var mediaFile = await restClientService.GetItem<ImageDto>(id);
                var imageModel = imagesService.GetImage(mediaFile);
                model.DesktopImage = imageModel;
                imageModel = imagesService.GetImage(mediaFile, 992);
                model.TabletImage = imageModel;
                imageModel = imagesService.GetImage(mediaFile, 540);
                model.MobileImage = imageModel;
            }

            id = context.Entity.TabletImages?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
                var mediaFile = await restClientService.GetItem<ImageDto>(id);
                var imageModel = imagesService.GetImage(mediaFile, 992);
                model.TabletImage = imageModel;
                imageModel = imagesService.GetImage(mediaFile, 540);
                model.MobileImage = imageModel;
            }

            id = context.Entity.MobileImages?.ItemIdsOrdered?.FirstOrDefault();
            if (id != null)
            {
                var mediaFile = await restClientService.GetItem<ImageDto>(id);
                var imageModel = imagesService.GetImage(mediaFile, 540);
                model.MobileImage = imageModel;
            }

            return View("Default", model);
        }
    }
}
