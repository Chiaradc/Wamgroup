using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupGallery
{
    public class WamgroupGalleryEntity
   {
      
      [DisplayName("Elementi")]
      [Content(Type = Constants.DynamicTypeGalleryItem, AllowMultipleItemsSelection = true)]
      public MixedContentContext? Elementi { get; set; }

      [DisplayName("Selettore dell'elemento da nascondere in caso di mancanza di gallery")]
      public string? HideElementSelector { get; set; }
   }

  
}
