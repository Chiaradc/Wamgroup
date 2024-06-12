namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupCatalog
{
    public class WamgroupCatalogViewModel
    {
        public List<WamgroupCatalogSolutionItem> SolutionsByIndustry { get; set; } = new List<WamgroupCatalogSolutionItem>();
        public List<WamgroupCatalogSolutionItem> SolutionsByNeed { get; set; } = new List<WamgroupCatalogSolutionItem>();
        public List<WamgroupCatalogBrandItem> Brands { get; set; } = new List<WamgroupCatalogBrandItem>();
        public List<WamgroupCatalogMacrofamilyItem> Macrofamilies { get; set; } = new List<WamgroupCatalogMacrofamilyItem>();
        public List<WamgroupCatalogProductItem> Products { get; set; } = new List<WamgroupCatalogProductItem>();

        public int TotalProducts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public string? ProductURL { get; set; }
    }

    public class WamgroupCatalogSolutionItem
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool IsSelected { get; set; }
    }

    public class WamgroupCatalogBrandItem
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool IsSelected { get; set; }
    }

    public class WamgroupCatalogMacrofamilyItem
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool IsSelected { get; set; }
        public List<WamgroupCatalogFamilyItem> Families { get; set; } = new List<WamgroupCatalogFamilyItem>();
    }

    public class WamgroupCatalogFamilyItem
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool IsSelected { get; set; }
    }

    public class WamgroupCatalogProductItem
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
    }
    
}
