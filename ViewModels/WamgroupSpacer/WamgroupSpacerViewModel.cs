using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSpacer
{
   public class WamgroupSpacerViewModel
   {
      public string AboveMargin { get; set; } 

      public string BelowMargin { get; set; } 


      public bool AddVerticalLine { get; set; }

      public IViewComponentContext Context { get; set; }

   }
}
