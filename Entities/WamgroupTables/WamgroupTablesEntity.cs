using static Telerik.Sitefinity.Data.Linq.Basic.QueryArgs;
using System.Reflection.Emit;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTables
{
   public class WamgroupTablesEntity
   {
      [DisplayName("Numero di colonne")]
      public int ColNum { get; set; }

      [DisplayName("Numero di righe")]
      public int RowNum { get; set; }

      [DisplayName("Caption")]
      public string Caption { get; set; } = string.Empty;

      [DisplayName("Summary")]
      public string Summary { get; set; } = string.Empty;


      public string TableContent { get; set; } = string.Empty;

   }
}
