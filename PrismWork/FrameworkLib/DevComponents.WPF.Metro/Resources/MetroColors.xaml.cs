using System;
using System.Windows;
using System.Windows.Media;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    public partial class MetroColors : ResourceDictionary
    {
        #region Construction, Fields

        // Resource Keys for stock Colors - These colors are generated based on Canvas color and Base color.
        //
        public static readonly ComponentResourceKey CanvasColorKey = new ComponentResourceKey(typeof(MetroColors), "CanvasColorKey");
        public static readonly ComponentResourceKey TextColorKey = new ComponentResourceKey(typeof(MetroColors), "TextColorKey");
        public static readonly ComponentResourceKey TextLightColorKey = new ComponentResourceKey(typeof(MetroColors), "TextLightColorKey");
        public static readonly ComponentResourceKey TextDarkColorKey = new ComponentResourceKey(typeof(MetroColors), "TextDarkColorKey");
        public static readonly ComponentResourceKey TextInactiveColorKey = new ComponentResourceKey(typeof(MetroColors), "TextInactiveColorKey");
        public static readonly ComponentResourceKey TextDisabledColorKey = new ComponentResourceKey(typeof(MetroColors), "TextDisabledColorKey");
        public static readonly ComponentResourceKey CanvasDarkColorKey = new ComponentResourceKey(typeof(MetroColors), "CanvasDarkColorKey");
        public static readonly ComponentResourceKey CanvasLightColorKey = new ComponentResourceKey(typeof(MetroColors), "CanvasLightColorKey");
        public static readonly ComponentResourceKey CanvasLighterColorKey = new ComponentResourceKey(typeof(MetroColors), "CanvasLighterColorKey");
        public static readonly ComponentResourceKey BaseColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseColorKey");
        public static readonly ComponentResourceKey BaseTextColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseTextColorKey");
        public static readonly ComponentResourceKey BaseDarkColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseDarkColorKey");
        public static readonly ComponentResourceKey BaseDarkerColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseDarkerColorKey");
        public static readonly ComponentResourceKey BaseLightColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseLightColorKey");
        public static readonly ComponentResourceKey BaseLightTextColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseLightTextColorKey");
        public static readonly ComponentResourceKey ComplementColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementColorKey");
        public static readonly ComponentResourceKey ComplementLightColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementLightColorKey");
        public static readonly ComponentResourceKey ComplementDarkColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementDarkColorKey");
        public static readonly ComponentResourceKey ComplementDarkerColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementDarkerColorKey");
        public static readonly ComponentResourceKey ComplementTextColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementTextColorKey");
        public static readonly ComponentResourceKey ComplementLightTextColorKey = new ComponentResourceKey(typeof(MetroColors), "ComplementLightTextColorKey");
        public static readonly ComponentResourceKey CanvasGradientStartColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseGradientStartColorKey");
        public static readonly ComponentResourceKey CanvasGradientEndColorKey = new ComponentResourceKey(typeof(MetroColors), "BaseGradientEndColorKey");

        // Resource Keys for stock Brushes.
        //
        public static readonly ComponentResourceKey CanvasBrushKey = new ComponentResourceKey(typeof(MetroColors), "CanvasBrushKey");
        public static readonly ComponentResourceKey TextBrushKey = new ComponentResourceKey(typeof(MetroColors), "TextBrushKey");
        public static readonly ComponentResourceKey TextLightBrushKey = new ComponentResourceKey(typeof(MetroColors), "TextLightBrushKey");
        public static readonly ComponentResourceKey TextDarkBrushKey = new ComponentResourceKey(typeof(MetroColors), "TextDarkBrushKey");
        public static readonly ComponentResourceKey TextInactiveBrushKey = new ComponentResourceKey(typeof(MetroColors), "TextInactiveBrushKey");
        public static readonly ComponentResourceKey TextDisabledBrushKey = new ComponentResourceKey(typeof(MetroColors), "TextDisabledBrushKey");
        public static readonly ComponentResourceKey CanvasDarkBrushKey = new ComponentResourceKey(typeof(MetroColors), "CanvasDarkBrushKey");
        public static readonly ComponentResourceKey CanvasLightBrushKey = new ComponentResourceKey(typeof(MetroColors), "CanvasLightBrushKey");
        public static readonly ComponentResourceKey CanvasLighterBrushKey = new ComponentResourceKey(typeof(MetroColors), "CanvasLighterBrushKey");
        public static readonly ComponentResourceKey BaseBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseBrushKey");
        public static readonly ComponentResourceKey BaseTextBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseTextBrushKey");
        public static readonly ComponentResourceKey BaseDarkBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseDarkBrushKey");
        public static readonly ComponentResourceKey BaseDarkerBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseDarkerBrushKey");
        public static readonly ComponentResourceKey BaseLightBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseLightBrushKey");
        public static readonly ComponentResourceKey BaseLightTextBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseLightTextBrushKey");
        public static readonly ComponentResourceKey ComplementBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementBrushKey");
        public static readonly ComponentResourceKey ComplementLightBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementLightBrushKey");
        public static readonly ComponentResourceKey ComplementDarkBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementDarkBrushKey");
        public static readonly ComponentResourceKey ComplementDarkerBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementDarkerBrushKey");
        public static readonly ComponentResourceKey ComplementTextBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementTextBrushKey");
        public static readonly ComponentResourceKey ComplementLightTextBrushKey = new ComponentResourceKey(typeof(MetroColors), "ComplementLightTextBrushKey");
        public static readonly ComponentResourceKey CanvasGradientBrushKey = new ComponentResourceKey(typeof(MetroColors), "CanvasGradientBrushKey");
        public static readonly ComponentResourceKey BaseActivateGradientBrushKey = new ComponentResourceKey(typeof(MetroColors), "BaseActivateGradientBrushKey");

        public MetroColors()
        {
            InitializeComponent();
        }

        #endregion // Construction, Fields

        #region Properties

        private MetroTheme? _Theme;
        /// <summary>
        /// Setting Theme is same as calling GenerateColors(...) with MetroTheme parameter.
        /// Theme must be set for colors to be generated.
        /// </summary>
        public MetroTheme Theme
        {
            get { return _Theme.HasValue ? _Theme.Value : new MetroTheme(); }
            set 
            {
                if (!_Theme.Equals(value))
                {
                    _Theme = value;
                    GenerateColors(value.CanvasColor, value.BaseColor);
                }
            }
        }

        /// <summary>
        /// Gets or sets the base canvas color, like form background.
        /// </summary>
        public Color CanvasColor
        {
            get { return (Color)this[CanvasColorKey]; }
            set { this[CanvasColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the text color displayed over the canvas color.
        /// </summary>
        public Color TextColor
        {
            get { return (Color)this[TextColorKey]; }
            set { this[TextColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the text color displayed over the canvas lighter and light color.
        /// </summary>
        public Color TextLightColor
        {
            get { return (Color)this[TextLightColorKey]; }
            set { this[TextLightColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the text color displayed over the canvas lighter and light color.
        /// </summary>
        public Color TextDarkColor
        {
            get { return (Color)this[TextDarkColorKey]; }
            set { this[TextDarkColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the lighter text color used for example for inactive non selected tab text etc.
        /// </summary>
        public Color TextInactiveColor
        {
            get { return (Color)this[TextInactiveColorKey]; }
            set { this[TextInactiveColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the text color used for disabled text.
        /// </summary>
        public Color TextDisabledColor
        {
            get { return (Color)this[TextDisabledColorKey]; }
            set { this[TextDisabledColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the color that is in darker shade off of the canvas color.
        /// </summary>
        public Color CanvasDarkColor
        {
            get { return (Color)this[CanvasDarkColorKey]; }
            set { this[CanvasDarkColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the color that is in light shade off of the canvas color.
        /// </summary>
        public Color CanvasLightColor
        {
            get { return (Color)this[CanvasLightColorKey]; }
            set { this[CanvasLightColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the color that is in lighter shade off of the canvas color.
        /// </summary>
        public Color CanvasLighterColor
        {
            get { return (Color)this[CanvasLighterColorKey]; }
            set { this[CanvasLighterColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the chrome base color, used for window border, selection marking etc.
        /// </summary>
        public Color BaseColor
        {
            get { return (Color)this[BaseColorKey]; }
            set { this[BaseColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the text color for text displayed over the BaseColor.
        /// </summary>
        public Color BaseTextColor
        {
            get { return (Color)this[BaseTextColorKey]; }
            set { this[BaseTextColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the light base color shade.
        /// </summary>
        public Color BaseLightColor
        {
            get { return (Color)this[BaseLightColorKey]; }
            set { this[BaseLightColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the light base color shade.
        /// </summary>
        public Color BaseLightTextColor
        {
            get { return (Color)this[BaseLightTextColorKey]; }
            set { this[BaseLightTextColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the dark base color shade.
        /// </summary>
        public Color BaseDarkColor
        {
            get { return (Color)this[BaseDarkColorKey]; }
            set { this[BaseDarkColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the darker base color shade.
        /// </summary>
        public Color BaseDarkerColor
        {
            get { return (Color)this[BaseDarkerColorKey]; }
            set { this[BaseDarkerColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the complement color.
        /// </summary>
        public Color ComplementColor
        {
            get { return (Color)this[ComplementColorKey]; }
            set { this[ComplementColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the light complement color.
        /// </summary>
        public Color ComplementLightColor
        {
            get { return (Color)this[ComplementLightColorKey]; }
            set { this[ComplementLightColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the dark complement color.
        /// </summary>
        public Color ComplementDarkColor
        {
            get { return (Color)this[ComplementDarkColorKey]; }
            set { this[ComplementDarkColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the darker complement color.
        /// </summary>
        public Color ComplementDarkerColor
        {
            get { return (Color)this[ComplementDarkerColorKey]; }
            set { this[ComplementDarkerColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the light complement color.
        /// </summary>
        public Color ComplementTextColor
        {
            get { return (Color)this[ComplementTextColorKey]; }
            set { this[ComplementTextColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the light complement color.
        /// </summary>
        public Color ComplementLightTextColor
        {
            get { return (Color)this[ComplementLightTextColorKey]; }
            set { this[ComplementLightTextColorKey] = value; }
        }

        /// <summary>
        /// Gets or sets the off base color button gradient start.
        /// </summary>
        public Color CanvasGradientStartColor
        {
            get { return (Color)this[CanvasGradientStartColorKey]; }
            set { this[CanvasGradientStartColorKey] = value; }
        }
        /// <summary>
        /// Gets or sets the off base color button gradient start.
        /// </summary>
        public Color CanvasGradientEndColor
        {
            get { return (Color)this[CanvasGradientEndColorKey]; }
            set { this[CanvasGradientEndColorKey] = value; }
        }

        #endregion // Properties

        #region Public Methods

        public void GenerateColors(MetroTheme theme)
        {
            _Theme = theme;
            GenerateColors(theme.CanvasColor, theme.BaseColor);
        }

        #endregion // Public Methods

        #region Private

        private void GenerateColors(Color canvasColor, Color baseColor)
        {
            CanvasColor = canvasColor;
            BaseColor = baseColor;

            var canvasHsl = HslColor.FromRgb(canvasColor);
            var canvasHsv = HsvColor.FromRgb(canvasColor);
            var baseHsv = HsvColor.FromRgb(baseColor);
            var baseHsl = HslColor.FromRgb(baseColor);

            TextColor = (canvasHsl.L < 0.4) ? Colors.White : Colors.Black; //GetTextColor(canvasColor);
            TextInactiveColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V + 0.6 : canvasHsv.V - 0.45);
            TextDisabledColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V + 0.3 : canvasHsv.V - 0.15);
            CanvasDarkColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V + 0.5 : canvasHsv.V - 0.5);
            CanvasLightColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V + 0.15 : canvasHsv.V - 0.15);
            CanvasLighterColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V + 0.05 : canvasHsv.V - 0.05);
            TextLightColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.Black ? canvasHsv.V - 0.6 : canvasHsv.V + 0.5);
            TextDarkColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, TextColor == Colors.White ? canvasHsv.V - 0.4 : canvasHsv.V + 0.6); // GetTextColor(CanvasDarkColor); 

            BaseTextColor = GetTextColor(BaseColor); // baseHsl.L < 0.65 ? Colors.White : Colors.Black;
            BaseLightColor = HslColor.ToRgb(baseHsl.H, baseHsl.S, baseHsl.L + 0.3);
            BaseLightTextColor = GetTextColor(BaseLightColor);
            BaseDarkColor = HslColor.ToRgb(baseHsl.H, baseHsl.S, baseHsl.L - 0.1);
            BaseDarkerColor = HslColor.ToRgb(baseHsl.H, baseHsl.S, baseHsl.L - 0.2);

            ComplementColor = HsvColor.ToRgb(GetComplementHue(baseHsv.H), baseHsv.S, baseHsv.V + (baseHsv.V > 0.5 ? -0.35 : 0));
            var compHsl = HslColor.FromRgb(ComplementColor);
            var compHsv = HsvColor.FromRgb(ComplementColor);
            ComplementLightColor = HsvColor.ToRgb(compHsv.H, compHsv.S, compHsv.V + 0.2);
            ComplementDarkColor = HslColor.ToRgb(compHsl.H, compHsl.L - 0.1, compHsl.S);
            ComplementDarkerColor = HslColor.ToRgb(compHsl.H, compHsl.L - 0.2, compHsl.S);
            ComplementTextColor = GetTextColor(ComplementColor);
            ComplementLightTextColor = GetTextColor(ComplementLightColor);

            CanvasGradientStartColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, canvasHsv.V > 0 ? canvasHsv.V - 0.01 : canvasHsv.V + 0.08);
            CanvasGradientEndColor = HsvColor.ToRgb(canvasHsv.H, canvasHsv.S, canvasHsv.V > 0 ? canvasHsv.V - 0.08 : canvasHsv.V + 0.2);
        }

        private Color GetTextColor(Color backColor)
        {
            var hsl = HslColor.FromRgb(backColor);
            return hsl.L < 0.6 ? Colors.White : Colors.Black;
            //var hsv = HsvColor.FromRgb(backColor);
            //return hsv.V < 0.6 ? Colors.White : Colors.Black;
        }

        private static double OffsetHue(double hue, int offset)
        {
            hue += offset;
            if (hue > 360)
                hue -= 360;
            else if (hue < 0)
                hue += 360;
            return hue;
        }

        private static double GetComplementSaturation(double saturation)
        {
            if (saturation == 1) return 1;

            if (saturation < .81d)
                return saturation + .1d;
            if (saturation < .91d)
                return saturation + .05d;
            return saturation + (.99d - saturation);
        }

        private static double GetComplementHue(double hue)
        {
            const double DEGREE = 1d / 360;
            double hNorm = hue / 360;

            if (hNorm <= .37777d)
            {
                hNorm = Math.Min(1, hNorm + (137 * DEGREE + (86 * DEGREE * (hNorm / .37777d))));
            }
            else
            {
                hNorm = Math.Max(0, hNorm - (137 * DEGREE + (86 * DEGREE * (hNorm / .6222222d))));
            }

            return hNorm * 360;
        }

        #endregion // Private
    }
}
