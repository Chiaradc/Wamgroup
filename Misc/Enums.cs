using System.ComponentModel;

namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
    public class Enums
    {
        public enum SectionBackgroundType
        {
            [Description("None")]
            None = 0,
            [Description("Color")]
            Color = 1,
            [Description("Image")]
            Image = 3
        }

        public enum SectionBackgroundImagePosition
        {
            [Description("Cover")]
            Cover = 0,
            [Description("Top")]
            Top = 1,
            [Description("Centered")]
            Centered = 3,
            [Description("Boottom")]
            Bottom = 4
        }

      public enum SectionCornersType
      {
         [Description("None")]
         None = 0,
         [Description("Left to Right")]
         LeftToRight = 1,
         [Description("Right to Left")]
         RightToLeft = 2
      }

      public enum HorizontalAlignment
      {
         [Description("Left")]
         Left = 0,
         [Description("Center")]
         Center = 1,
         [Description("Right")]
         Right = 2
      }
      
      public enum VerticalAlignment
      {
         [Description("Top")]
         Top = 0,
         [Description("Center")]
         Center = 1,
         [Description("Bottom")]
         Bottom = 2
      }

      public enum SpacerMargin
      {
         [Description("0x")]
         None = 0,
         [Description("1x")]
         Small = 1,
         [Description("2x")]
         Medium = 2,
         [Description("3x")]
         Large = 3
      }

      public enum ColumnSpace
      {
         [Description("1")]
         Space1 = 1,
         [Description("2")]
         Space2 = 2,
         [Description("3")]
         Space3 = 3,
         [Description("4")]
         Space4 = 4,
         [Description("5")]
         Space5 = 5,
         [Description("6")]
         Space6 = 6,
         [Description("7")]
         Space7 = 7,
         [Description("8")]
         Space8 = 8,
         [Description("9")]
         Space9 = 9,
         [Description("10")]
         Space10 = 10,
         [Description("11")]
         Space11 = 11,
         [Description("12")]
         Space12 = 12,
         [Description("Hide")]
         Hide = 0
      }

      public enum TypeVideo
      {
         [Description("Youtube")]
         Youtube = 1,
         [Description("Vimeo")]
         Vimeo = 2
      }

      public enum TipoVisualizzazioneGeneralSlider
      {
         [Description("Verticale")]
         Verticale = 1,
         [Description("Orizzontale")]
         Orizzontale = 2
      }

    public enum CtaType
    {
        [Description("Testo")]
        Text = 0,
        [Description("Card")]
        Card = 1,
        [Description("Lista")]
        List = 3
    }
        public enum CtaTypeText
        {
            [Description("Sinistra")]
            Sx = 0,
            [Description("Destra")]
            Dx = 1,
          
        }
    }
}
