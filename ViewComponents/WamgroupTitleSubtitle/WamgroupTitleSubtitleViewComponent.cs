using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTitleSubtitle;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTitleSubtitle;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupTitleSubtitle
{
   [SitefinityWidget(Title = "Wamgroup Title Subtitle", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Title Subtitle", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/titlesubtitle.jpg")]
    public class WamgroupTitleSubtitleViewComponent : ViewComponent
   {
      public IViewComponentResult Invoke(IViewComponentContext<WamgroupTitleSubtitleEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         WamgroupTitleSubtitleViewModel sectionViewModel = new WamgroupTitleSubtitleViewModel
         {
            RenderTitleAsH1 = context.Entity.RenderTitleAsH1,
            Title = context.Entity.Title,
            Subtitle = context.Entity.Subtitle,
            ViewLine = context.Entity.ViewLine,
            LineColor = context.Entity.LineColor,

            Context = context
         };

         if (!string.IsNullOrEmpty(context.Entity.Link?.Href))
         {
            sectionViewModel.linkUrl = context.Entity.Link.Href;
            sectionViewModel.linkText = context.Entity.Link.Text;
            sectionViewModel.linkTargetBlank = context.Entity.Link.Target;
         }
         
         return View("Default", sectionViewModel);
      }
   }
}
