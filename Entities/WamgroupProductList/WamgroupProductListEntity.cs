using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.Renderer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupProductList
{
   public class WamgroupProductListEntity
   {
      [DisplayName("Title")]
      public string? Title { get; set; }

      [DisplayName("Max number of product displayed")]
      public int? MaxNumber{ get; set; }

      [DisplayName("Tecnologies")]
      [Content(Type = "Taxonomy_tecnologies", AllowMultipleItemsSelection = true)]
      public MixedContentContext? Tecnologies { get; set; }

      [DisplayName("Solutions")]
      [Content(Type = "Taxonomy_solutions", AllowMultipleItemsSelection = true)]
      public MixedContentContext? Solutions { get; set; }

      [DisplayName("Link product detail")]
      [DataType(customDataType: "linkSelector")]
      public LinkModel LinkProductDetail { get; set; } = new LinkModel();
   }
}
