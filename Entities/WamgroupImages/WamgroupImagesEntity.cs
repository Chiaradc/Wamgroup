using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupImages
{
   public class WamgroupImagesEntity
   {
      
      [DisplayName("Immagine desktop")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? DesktopImages { get; set; }

      [DisplayName("Immagine tablet")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? TabletImages { get; set; }

      [DisplayName("Immagine mobile")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? MobileImages { get; set; }

      [DisplayName("Alt Immagine")]
      public string? ImageAlt { get; set; }

      [DisplayName("Classe css")]
      public string? CssClass { get; set; }

   }
}
