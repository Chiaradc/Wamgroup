using Elogic.Wamgroup.Sito2023.Models.Misc;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupGeneralSlider
{
    public class WamgroupGeneralSliderEntity
   {
      

      [DisplayName("Elementi")]
      [Content(Type = Constants.DynamicTypeGeneralSliderItem, AllowMultipleItemsSelection = true)]
      public MixedContentContext? Elementi { get; set; }

      //[Category(PropertyCategory.Advanced)]
      //public int Conteggio { get; set; } = 0;

      //[DisplayName("Test Property")]
      //[TableView(Reorderable = true, Selectable = true, MultipleSelect = true)]
      //public IList<TestProperty> TestProperties { get; set; } = new List<TestProperty>();


    
   }

   //public class TestProperty
   //{
   //   public string Prop1 { get; set; } = string.Empty;
   //   public string Prop2 { get; set; } = string.Empty;
   //   public string Prop3 { get; set; } = string.Empty;
   //   public string Prop4 { get; set; } = string.Empty;
   //   public string Prop5 { get; set; } = string.Empty;
   //   public string Prop6 { get; set; } = string.Empty;
   //   public string Prop7 { get; set; } = string.Empty; 
   //   public string Prop8 { get; set; } = string.Empty;
   //   public string Prop9 { get; set; } = string.Empty;
   //   public string Prop10 { get; set; } = string.Empty;
   //   public string Prop11 { get; set; } = string.Empty;
   //   public string Prop12 { get; set; } = string.Empty;
   //   public string Prop13 { get; set; } = string.Empty;
   //   public string Prop14 { get; set; } = string.Empty;
   //   public string Prop15 { get; set; } = string.Empty;

   //}
  
}
