namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSlidingTitle
{
   public class WamgroupSlidingTitleViewModel
   {
      public string DesktopImageUrl { get; set; } = string.Empty;
      public string TabletImageUrl { get; set; } = string.Empty;
      public string MobileImageUrl { get; set; } = string.Empty;
      public string ImageAlt { get; set; } = string.Empty;
      public string Title { get; set; } = string.Empty;
      public int Width { get; set; }
      public int Height { get; set; }
      public string CTALink { get; set; } = string.Empty;
      public string CTAText { get; set; } = string.Empty;
   }
}
