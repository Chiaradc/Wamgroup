using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSpacer;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSpacer;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSection;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;


namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupSpacer
{
    [SitefinityWidget(Title = "Wamgroup Spacer", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/spacer.jpg")]
    public class WamgroupSpacerViewComponent : ViewComponent
    {
        //public async Task<IViewComponentResult> InvokeAsync()
        public IViewComponentResult Invoke(ICompositeViewComponentContext<WamgroupSpacerEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            WamgroupSpacerViewModel sectionViewModel = new WamgroupSpacerViewModel
            {
                AddVerticalLine = context.Entity.AddVerticalLine,
                Context = context
            };

            sectionViewModel.AboveMargin = "1x";
            if (context.Entity.AboveMargin == Misc.Enums.SpacerMargin.None)
                sectionViewModel.AboveMargin = "0x";
            else if (context.Entity.AboveMargin == Misc.Enums.SpacerMargin.Small)
                sectionViewModel.AboveMargin = "1x";
            else if (context.Entity.AboveMargin == Misc.Enums.SpacerMargin.Medium)
                sectionViewModel.AboveMargin = "2x";
            else if (context.Entity.AboveMargin == Misc.Enums.SpacerMargin.Large)
                sectionViewModel.AboveMargin = "3x";

            sectionViewModel.BelowMargin = "1x";
            if (context.Entity.BelowMargin == Misc.Enums.SpacerMargin.None)
                sectionViewModel.BelowMargin = "0x";
            else if (context.Entity.BelowMargin == Misc.Enums.SpacerMargin.Small)
                sectionViewModel.BelowMargin = "1x";
            else if (context.Entity.BelowMargin == Misc.Enums.SpacerMargin.Medium)
                sectionViewModel.BelowMargin = "2x";
            else if (context.Entity.BelowMargin == Misc.Enums.SpacerMargin.Large)
                sectionViewModel.BelowMargin = "3x";

            return View("Default", sectionViewModel);
        }
    }
}
