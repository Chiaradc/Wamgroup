namespace Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupNews
{
   public class WamgroupNewsViewModel
   {
      public List<WamgroupNewsItemViewModel> News = new List<WamgroupNewsItemViewModel>();
      public string LinkDiscoverMore { get; set; } = string.Empty;
      public string TestoLinkDiscoverMore { get; set; } = string.Empty;
      public string TargetLinkDiscoverMore { get; set; } = string.Empty;
      public string LinkDettaglioNews { get; set; } = string.Empty;
      public string TestoLinkDettaglioNews { get; set; } = string.Empty;
      public string TargetLinkDettaglioNews { get; set; } = string.Empty;
      public string SelettorePerNascondereElemento { get; set; } = string.Empty;
      public bool MostraReadMore { get; set; } = true;
      public int TopN { get; set; }
      public string TipoPresentazione { get; set; } = "slider";
        public string TitleSection { get; set; } = string.Empty;
    }

   public class WamgroupNewsItemViewModel
   {
      public string Titolo { get; set; } = string.Empty;
      public string DataStringa { get; set; } = string.Empty;
      public IEnumerable<string> Categorie { get; set; } = Enumerable.Empty<string>();
      public string ImmagineLista { get; set; } = string.Empty;
      public string AltImmagineLista { get; set; } = string.Empty;
      public string Intro { get; set; } = string.Empty;
      public string Testo { get; set; } = string.Empty;
      public int WidthLista { get; set; }
      public int HeightLista { get; set; }
      public string Url { get; set; } = string.Empty;
      public string PrevUrl { get; set; } = string.Empty;
      public string NextUrl { get; set; } = string.Empty;

      public string ImmagineDettaglio { get; set; } = string.Empty;
      public string AltImmagineDettaglio { get; set; } = string.Empty; 
      public int WidthDettaglio { get; set; }
      public int HeightDettaglio { get; set; }
      public string Type { get; set; } = string.Empty;
      public DateTime Date { get; set; } = DateTime.MinValue;
      public bool Highlighted { get; set; }
      public string Category { get; set; } = string.Empty;
    }
}
