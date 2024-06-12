using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow
{
    public class ColumnColor
    {
        [DisplayName("Text color")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Inherit\",\"Name\":\"Inherit\",\"Value\":0}," +
           "{\"Title\":\"Column color\",\"Name\":\"Column color\",\"Value\":1}]")]
        public int TextColorType { get; set; } = 0;

        [DisplayName("Color")]
        [ColorPalette("WamgroupColors")]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"TextColorType\",\"operator\":\"Equals\",\"value\":1}]}")]
        public string TextColor { get; set; } = string.Empty;
    }
}
