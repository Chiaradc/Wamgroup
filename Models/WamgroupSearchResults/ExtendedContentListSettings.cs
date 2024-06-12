using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults
{

   /// <summary>
   /// Represents the extended settings for displaying lists.
   /// </summary>
   [MappedType(DataType = "resultsListSettings")]
   [DataContract]
   public class ExtendedContentListSettings : ContentListSettings
   {
      /// <summary>
      /// Gets or sets a value indicating whether to show all items in the list without pagination.
      /// </summary>
      public bool ShowAllResults { get; set; }
   }

}
