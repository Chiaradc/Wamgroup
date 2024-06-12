using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupFiltroSettori
{
   public class WamgroupFiltroSettoriEntity
   {
      public IEnumerable<WamgroupFiltroSettoriItem> Settori { get; set; } = Enumerable.Empty<WamgroupFiltroSettoriItem>();

      [DisplayName("Mostra pulsante conferma")]
      public bool MostraPulsanteConferma { get; set; } = true;
    }

   public class WamgroupFiltroSettoriItem
   {
      [DataType(customDataType: KnownFieldTypes.Choices)]
      [DefaultValue("")]
      [Choice("[{\"Title\":\"\",\"Name\":\"\",\"Value\":\"\"}," +
         "{\"Title\":\"Buiding & Construction\",\"Name\":\"Buiding & Construction\",\"Value\":\"0\"}," +
         "{\"Title\":\"Feed & Food\",\"Name\":\"Feed & Food\",\"Value\":\"1\"}," +
         "{\"Title\":\"Plastics & Chemicals\",\"Name\":\"Plastics & Chemicals\",\"Value\":\"2\"}," +
         "{\"Title\":\"Heavy Industries\",\"Name\":\"Heavy Industries\",\"Value\":\"3\"}," +
         "{\"Title\":\"Renewable Energy\",\"Name\":\"Renewable Energy\",\"Value\":\"4\"}," +
         "{\"Title\":\"Plants & Machinery\",\"Name\":\"Plants & Machinery\",\"Value\":\"5\"}," +
         "{\"Title\":\"Environmental\",\"Name\":\"Environmental\",\"Value\":\"6\"}," +
         "{\"Title\":\"Miscellaneous\",\"Name\":\"Miscellaneous\",\"Value\":\"7\"}]")]
      [Required(ErrorMessage = "Valore richiesto.")]
      public string Settore { get; set; } = "";

      [DataType(customDataType: KnownFieldTypes.ChipChoice)]
      [DefaultValue("Selezione")]
      [Required(ErrorMessage = "Valore richiesto.")]
      [Choice("[{\"Title\":\"Selezione\",\"Name\":\"Selezione\",\"Value\":\"Selezione\"}," +
         "{\"Title\":\"Link\",\"Name\":\"Link\",\"Value\":\"Link\"}]")]
      public string Azione { get; set; } = "Selezione";

      [ConditionalVisibilityAttribute("{\"conditions\":[{\"fieldName\":\"Azione\",\"operator\":\"Equals\",\"value\":\"url\"}]}")]
      [DataType(customDataType: "linkSelector")]
      public LinkModel Link { get; set; } = new LinkModel();
   }
}
