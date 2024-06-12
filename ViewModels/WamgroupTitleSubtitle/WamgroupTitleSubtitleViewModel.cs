using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTitleSubtitle
{
   public class WamgroupTitleSubtitleViewModel
   {
      
      public string? Title { get; set; }
      public string? Subtitle { get; set; }
      public bool RenderTitleAsH1 { get; set; }
      public string? linkUrl { get; set; }
      public string? linkText { get; set; }
      public string? linkTargetBlank { get; set; }
      public bool ViewLine { get; set; }
      public string? LineColor { get; set; }

      public IViewComponentContext Context { get; set; }
   }
}
