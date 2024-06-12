namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupCta
{
   public class WamgroupCtaViewModel
   {
      public string Title { get; set; } = string.Empty;
        public string CTALink { get; set; } = string.Empty;
        public bool ViewIsText { get; set; }
        public string Arrow { get; set; } = string.Empty;
        public bool ViewIsCard { get; set; }
        public bool ViewLine { get; set; }
        public string? LineColor { get; set; }
        public bool ViewIsList { get; set; }
        public string? BackgroundColor { get; set; }
        public bool TargetBlank { get; set; }
    }
}
