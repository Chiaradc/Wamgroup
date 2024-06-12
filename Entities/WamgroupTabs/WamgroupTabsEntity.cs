using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTabs
{
   public class WamgroupTabsEntity
   {
      
      [DisplayName("Tabs")]
      public IList<TabDefinition>? TabsDefinition { get; set; }
   }

   public class TabDefinition
   {
      [DisplayName("Text")]
      public string? Text { get; set; }

      [DisplayName("Image")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? Image { get; set; }

      [DisplayName("Hover Image")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? HoverImage { get; set; }

   }
}
