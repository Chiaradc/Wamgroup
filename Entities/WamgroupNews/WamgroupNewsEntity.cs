using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.Renderer.Models;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNews
{
   public class WamgroupNewsEntity
   {
    [DisplayName("Titolo sezione")]
    public string? TitleSection { get; set; }

      [DisplayName("Numero massimo di news da mostrare")]
      public int TopN { get; set; }

      [DisplayName("Mostra read more")]
      [DefaultValue(true)]
      public bool MostraReadMore { get; set;}

      [DisplayName("Link read more")]
      [DataType(customDataType: "linkSelector")]  
      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"MostraReadMore\",\"operator\":\"Equals\",\"value\":\"true\"}]}")]
      public LinkModel LinkReadMore { get; set; } = new LinkModel();

      [DisplayName("Link dettaglio news")]
      [DataType(customDataType: "linkSelector")]
      public LinkModel LinkDettaglioNews { get; set; } = new LinkModel();

      [DisplayName("Selettore dell'elemento da nascondere in caso di mancanza di news")]
      public string? HideElementSelector { get; set; }
     
      [DisplayName("Tipo Presentazione")]
      [DefaultValue("slider")]
      [DataType(customDataType: KnownFieldTypes.Choices)]
      [Choice("[{\"Title\":\"Slider\",\"Name\":\"Slider\",\"Value\":\"slider\"}," +
         "{\"Title\":\"Lista\",\"Name\":\"Lista\",\"Value\":\"lista\"}]")]
      public string? TipoPresentazione { get; set; } = "slider";
   }
}
