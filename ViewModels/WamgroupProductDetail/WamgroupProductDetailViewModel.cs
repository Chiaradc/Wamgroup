using Telerik.Sitefinity.Publishing.Configuration;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupProductDetail
{
    public class WamgroupProductDetailViewModel
    {
        public string? Title { get; set; }
        public List<string>? AvailableInSite { get; set; } = new List<string>();
        public int? Gravity { get; set; }
        public List<string>? Family { get; set; } = new List<string>();
        public List<string>? MacroFamily { get; set; } = new List<string>();
        public List<string>? FamilyProperty { get; set; } = new List<string>();
        public List<string>? MicroFamily { get; set; } = new List<string>();
        public List<string>? Brand { get; set; } = new List<string>();
        public string? Code { get; set; }
        public string? TitleOrder { get; set; }
        public string? Subtitle { get; set; }
        public string? Presentation { get; set; }
        public string? Description { get; set; }
        public string? DescriptionImage { get; set; }
        public string? Function { get; set; }
        public string? Features { get; set; }
        public string? Benefits { get; set; }
        public string? Options { get; set; }
        public string? Accessories { get; set; }
        public string? CommercialData { get; set; }
        public string? TechnicalTable { get; set; }
        public List<string>? TechnicalDrawing { get; set; } = new List<string>();
        public string[]? brands { get; set; } = new string[0];
        public string[]? families { get; set; } = new string[0];
        public string[]? industries { get; set; } = new string[0];
        public string[]? macrofamilies { get; set; } = new string[0];
        public string[]? tecnologies { get; set; } = new string[0];
        public string[]? solutionbyindustry { get; set; } = new string[0];
        public string[]? solutionbyneed { get; set; } = new string[0];
        public string[]? solutions { get; set; } = new string[0];

        public List<WamgroupProductDetailVisualiSlide>? VisualiSlides { get; set; } = new List<WamgroupProductDetailVisualiSlide>();
        public List<WamgroupProductDetailMultimedia>? Multimedia { get; set; } = new List<WamgroupProductDetailMultimedia>();
        public WamgroupProductDetailMultimedia? MainImage { get; set; } = new WamgroupProductDetailMultimedia();
        public List<WamgroupProductDetailSpotBox>? SpotBoxes { get; set; } = new List<WamgroupProductDetailSpotBox>();
        public List<WamgroupProductDetailLinkedProduct>? LinkedProducts { get; set; } = new List<WamgroupProductDetailLinkedProduct>();
        public List<WamgroupProductDetailDocument>? Documents { get; set; } = new List<WamgroupProductDetailDocument>();
        public List<WamgroupProductDetailLanguage>? DocumentsLanguages { get; set; } = new List<WamgroupProductDetailLanguage>();
    }

    public class WamgroupProductDetailVisualiSlide
    {
        public string? Title { get; set; }
        public string? URL { get; set; }
    }

    public class WamgroupProductDetailMultimedia
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoEmbedCode { get; set; }
        public string? VideoTeaserUrl { get; set; }
    }

    public class WamgroupProductDetailSpotBox
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class WamgroupProductDetailLinkedProduct
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class WamgroupProductDetailDocument
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public DateTime? RefDate { get; set; }
        public string? Code { get; set; }
        public string? Languages { get; set; }
        public List<WamgroupProductDetailLanguage>? ListLanguages { get; set; } = new List<WamgroupProductDetailLanguage>();
        public string? URL { get; set; }
        public string? Issue { get; set; }
    }

    public class WamgroupProductDetailLanguage
    {
        public string? DisplayName { get; set; }
        public string? Code { get; set; }
        
    }
}
