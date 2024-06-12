using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupWidget
{
    public class WamgroupWidgetEntity
    {
        [Description("Codice")]
        public string? Codice { get; set; }
    }
}
