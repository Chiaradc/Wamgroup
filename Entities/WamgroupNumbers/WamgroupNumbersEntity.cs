using static Telerik.Sitefinity.Data.Linq.Basic.QueryArgs;
using System.Reflection.Emit;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel.DataAnnotations;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNumbers
{
   public class WamgroupNumbersEntity
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
      [DisplayName("Numero 1")]
      public string Number1 { get; set; } = string.Empty;
      [DisplayName("Numero 1 small")]
      public string Number1Small { get; set; } = string.Empty;
      [DisplayName("Caption 1")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption1 { get; set; } = string.Empty;
      [DisplayName("Numero 2")]
      public string Number2 { get; set; } = string.Empty;
      [DisplayName("Numero 2 small")]
      public string Number2Small { get; set; } = string.Empty;
      [DisplayName("Caption 2")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption2 { get; set; } = string.Empty;
      [DisplayName("Numero 3")]
      public string Number3 { get; set; } = string.Empty;
      [DisplayName("Numero 3 small")]
      public string Number3Small { get; set; } = string.Empty;
      [DisplayName("Caption 3")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption3 { get; set; } = string.Empty;
      [DisplayName("Numero 4")]
      public string Number4 { get; set; } = string.Empty;
      [DisplayName("Numero 4 small")]
      public string Number4Small { get; set; } = string.Empty;
      [DisplayName("Caption 4")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption4 { get; set; } = string.Empty;
      [DisplayName("Numero 5")]
      public string Number5 { get; set; } = string.Empty;
      [DisplayName("Numero 5 small")]
      public string Number5Small { get; set; } = string.Empty;
      [DisplayName("Caption 5")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption5 { get; set; } = string.Empty;
      [DisplayName("Numero 6")]
      public string Number6 { get; set; } = string.Empty;
      [DisplayName("Numero 6 small")]
      public string Number6Small { get; set; } = string.Empty;
      [DisplayName("Caption 6")]
      [DataType(customDataType: KnownFieldTypes.TextArea)]
      public string Caption6 { get; set; } = string.Empty;
      [DisplayName("Testo finale")]
      public string FinalText { get; set; } = string.Empty;
      [DisplayName("Visualizzazione Key number")]
      public bool KeyNumberView { get; set; }
   }
}
