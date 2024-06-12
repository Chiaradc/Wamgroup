namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
    /// <summary>
    /// Clase helper per la gestione dei colori
    /// </summary>
    public class ColorsHelper
    {
        Dictionary<string, string> colorsDefinition = new Dictionary<string, string>();

        public ColorsHelper()
        {
            colorsDefinition.Add("FFFFFF", "Bianco");
            colorsDefinition.Add("FFEE00", "Giallo");
            colorsDefinition.Add("5A96BF", "BluChiaro");
            colorsDefinition.Add("1C3F69", "Blu");
            colorsDefinition.Add("262D36", "BluScuro");
            colorsDefinition.Add("000000", "Nero");
        }

        /// <summary>
        /// Metodo per ottenere la classe di colore di sfondo a partire da un colore esadecimale
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="colorClass"></param>
        /// <returns></returns>
        public bool ParseBackgroundColorFromHEX(string hex, out string colorClass)
        {
            bool retValue = false;
            colorClass = string.Empty;
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);
            if (colorsDefinition.ContainsKey(hex))
            {
                colorClass = $"bg{colorsDefinition[hex]}";
                retValue = true;
            }
            return retValue;
        }

        /// <summary>
        /// Metodo per ottenere la classe di colore del testo a partire da un colore esadecimale
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="colorClass"></param>
        /// <returns></returns>
        public bool ParseTextColorFromHEX(string hex, out string colorClass)
        {
            bool retValue = false;
            colorClass = string.Empty;
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);
            if (colorsDefinition.ContainsKey(hex))
            {
                colorClass = $"txt{colorsDefinition[hex]}";
                retValue = true;
            }
            return retValue;
        }
    }
}
