using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupCta;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupCta;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Telerik.Sitefinity.Web.UI.Fields;
using static System.Collections.Specialized.BitVector32;


using System.Drawing;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using static Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.CommonDesignTime;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupCta
{
   [SitefinityWidget(Title = "Wamgroup Cta", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Cta", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/cta.jpg")]
    
    [ScriptFile("/css/Widgets/cta.min.css", true, true, false)]
    public class WamgroupCtaViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupCtaViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupCtaEntity> context)
      {
         var model = new WamgroupCtaViewModel();
         model.Title = context.Entity.Title.Replace(Environment.NewLine, "<br/>");

         var id = context.Entity.CTALinkPath?.ItemIdsOrdered?.FirstOrDefault();
        
         if (id != null)
         {
            var nodeDto = await restClientService.GetItem<PageNodeDto>(id);
            model.CTALink = nodeDto.ViewUrl;
         }


            if (context.Entity.ViewType == CtaType.Text)
            {
                model.ViewIsText = true;
                if (context.Entity.ViewTypeText == CtaTypeText.Sx)
                {
                    model.Arrow = "0";
                }
                else if (context.Entity.ViewTypeText == CtaTypeText.Dx)
                {
                    model.Arrow = "1";
                }
            }
            else if (context.Entity.ViewType == CtaType.Card)
            {
                model.ViewIsCard = true;

                if(context.Entity.ViewLine)
                {
                    model.ViewLine = true;
                    model.LineColor = context.Entity.LineColor;
                }
            }
            else if (context.Entity.ViewType == CtaType.List)
            {
                model.ViewIsList = true;
                model.BackgroundColor = context.Entity.BackgroundColor;
            }
            else
            {
                model.ViewIsText = true;
            }
            return View("Default", model);

      }
   }
}
