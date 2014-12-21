using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.ComponentModel;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// A wrapper for the Canvas and Base colors used in generating Metro colors, with a display name for UI.
    /// </summary>
    public struct MetroTheme
    {
        private Color _BaseColor;
        private Color _CanvasColor;
        private string _DisplayName;

        /// <summary>
        /// Construct using standard color strings for defining canvas and base colors.
        /// </summary>
        public MetroTheme(string canvasColorString, string baseColorString, string displayName)
            : this((Color)ColorConverter.ConvertFromString(canvasColorString), (Color)ColorConverter.ConvertFromString(baseColorString), displayName)
        {
        }

        /// <summary>
        /// Construct with canvas and base colors along with the display name for this theme.
        /// </summary>
        public MetroTheme(Color canvasColor, Color baseColor, string displayName)
        {
            _BaseColor = baseColor;
            _CanvasColor = canvasColor;
            _DisplayName = displayName;
        }

        /// <summary>
        /// Access the display name.
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        /// <summary>
        /// Gets or sets the "Base" color used to generate Metro colors.
        /// </summary>
        public Color BaseColor
        { 
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        /// <summary>
        /// Gets or sets the "Canvas" color used to generate Metro colors.
        /// </summary>
        public Color CanvasColor 
        { 
            get { return _CanvasColor; }
            set { _CanvasColor = value; }
        }

        /// <summary>
        /// Returns true of both BaseColor and CanvasColor are the same.
        /// </summary>
        public override bool Equals(object obj)
        {
            if(!(obj is MetroTheme))
                return false;

            var other = (MetroTheme)obj;
            return other.BaseColor.Equals(BaseColor) && other.CanvasColor.Equals(CanvasColor);
        }

        /// <summary>
        /// Returns hash code based on BaseColor and CanvasColor combined.
        /// </summary>
        public override int GetHashCode()
        {
            return BaseColor.GetHashCode() * CanvasColor.GetHashCode();
        }

        private static List<MetroTheme> _PreDefinedThemes;

        /// <summary>
        /// Return a list of the pre-defined MetroThemes
        /// </summary>
        public static List<MetroTheme> PreDefinedThemes
        {
            get
            {
                if (_PreDefinedThemes == null)
                {
                    _PreDefinedThemes = new List<MetroTheme>
                    {
                        Default, BlackClouds, BlackLilac, BlackMaroon, BlackSky, BlueishBrown, Bordeaux, Brown, Cherry, DarkBrown, DarkRed,
                        EarlyMaroon, EarlyOrange, EarlyRed, Espresso, GrayOrange, Latte, LatteDarkSteel, LatteRed, MaroonSilver, NapaRed,
                        Orange, PowderRed, RetroBlue, Rust, SilverBlues, SilverGreen, SimplyBlue, SkyGreen, WashedBlue, WashedWhite, Office2013LightGray, Office2013DarkGray
                    };
                }
                return _PreDefinedThemes;
            }
        }

        public static string NullThemeDisplayName = " ";

        public static MetroTheme Default { get { return new MetroTheme("#FFFFFF", "#FFA31A", "Default"); } }
        public static MetroTheme BlackClouds { get { return new MetroTheme("#000000", "#9B96A3", "Black Clouds"); } }
        public static MetroTheme BlackLilac { get { return new MetroTheme("#191919", "#D8CAFF", "Black Lilac"); } }
        public static MetroTheme BlackMaroon { get { return new MetroTheme("#1C190F", "#8A2F25", "Black Maroon"); } }
        public static MetroTheme BlackSky { get { return new MetroTheme("#000000", "#00A7FF", "Black Sky"); } }
        public static MetroTheme BlueishBrown { get { return new MetroTheme("#D5D5E0", "#3D1429", "Blueish Brown"); } }
        public static MetroTheme Bordeaux { get { return new MetroTheme("#F2EBDC", "#960E00", "Bordeaux"); } }
        public static MetroTheme Brown { get { return new MetroTheme("#E7DEC0", "#6D4320", "Brown"); } }
        public static MetroTheme Cherry { get { return new MetroTheme("#DECDAE", "#5A0D12", "Cherry"); } }
        public static MetroTheme DarkBrown { get { return new MetroTheme("#D9B573", "#261105", "Dark Brown"); } }
        public static MetroTheme DarkRed { get { return new MetroTheme("#F2F2F2", "#8D1D21", "Dark Red"); } }
        public static MetroTheme EarlyMaroon { get { return new MetroTheme("#FCFCF0", "#730046", "Early Maroon"); } }
        public static MetroTheme EarlyOrange { get { return new MetroTheme("#FCFCF0", "#E88801", "Early Orange"); } }
        public static MetroTheme EarlyRed { get { return new MetroTheme("#FCFCF0", "#C93C00", "Early Red"); } }
        public static MetroTheme Espresso { get { return new MetroTheme("#AB9667", "#383025", "Espresso"); } }
        public static MetroTheme GrayOrange { get { return new MetroTheme("#E8E8DC", "#FF8600", "Gray Orange"); } }
        public static MetroTheme Latte { get { return new MetroTheme("#B18D5A", "#851A1D", "Latte"); } }
        public static MetroTheme LatteDarkSteel { get { return new MetroTheme("#A77A51", "#2C2D40", "Latte Dark Steel"); } }
        public static MetroTheme LatteRed { get { return new MetroTheme("#A77A51", "#8C170D", "Latte Red"); } }
        public static MetroTheme MaroonSilver { get { return new MetroTheme("#260B01", "#F2F2F2", "Maroon Silver"); } }
        public static MetroTheme NapaRed { get { return new MetroTheme("#F2F2F2", "#A64B29", "Napa Red"); } }
        public static MetroTheme Orange { get { return new MetroTheme("#FFFFFF", "#FFB90A", "Orange"); } }
        public static MetroTheme PowderRed { get { return new MetroTheme("#D0D9D6", "#BF0404", "Powder Red"); } }
        public static MetroTheme RetroBlue { get { return new MetroTheme("#FEFEFE", "#3E788F", "Retro Blue"); } }
        public static MetroTheme Rust { get { return new MetroTheme("#B4B48D", "#A94D2D", "Rust"); } }
        public static MetroTheme SilverBlues { get { return new MetroTheme("#dcdcdc", "#002395", "Silver Blues"); } }
        public static MetroTheme SilverGreen { get { return new MetroTheme("#F2F2F2", "#588C3F", "Silver Green"); } }
        public static MetroTheme SimplyBlue { get { return new MetroTheme("#E2E2E2", "#215BA6", "Simply Blue"); } }
        public static MetroTheme SkyGreen { get { return new MetroTheme("#BDD5D0", "#04ADBF", "Sky Green"); } }
        public static MetroTheme WashedBlue { get { return new MetroTheme("#B8CDD1", "#B4090A", "Washed Blue"); } }
        public static MetroTheme WashedWhite { get { return new MetroTheme("#FEFFF8", "#92AAB1", "Washed White"); } }
        public static MetroTheme NullTheme { get { return new MetroTheme("#00000000", "#00000000", NullThemeDisplayName); } }
        public static MetroTheme Office2013LightGray { get { return new MetroTheme("#F6F6F6", "#19478A", "Office 2013 Light Gray"); } }
        public static MetroTheme Office2013DarkGray { get { return new MetroTheme("#E5E5E5", "#2B579A", "Office 2013 Dark Gray"); } }
    }
}
