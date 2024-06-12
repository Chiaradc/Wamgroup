namespace Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults
{
   public class SearchResultDocumentDto
   {
      /// <summary>
      /// Gets or sets the highlighter result.
      /// </summary>
      public string HighLighterResult { get; set; }

      /// <summary>
      /// Gets or sets the language.
      /// </summary>
      public string Language { get; set; }

      /// <summary>
      /// Gets or sets the provider.
      /// </summary>
      public string Provider { get; set; }

      /// <summary>
      /// Gets or sets the link.
      /// </summary>
      public string Link { get; set; }

      /// <summary>
      /// Gets or sets the title.
      /// </summary>
      public string Title { get; set; }

      /// <summary>
      /// Gets or sets the content type.
      /// </summary>
      public string ContentType { get; set; }

      /// <summary>
      /// Gets or sets the item's original id.
      /// </summary>
      public string Id { get; set; }

      /// <summary>
      /// Gets or sets the url of the item's thumbnail.
      /// </summary>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Used on the front end.")]
      public string ThumbnailUrl { get; set; }
   }
}
