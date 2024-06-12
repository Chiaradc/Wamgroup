namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTimeline
{
   public class WamgroupTimelineViewModel
   {
      public IEnumerable<WamgroupTimelineViewModelItem> Items { get; set; } = Enumerable.Empty<WamgroupTimelineViewModelItem>();
       
   }

   public class WamgroupTimelineViewModelItem
   {
      public string? Id { get; set; }
      public string? Title { get; set; }
      public string? Text { get; set; }
      public string? ImageAlt { get; set; }
      public string? ImageUrl { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
   }
}
