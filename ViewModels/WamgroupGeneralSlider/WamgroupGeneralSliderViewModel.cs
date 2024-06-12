
namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupGeneralSlider
{
   public class WamgroupGeneralSliderViewModel
   {
      public IEnumerable<WamgroupGeneraSliderItem> Items { get; set; } = Enumerable.Empty<WamgroupGeneraSliderItem>();
   }

   public class WamgroupGeneraSliderItem
   {
      public string DesktopImageUrl { get; set; } = string.Empty;
      public string TabletImageUrl { get; set; } = string.Empty;
      public string MobileImageUrl { get; set; } = string.Empty;
      public string ImageAlt { get; set; } = string.Empty;
      public int Width { get; set; }
      public int Height { get; set; }
      public string Title { get; set; } = string.Empty;
      public string Text { get; set; } = string.Empty;
      public string CTALink { get; set; } = string.Empty;
      public string CTAText { get; set; } = string.Empty;
      public bool CTATargetBlank { get; set; }
      public string TipoContenuto { get; set; } = "1";
      public string CodiceVideo { get; set; } = string.Empty;
      public string TipoVideo { get; set; } = "1";
      public string VideoLocaleUrl { get; set; } = string.Empty;
      public bool FullWidthText { get; set; }
      public string TextAlignment { get; set; } = "1";
      public bool VisualizzaControlli { get; set; }
      public bool VisualizzaModale { get; set; }
      public string BackgroundColor { get; set; } = string.Empty;
      public string TextColor { get; set; } = string.Empty;
      public int BackgroundOpacity { get; set; } = 100;
   }
}
