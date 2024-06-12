using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow
{
    public class ColumnBackground
    {
        [DisplayName("Background type")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"None\",\"Name\":\"None\",\"Value\":\"None\"}," +
            "{\"Title\":\"Color\",\"Name\":\"Color\",\"Value\":\"Color\"}]")]
        public SectionBackgroundType BackgroundType { get; set; } = SectionBackgroundType.None;

        [DisplayName("Color")]
        [ColorPalette("WamgroupBackground")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Color\"}]}")]
        public string BackgroundColor { get; set; } = string.Empty;

        [DisplayName("Opacity perc.")]
        [DefaultValue(100)]
        [Range(1, 100, ErrorMessage = "Opacity must be between {1} and {2}.")]
        public int Opacity { get; set; } =100;
    }
}
