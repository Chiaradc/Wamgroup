using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupFooter
{
   [SitefinityWidget(Title = "Wamgroup Footer", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Footer", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/footer.jpg")]
   public class WamgroupFooterViewComponent : ViewComponent
   {
      public WamgroupFooterViewComponent()
      {
      }

      public IViewComponentResult Invoke()
      {
         return View("Default");
      }
   }
}
