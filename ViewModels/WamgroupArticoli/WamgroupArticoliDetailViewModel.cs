using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupArticoli
{
    public class WamgroupArticoliDetailViewModel
   {
      public Articolo Articolo { get; set; }
      public IList<VarianteArticolo> VariantiArticolo { get; set; }
      public IViewComponentContext Context { get; set; }
   }
}
