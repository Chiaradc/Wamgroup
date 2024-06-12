using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupArticoli
{
    public class WamgroupMagazzinoViewModel
   {
      public IList<RigaMagazzino> RigheMagazzino { get; set; }
      public IViewComponentContext Context { get; set; }
   }
}
