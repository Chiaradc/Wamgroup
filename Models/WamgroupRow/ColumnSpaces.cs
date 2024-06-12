using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow
{
    public class ColumnSpaces
    {
        [DefaultValue(ColumnSpace.Space12)]
        //[Range(1, 12, ErrorMessage = "Number must be between {1} and {2}.")]
        [DisplayName("Desktop spaces")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        //[Choice("[{\"Title\":\"1\",\"Name\":\"1\",\"Value\":1}," +
        //    "{\"Title\":\"2\",\"Name\":\"2\",\"Value\":2}," +
        //    "{\"Title\":\"3\",\"Name\":\"3\",\"Value\":3}," +
        //    "{\"Title\":\"4\",\"Name\":\"4\",\"Value\":4}," +
        //    "{\"Title\":\"5\",\"Name\":\"5\",\"Value\":5}," +
        //    "{\"Title\":\"6\",\"Name\":\"6\",\"Value\":6}," +
        //    "{\"Title\":\"7\",\"Name\":\"7\",\"Value\":7}," +
        //    "{\"Title\":\"8\",\"Name\":\"8\",\"Value\":8}," +
        //    "{\"Title\":\"9\",\"Name\":\"9\",\"Value\":9}," +
        //    "{\"Title\":\"10\",\"Name\":\"10\",\"Value\":10}," +
        //    "{\"Title\":\"11\",\"Name\":\"11\",\"Value\":11}," +
        //    "{\"Title\":\"12\",\"Name\":\"12\",\"Value\":12}," +
        //    "{\"Title\":\"Hide\",\"Name\":\"Hide\",\"Value\":0}]")]
        public ColumnSpace DesktopSpacesCount { get; set; } = ColumnSpace.Space12;

        [DefaultValue(ColumnSpace.Space12)]
        [DisplayName("Tablet spaces")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        public ColumnSpace TabletSpacesCount { get; set; } = ColumnSpace.Space12;

        [DefaultValue(ColumnSpace.Space12)]
        [DisplayName("Mobile spaces")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        public ColumnSpace MobileSpacesCount { get; set; } = ColumnSpace.Space12;

     
   }
}
