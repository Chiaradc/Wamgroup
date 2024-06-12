using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.Renderer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Elogic.Wamgroup.Sito2023.NetCore.Misc.Enums;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupSection
{
   public class WamgroupSectionEntity
   {
      [DisplayName("Full width")]
      [Description("Check this option if you want the section displayed without left/right margins.")]
      public bool FullWidth { get; set; }

      [DisplayName("Display on desktop")]
      [DefaultValue(true)]
      public bool DisplayOnDesktop { get; set; } = true;

      [DisplayName("Display on tablet")]
      [DefaultValue(true)]
      public bool DisplayOnTablet { get; set; } = true;

      [DisplayName("Display on mobile")]
      [DefaultValue(true)]
      public bool DisplayOnMobile { get; set; } = true;

      [DisplayName("Text color")]
      [ColorPalette("WamgroupColors")]
      public string TextColor { get; set; } = string.Empty;

      [DisplayName("Background type")]
      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      public SectionBackgroundType BackgroundType { get; set; } = SectionBackgroundType.None;

      [DisplayName("Color")]
      [ContentSection("Background definition")]
      [ColorPalette("WamgroupBackground")]
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Color\"}]}")]
      public string BackgroundColor { get; set; } = string.Empty;

      [DisplayName("Desktop image")]
      [ContentSection("Background definition")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false, AllowCreate = false)]
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
      public MixedContentContext BackgroundDesktopImage { get; set; } = new MixedContentContext();

      [DisplayName("Tablet image")]
      [ContentSection("Background definition")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false, AllowCreate = false)]
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
      public MixedContentContext BackgroundTabletImage { get; set; } = new MixedContentContext();

      [DisplayName("Mobile image")]
      [ContentSection("Background definition")]
      [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = false, AllowCreate = false)]
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
      public MixedContentContext BackgroundMobileImage { get; set; } = new MixedContentContext();

      [DisplayName("Background position")]
      [ContentSection("Background definition")]
      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
      public SectionBackgroundImagePosition BackgroundImagePosition { get; set; } = SectionBackgroundImagePosition.Cover;

      [DisplayName("Display Corners")]
      [ContentSection("Background definition")]
      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      public SectionCornersType CornersType { get; set; } = SectionCornersType.None;

      
        [DataType(customDataType: "linkSelector")]
        public LinkModel Link { get; set; } = new LinkModel();
    }
}
