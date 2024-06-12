using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTimeline
{
    public class WamgroupTimelineEntity
   {
      [DisplayName("Elementi")]
      [Content(Type = Constants.DynamicTypeTimelineItem, AllowMultipleItemsSelection = true)]
      public MixedContentContext? Elementi { get; set; }
   }
}
