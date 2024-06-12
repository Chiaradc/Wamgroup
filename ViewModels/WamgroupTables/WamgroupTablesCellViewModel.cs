namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTables
{
   public class WamgroupTablesCellViewModel
   {
      public string Value { get; set; } = string.Empty;

      public string Alignment { get; set; } = "start";

      public string ColSpan { get; set; } = "1";

      public string RowSpan { get; set; } = "1";

      public bool? NoBorder { get; set; }
   }
}
