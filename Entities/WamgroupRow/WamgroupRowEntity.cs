using Progress.Sitefinity.Renderer.Designers.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow;
using Progress.Sitefinity.Renderer.Designers;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupRow
{
    public class WamgroupRowEntity
    {


        [LocalizedDisplayName("Entity.WamgroupRowEntity.NumberOfColumns")]
        [DefaultValue(1)]
        [Range(1, 12, ErrorMessage = "Number must be between {1} and {2}.")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice("[{\"Title\":\"1\",\"Name\":\"1\",\"Value\":1}," +
            "{\"Title\":\"2\",\"Name\":\"2\",\"Value\":2}," +
            "{\"Title\":\"3\",\"Name\":\"3\",\"Value\":3}," +
            "{\"Title\":\"4\",\"Name\":\"4\",\"Value\":4}," +
            "{\"Title\":\"5\",\"Name\":\"5\",\"Value\":5}," +
            "{\"Title\":\"6\",\"Name\":\"6\",\"Value\":6}," +
            "{\"Title\":\"7\",\"Name\":\"7\",\"Value\":7}," +
            "{\"Title\":\"8\",\"Name\":\"8\",\"Value\":8}," +
            "{\"Title\":\"9\",\"Name\":\"9\",\"Value\":9}," +
            "{\"Title\":\"10\",\"Name\":\"10\",\"Value\":10}," +
            "{\"Title\":\"11\",\"Name\":\"11\",\"Value\":11}," +
            "{\"Title\":\"12\",\"Name\":\"12\",\"Value\":12}]")]
        public int ColumnsNumber { get; set; }

        [DisplayName("Reverse columns on mobile")]
        [DefaultValue(false)]
        public bool InvertiColonneSuMobile { get; set; } = false;

        [DisplayName("Centered columns")]
        [DefaultValue(false)]
        public bool CentraColonne { get; set; } = false;

        [LocalizedDisplayName("Columns Gap")]
        [DefaultValue(0)]
        [Range(0, 5, ErrorMessage = "Number must be between {1} and {2}.")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice("[{\"Title\":\"No\",\"Name\":\"No\",\"Value\":0}," +
            "{\"Title\":\"1\",\"Name\":\"1\",\"Value\":1}," +
          "{\"Title\":\"2\",\"Name\":\"2\",\"Value\":2}," +
          "{\"Title\":\"3\",\"Name\":\"3\",\"Value\":3}," +
          "{\"Title\":\"4\",\"Name\":\"4\",\"Value\":4}," +
          "{\"Title\":\"5\",\"Name\":\"5\",\"Value\":5}]")]
        public int ColumnsGap { get; set; }

        [DisplayName("Preset row class")]
        [DefaultValue("")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice("[{\"Title\":\"None\",\"Name\":\"\",\"Value\":\"\"}," +
         "{\"Title\":\"Images & Titles\",\"Name\":\"Images & Titles\",\"Value\":\"imgTitleVisual\"}," +
         "{\"Title\":\"3 Columns with Image\",\"Name\":\"3 Columns with Image\",\"Value\":\"textAroundImage\"}," +
         "{\"Title\":\"Square Column\",\"Name\":\"Square Column\",\"Value\":\"squareCol\"}," +
         "{\"Title\":\"Text and Image\",\"Name\":\"Text and Image\",\"Value\":\"textAndImage\"}]")]
        public string PresetRowClass { get; set; } = "";

        [ContentSection("Columns definition")]
        [DisplayName("Spaces")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnSpaces> ColumnSpaces { get; set; } = new Dictionary<string, ColumnSpaces>();

        [ContentSection("Columns definition")]
        [DisplayName("Alignments")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnAlignments> ColumnAlignments { get; set; } = new Dictionary<string, ColumnAlignments>();

        [ContentSection("Columns definition")]
        [DisplayName("Classes")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnCustomClass> ColumnCustomClass { get; set; } = new Dictionary<string, ColumnCustomClass>();

        [ContentSection("Columns definition")]
        [DisplayName("Backgrounds")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnBackground> ColumnBackgrounds { get; set; } = new Dictionary<string, ColumnBackground>();

        [ContentSection("Columns definition")]
        [DisplayName("BackgroundImage")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnBackgroundImage> ColumnBackgroundImage { get; set; } = new Dictionary<string, ColumnBackgroundImage>();

        [ContentSection("Columns definition")]
        [DisplayName("Colors")]
        [LengthDependsOn("ColumnsNumber", "Column", "Column ")]
        public IDictionary<string, ColumnColor> ColumnColors { get; set; } = new Dictionary<string, ColumnColor>();
       
    }
}
