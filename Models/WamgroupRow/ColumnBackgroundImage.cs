using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow
{
    public class ColumnBackgroundImage
    {
        [DisplayName("Background type")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"None\",\"Name\":\"None\",\"Value\":\"None\"}," +
            "{\"Title\":\"Image\",\"Name\":\"Image\",\"Value\":\"Image\"}]")]
        public SectionBackgroundType BackgroundImage { get; set; } = SectionBackgroundType.None;
      
        [DisplayName("Image")]
        [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false, AllowCreate = false)]
        [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundImage\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
        public MixedContentContext BackgroundDesktopImage { get; set; } = new MixedContentContext();

      /*  [DisplayName("Position")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Cover\",\"Name\":\"Cover\",\"Value\":\"Cover\"}," +
            "{\"Title\":\"Top Left\",\"Name\":\"Top Left\",\"Value\":\"Top Left\"}," +
            "{\"Title\":\"Top Right\",\"Name\":\"Top Right\",\"Value\":\"Top Right\"}," +
            "{\"Title\":\"Bottom Left\",\"Name\":\"Bottom Left\",\"Value\":\"Bottom Left\"}]")]
        public Alignment Position { get; set; } = Alignment.Cover;
*/

    }
}
