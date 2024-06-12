using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using static Telerik.Sitefinity.Data.Linq.Basic.QueryArgs;
using System.Reflection.Emit;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSlidingTitle
{
   public class WamgroupSlidingTitleEntity
   {
      [DisplayName("Immagine di fondo desktop")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? DesktopImages { get; set; }

      [DisplayName("Immagine di fondo tablet")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? TabletImages { get; set; }

      [DisplayName("Immagine di fondo mobile")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
      public MixedContentContext? MobileImages { get; set; }

      [DisplayName("Alt Immagine di fondo")]
      public string? ImageAlt { get; set; }
      [DisplayName("Titolo")]
      public string Title { get; set; } = string.Empty;
      
      [DisplayName("Link Interno")]
      [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
      public MixedContentContext? CTALinkPath { get; set; }

      [DisplayName("Testo Link Interno")]
      public string CTALinkText { get; set; } = string.Empty;
   }
}
