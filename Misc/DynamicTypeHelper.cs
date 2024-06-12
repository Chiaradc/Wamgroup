using Elogic.Wamgroup.Sito2023.Models.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Telerik.Sitefinity.Model.Localization;

namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
   public class DynamicTypeHelper
   {
      public class DynamicTypeMap
      {
         public string? Namespace { get; set; }
         public string? Singular { get; set; }
         public string? Plural { get; set; }
         public string? IndexCatalog { get; set; }

         public DynamicTypeMap(string ns, string singular, string plural, string indexCatalog)
         {
            Namespace = ns;
            Singular = singular;
            Plural = plural;
            IndexCatalog = indexCatalog;
         }

         public DynamicTypeMap()
         {

         }
      }

      private readonly LocalizationHelperService localizationHelper;

      public DynamicTypeMap DynamicTypeCustomNewsItem = new DynamicTypeMap(Constants.DynamicTypeCustomNewsItem, "News", "News", "index-news");
      public DynamicTypeMap DynamicTypeNewsItem = new DynamicTypeMap(Constants.DynamicTypeNewsItem, "News", "News", "index-news");
      public DynamicTypeMap DynamicTypeBlogPost = new DynamicTypeMap(Constants.DynamicTypeBlogPost, "Blog Post", "Blog Posts", "index-blogpost");
      public DynamicTypeMap DynamicTypeColorSchema = new DynamicTypeMap(Constants.DynamicTypeColorSchema, "Color Schema", "Color Schemas", "index-colorschema");
      public DynamicTypeMap DynamicTypeGalleryItem = new DynamicTypeMap(Constants.DynamicTypeGalleryItem, "Gallery Item", "Gallery Items", "index-galleryitems");
      public DynamicTypeMap DynamicTypeGeneralSliderItem = new DynamicTypeMap(Constants.DynamicTypeGeneralSliderItem, "General Slider Item", "General Slider Item", "index-generalslider");
      public DynamicTypeMap DynamicTypeTimelineItem = new DynamicTypeMap(Constants.DynamicTypeTimelineItem, "Timeline Item", "Timeline Items", "index-timelineitems");
      public DynamicTypeMap DynamicTypePage = new DynamicTypeMap(Constants.DynamicTypePage, "Page", "Pages", "index-pages");
      public DynamicTypeMap DynamicTypeImage = new DynamicTypeMap(Constants.DynamicTypeImage, "Image", "Images", "index-images");
      public DynamicTypeMap DynamicTypeVideo = new DynamicTypeMap(Constants.DynamicTypeVideo, "Video", "Videos", "index-video");
      public DynamicTypeMap DynamicTypeDocument = new DynamicTypeMap(Constants.DynamicTypeDocument, "Document", "Documents", "index-document");
      public DynamicTypeMap DynamicTypeMacroFamily = new DynamicTypeMap(Constants.DynamicTypeMacroFamily, "Macro Family", "Macro Families", "index-macro-family");
      public List<DynamicTypeMap> DynamicTypes = new List<DynamicTypeMap>();

      public DynamicTypeHelper(LocalizationHelperService localizationHelper)
      {
         DynamicTypes.Add(DynamicTypeCustomNewsItem);
         DynamicTypes.Add(DynamicTypeNewsItem);
         DynamicTypes.Add(DynamicTypeBlogPost);
         DynamicTypes.Add(DynamicTypeColorSchema);
         DynamicTypes.Add(DynamicTypeGalleryItem);
         DynamicTypes.Add(DynamicTypeGeneralSliderItem);
         DynamicTypes.Add(DynamicTypeTimelineItem);
         DynamicTypes.Add(DynamicTypePage);
         DynamicTypes.Add(DynamicTypeImage);
         DynamicTypes.Add(DynamicTypeVideo);
         DynamicTypes.Add(DynamicTypeDocument);
         DynamicTypes.Add(DynamicTypeMacroFamily);
         this.localizationHelper = localizationHelper;
      }

      public DynamicTypeMap? GetDynamicType(string ns)
      {
         
         var sel = DynamicTypes.FirstOrDefault(x => x.Namespace == ns) ?? new DynamicTypeMap(ns, ns, ns, "");

         return new DynamicTypeMap(sel.Namespace ?? "", localizationHelper.Localize(sel.Singular ?? ""), localizationHelper.Localize(sel.Plural ?? ""), sel.IndexCatalog ?? "");
      }
   }
}