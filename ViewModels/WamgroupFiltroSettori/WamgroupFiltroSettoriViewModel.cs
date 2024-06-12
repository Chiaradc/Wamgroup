using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupFiltroSettori;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupFiltroSettori
{
   public class WamgroupFiltroSettoriViewModel
   {
      public IList<WamgroupFiltroSettoriModelItem> Settori { get; set; } = new List<WamgroupFiltroSettoriModelItem>();
      public bool MostraPulsanteConferma { get; set; } = true;
    }
}
