using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupBanner
{
    public class WamgroupBannerEntity
   {
      [DisplayName("Testo")]
      [DataType(customDataType:KnownFieldTypes.Html)]
      public string? Testo { get; set; } = string.Empty;

      [DisplayName("Colore sfondo")]
      //[ColorPalette("WamgroupBackground")]
      //[DataType(customDataType: KnownFieldTypes.Color)]
      [Content(Type = Constants.DynamicTypeColorSchema, AllowMultipleItemsSelection = false)]
      public MixedContentContext? ColoreSfondo { get; set; } //= "__TRANSPARENT__";

      [DisplayName("Colore testo")]
      //[ColorPalette("WamgroupColors")]
      //[DataType(customDataType: KnownFieldTypes.Color)]
      [Content(Type = Constants.DynamicTypeColorSchema, AllowMultipleItemsSelection = false)]
      public MixedContentContext? ColoreTesto { get; set; } //= "__TRANSPARENT__";


      [DisplayName("Immagine")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = true)]
      public MixedContentContext? Images { get; set; }

      [DisplayName("Alt immagine")]
      public string AltImmagine { get; set; } = string.Empty;

   }
}
