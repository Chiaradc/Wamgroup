using static Telerik.Sitefinity.Data.Linq.Basic.QueryArgs;
using System.Reflection.Emit;
using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel.DataAnnotations;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSpacer
{
    public class WamgroupSpacerEntity
    {
        [DisplayName("Spazio superiore")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DefaultValue(SpacerMargin.Small)]
        public SpacerMargin AboveMargin { get; set; } = SpacerMargin.Small;

        [DisplayName("Spazio inferiore")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DefaultValue(SpacerMargin.Small)]
        public SpacerMargin BelowMargin { get; set; } = SpacerMargin.Small;

        [DisplayName("Inserisci linea verticale")]
        public bool AddVerticalLine { get; set; }
    }
}
