using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSection;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupSection
{
    public class WamgroupSectionViewModel
    {
        public bool FullWidth { get;set; }        
        public string SectionClasses { get; set; }
        public string SectionStyle { get; set; }
        public bool BackgroundIsImage { get; set; }
        public string BackgroundDestktopImageUrl { get; set; }
        public string BackgroundTabletImageUrl { get; set; }
        public string BackgroundMobileImageUrl { get; set; }
        public string BackgroundImageClasses { get; set; }
        public string BackgroundImageAlternativeText { get; set; }
        public string BackgroundImageWidth { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Link { get; set; } = string.Empty;

        public ICompositeViewComponentContext Context { get; set; }
    }
}
