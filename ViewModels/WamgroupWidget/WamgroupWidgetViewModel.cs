using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupWidget
{
    public class WamgroupWidgetViewModel
    {
        public string? Codice { get; set; }
        public IList<Articolo> Articoli { get; set; }
        public IViewComponentContext Context { get; set; }
    }
}
