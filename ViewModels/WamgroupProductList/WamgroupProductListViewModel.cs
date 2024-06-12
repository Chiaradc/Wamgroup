namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupProductList
{
   public class WamgroupProductListViewModel
   {
      public string? Title { get; set; }
      public int? MaxNumber { get; set; }
      public IEnumerable<WamgroupProductListItemModel> Items { get; set; } = Enumerable.Empty<WamgroupProductListItemModel>();
   }

   public class WamgroupProductListItemModel
   {
      public string? Title { get; set; }
      public string? Description { get; set; }
      public string? ImageUrl { get; set; }
      public string? Url { get; set; }
   }
}