using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupRow;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupRow
{
    public class WamgroupRowViewModel
    {
        public string PresetRowClass { get; set; }
        public int ColumnsNumber { get; set; }
        public int ColumnGap { get; set; }
        public Dictionary<string, WamgroupRowColumnViewModel> ColumnDefinitions{ get; set; }
        public ICompositeViewComponentContext Context { get; set; }
    }

    public class WamgroupRowColumnViewModel
    {
        public string ColumnOuterClasses{ get; set; }
        public string ColumnClasses{ get; set; }
        public string ColumnStyles { get; set; }
        public string ColumnDesktopImageUrl { get; set; }
        public string ColumnTabletImageUrl { get; set; }
        public string ColumnMobileImageUrl { get; set; }
        public string ColumnImageAlt { get; set; }
    }   
}
