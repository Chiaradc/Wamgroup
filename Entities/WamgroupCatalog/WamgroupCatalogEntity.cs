using Progress.Sitefinity.Renderer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupCatalog
{
    public class WamgroupCatalogEntity
    {
        [DisplayName("Link product detail")]
        [DataType(customDataType: "linkSelector")]
        public LinkModel LinkProductDetail { get; set; } = new LinkModel();
    }
}
