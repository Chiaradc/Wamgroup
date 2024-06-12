using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults
{
   /// <summary>
   /// Respresents the options for sorting search results widget options.
   /// </summary>
   public enum SearchResultsSorting
   {
      /// <summary>
      /// Most relevant on top.
      /// </summary>
      [Description("Most relevant on top")]
      MostRelevantOnTop,

      /// <summary>
      /// Newest first.
      /// </summary>
      [Description("Newest first")]
      NewestFirst,

      /// <summary>
      /// Oldest first.
      /// </summary>
      [Description("Oldest first")]
      OldestFirst,
   }
}
