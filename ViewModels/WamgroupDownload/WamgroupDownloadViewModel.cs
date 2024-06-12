namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupDownload
{
   public class WamgroupDownloadViewModel
   {
      public List<DownloadableDocument> Documenti = new List<DownloadableDocument>();
      public bool MostraReadMore { get; set; }
      public bool MostrFiltro { get; set; }
      public bool MostrCategorieFiltro { get; set; }
      public bool NascondiNessunRisultato { get; set; }
      public int TopN { get; set; }
      public IEnumerable<string> Anni { get; set; } = Enumerable.Empty<string>();
      public string AnnoSelezionato { get; set; } = string.Empty;
      public IDictionary<string, string> Categorie { get; set; } = new Dictionary<string, string>();
      public string CategoriaSelezionata { get; set; } = string.Empty;
      public string CategoriaSelezionataTesto { get; set; } = string.Empty;
      public string UrlCorrente { get; set; } = string.Empty;
      public string Folder { get; set; } = string.Empty;
   }

   public class DownloadableDocument
   {
      public DateTime Date { get; set; }
      public string Description { get; set; } = string.Empty;
      public int NodeID { get; set; }
      public Dictionary<string, DownloadableFile> Files { get; set; } = new Dictionary<string, DownloadableFile>();
   }

   public class DownloadableFile
    {
        public string? DocumentCulture { get; set; }
        public string? DocumentDescription { get; set; }
        public string? FileUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}
