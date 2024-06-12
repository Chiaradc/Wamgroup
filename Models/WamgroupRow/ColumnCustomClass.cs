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
   public class ColumnCustomClass
   {


      [Group("Column Custom Class")]
      [DisplayName("Square")]
      [DataType(customDataType: KnownFieldTypes.CheckBox)]
      public bool ClassSquare { get; set; }

   
   }
}
