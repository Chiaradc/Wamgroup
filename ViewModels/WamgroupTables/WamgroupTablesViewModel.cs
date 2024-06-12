namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTables
{
   public class WamgroupTablesViewModel
   {
      public List<WamgroupTablesRowViewModel> Headers { get; set; } = new List<WamgroupTablesRowViewModel>();

      public List<WamgroupTablesRowViewModel> Rows { get; set; } = new List<WamgroupTablesRowViewModel>();
   }
}
