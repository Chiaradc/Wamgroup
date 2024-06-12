using Progress.Sitefinity.Renderer.Designers;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow
{
   public class ColumnAlignments
   {
      [DisplayName("Horizontal Alignment")]
      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;

      [DisplayName("Vertical Alignment")]
      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;
   }
}
