using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using static Telerik.Sitefinity.Data.Linq.Basic.QueryArgs;
using System.Reflection.Emit;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupCta
{ 
   public class WamgroupCtaEntity
   {
  /*      [DisplayName("Immagine Background")]
        [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false)]
        public MixedContentContext? DesktopImages { get; set; }

        [DisplayName("Alt Immagine Background")]
        public string? ImageAlt { get; set; }*/

        [DisplayName("Titolo")]
        public string Title { get; set; } = "Read more";

        /*  [DisplayName("Sottotitolo")] 
          public string Sottotitolo { get; set; } = string.Empty;*/
        [DisplayName("Tipo di visualizzazione")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public CtaType ViewType { get; set; } = CtaType.Text;

        [DisplayName("Allineamento freccia")]
        [ContentSection("Allineamento freccia")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"ViewType\",\"operator\":\"Equals\",\"value\":\"Text\"}]}")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public CtaTypeText ViewTypeText { get; set; } = CtaTypeText.Sx;

        [DisplayName("Visualizza linea")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"ViewType\",\"operator\":\"Equals\",\"value\":\"Card\"}]}")]
        public bool ViewLine { get; set; }

        [DisplayName("Colore linea")]
        [ColorPalette("WamgroupColors")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"ViewLine\",\"operator\":\"Equals\",\"value\":\"true\"}]}")]
        public string LineColor { get; set; } = string.Empty;

        [DisplayName("Background Color")]
        [ColorPalette("WamgroupColors")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"ViewType\",\"operator\":\"Equals\",\"value\":\"List\"}]}")]
        public string BackgroundColor { get; set; } = string.Empty;

        [DisplayName("Link Interno")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext? CTALinkPath { get; set; }

        [DisplayName("Apri in una nuova scheda")]
        [DefaultValue(true)]
        public bool TargetBlank { get; set; }

    }
}
