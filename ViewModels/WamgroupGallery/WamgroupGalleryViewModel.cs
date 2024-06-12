namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupGallery
{
   public class WamgroupGalleryViewModel
   {
      public IEnumerable<GalleryWidgetItemModel> Items { get; set; } = Enumerable.Empty<GalleryWidgetItemModel>();
      public string? SelettorePerNascondereElemento { get; set; }
   }

   public class GalleryWidgetItemModel
   {
      
      public string DesktopImageUrl { get; set; } = string.Empty;
      public string TabletImageUrl { get; set; } = string.Empty;
      public string MobileImageUrl { get; set; } = string.Empty;
      public string ImageAlt { get; set; } = string.Empty;
      public int Width { get; set; }
      public int Height { get; set; }

      public string CodiceVideo { get; set; } = string.Empty;
      public string TipoVideo { get; set; } = "1";
   }
}
