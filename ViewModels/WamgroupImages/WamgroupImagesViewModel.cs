using Elogic.Sitefinity.Infrastructure.Images;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupImages
{
    public class WamgroupImagesViewModel
    {
        public ImageModel DesktopImage { get; set; } = ImageModel.Empty;
        public ImageModel TabletImage { get; set; } = ImageModel.Empty;
        public ImageModel MobileImage { get; set; } = ImageModel.Empty;
        public string CssClass { get; set; } = string.Empty;

        public IViewComponentContext Context { get; set; }
    }
}
