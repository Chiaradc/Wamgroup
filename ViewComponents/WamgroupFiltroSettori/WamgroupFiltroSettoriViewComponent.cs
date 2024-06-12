using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupFiltroSettori;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupFiltroSettori;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupCta;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupFiltroSettori;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Telerik.Sitefinity.Versioning.Model;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupFiltroSettori
{
    [SitefinityWidget(Title = "Wamgroup Filtro Settori", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Filtro Settori", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/filtrosettori.jpg")]
    [ScriptFile("/css/widgets/filtroSettori.min.css", true, true, true)]

    public class WamgroupFiltroSettoriViewComponent : ViewComponent
    {
        public WamgroupFiltroSettoriViewComponent(CustomScriptService customScriptService)
        {           
                customScriptService.RegisterType(this.GetType());
        }
        public IViewComponentResult Invoke(IViewComponentContext<WamgroupFiltroSettoriEntity> context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            WamgroupFiltroSettoriViewModel model = new WamgroupFiltroSettoriViewModel
            {
                MostraPulsanteConferma = context.Entity.MostraPulsanteConferma,

            };

            foreach (var item in context.Entity.Settori)
            {
                WamgroupFiltroSettoriModelItem filtroSettoriModelItem = new WamgroupFiltroSettoriModelItem
                {
                    Settore = item.Settore,
                    Azione = item.Azione,

                };
                model.Settori.Add(filtroSettoriModelItem);

                if (!string.IsNullOrEmpty(item.Link?.Href))
                {
                    filtroSettoriModelItem.Link = item.Link.Href;

                }
            }

            return View("Default", model);
        }
    }
}
