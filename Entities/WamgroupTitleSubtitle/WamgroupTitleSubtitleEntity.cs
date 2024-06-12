using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTitleSubtitle
{
    public class WamgroupTitleSubtitleEntity
    {

        [DisplayName("Titolo")]
        public string? Title { get; set; } = string.Empty;

        [DisplayName("Sottotitolo")]
        [DataType(customDataType: KnownFieldTypes.Html)]
        public string? Subtitle { get; set; } = string.Empty;

        [DisplayName("Rendi il titolo come H1")]
        public bool RenderTitleAsH1 { get; set; }

        [DisplayName("Visualizza linea")]
        public bool ViewLine { get; set; }

        [DisplayName("Colore linea")]
        [ColorPalette("WamgroupColors")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"ViewLine\",\"operator\":\"Equals\",\"value\":\"true\"}]}")]
        public string LineColor { get; set; } = string.Empty;

        [DisplayName("Link")]
        [DataType(customDataType: "linkSelector")]
        public LinkModel Link { get; set; } = new LinkModel();
    }
}
