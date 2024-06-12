using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupArticoli
{
    public class WamgroupArticoliEntity
   {
      public string CercaFiltro { get; set; }

      public IList<Articolo> Articoli { get; set; }

   }
}
