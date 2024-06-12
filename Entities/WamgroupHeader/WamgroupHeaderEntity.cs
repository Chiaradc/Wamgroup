using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupHeader
{
    public class WamgroupHeaderEntity
    {
        [DisplayName("Login page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext? LoginPage { get; set; }
    }
}
