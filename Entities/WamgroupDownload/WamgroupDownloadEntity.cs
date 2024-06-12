using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupDownload
{
   public class WamgroupDownloadEntity
   {

      [DisplayName("Numero massimo di documenti da mostrare")]
      public int TopN { get; set; }
      
      [DisplayName("Cartella")]
      [Content(Type = KnownContentTypes.DocumentLibraries, AllowMultipleItemsSelection = false)]
      public MixedContentContext? PagePaths { get; set; }
      
      [DisplayName("Mostra solo i documenti correlati con il documento corrente")] 
      public bool MostraRelazioniConDocumentoCorrente { get; set; }
      
      [DisplayName("Mostra il read more")]
      public bool MostraReadMore { get; set; }
      
      [DisplayName("Mostra il filtro")]
      public bool MostrFiltro { get; set; }
      
      [DisplayName("Mostra le categorie nel filtro")]
      public bool MostrCategorieFiltro { get; set; }
      
      [DisplayName("Nascondi il testo per nessun risultato")]
      public bool NascondiNessunRisultato { get; set; }

   }
}
