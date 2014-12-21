using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    [DesignTimeVisible(true)]
    public class MetroTileFrame : ContentControl
    {
        static MetroTileFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTileFrame), new FrameworkPropertyMetadata(typeof(MetroTileFrame)));
        }

        // Using a DependencyProperty as the backing store for DisplayDuration.
        public static readonly DependencyProperty DisplayDurationProperty =
            DependencyProperty.Register("DisplayDuration", typeof(Duration), typeof(MetroTileFrame), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(5))));
        public Duration DisplayDuration
        {
            get { return (Duration)GetValue(DisplayDurationProperty); }
            set { SetValue(DisplayDurationProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ImageSource.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MetroTileFrame), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or Set the image source for the image displayed by the Tile.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The image source for the image displayed by the Tile")]
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetCurrentValue(ImageSourceProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TileColor.
        /// </summary>
        public static readonly DependencyProperty TileColorProperty =
            DependencyProperty.Register("TileColor", typeof(MetroTileColor), typeof(MetroTileFrame), new UIPropertyMetadata(MetroTileColor.Default,
                (s, e) =>
                {
                    ((Control)s).Background = GetBackgroundBrushFromTileColor((MetroTileColor)e.NewValue);
                }));
        /// <summary>
        /// Causes Background to be set with pre-defined Brush which maps to the enumerated value.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Pre-defined Background brush.")]
        public MetroTileColor TileColor
        {
            get { return (MetroTileColor)GetValue(TileColorProperty); }
            set { SetCurrentValue(TileColorProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Title.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(MetroTileFrame), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or Set the title of the Tile.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The Tile's title.")]
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetCurrentValue(TitleProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TitleTemplate.
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(MetroTileFrame), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or Set a DataTemplate which shows how to render the Title.
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetCurrentValue(TitleTemplateProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (BackgroundProperty.IsUnsetValue(this))
                Background = GetBackgroundBrushFromTileColor(TileColor);
        }

        internal static Brush GetBackgroundBrushFromTileColor(MetroTileColor tileColor)
        {
            Color color1, color2;

            if (tileColor == MetroTileColor.None)
                return null;

            if (tileColor == MetroTileColor.Blue)
            {
                color1 = GetColor(0x4C66A8);
                color2 = GetColor(0x5B78BE);
            }
            else if (tileColor == MetroTileColor.Green)
            {
                color1 = GetColor(0x7D972A);
                color2 = GetColor(0x98B133);
            }
            else if (tileColor == MetroTileColor.Magenta)
            {
                color1 = GetColor(0x765594);
                color2 = GetColor(0x8562B9);
            }
            else if (tileColor == MetroTileColor.Orange)
            {
                color1 = GetColor(0xDF8300);
                color2 = GetColor(0xE88800);
            }
            else if (tileColor == MetroTileColor.Plum)
            {
                color1 = GetColor(0x6D3453);
                color2 = GetColor(0x884371);
            }
            else if (tileColor == MetroTileColor.Teal)
            {
                color1 = GetColor(0x45A98E);
                color2 = GetColor(0x50BB9E);
            }
            else if (tileColor == MetroTileColor.Coffee)
            {
                color1 = GetColor(0x734C29);
                color2 = GetColor(0x664325);
            }
            else if (tileColor == MetroTileColor.RedOrange)
            {
                color1 = GetColor(0xC93C00);
                color2 = GetColor(0xBD3900);
            }
            else if (tileColor == MetroTileColor.RedViolet)
            {
                color1 = GetColor(0x730046);
                color2 = GetColor(0x66003D);
            }
            else if (tileColor == MetroTileColor.Olive)
            {
                color1 = GetColor(0xBFBB11);
                color2 = GetColor(0xB2B010);
            }
            else if (tileColor == MetroTileColor.DarkOlive)
            {
                color1 = GetColor(0x787860);
                color2 = GetColor(0x6B6B56);
            }
            else if (tileColor == MetroTileColor.Rust)
            {
                color1 = GetColor(0x301818);
                color2 = GetColor(0x1F0F0F);
            }
            else if (tileColor == MetroTileColor.Maroon)
            {
                color1 = GetColor(0x603030);
                color2 = GetColor(0x4F2828);
            }
            else if (tileColor == MetroTileColor.Yellowish)
            {
                color1 = GetColor(0xF2B705);
                color2 = GetColor(0xE0A904);
            }
            else if (tileColor == MetroTileColor.Blueish)
            {
                color1 = GetColor(0x03658C);
                color2 = GetColor(0x02587A);
            }
            else if (tileColor == MetroTileColor.DarkBlue)
            {
                color1 = GetColor(0x022E40);
                color2 = GetColor(0x022533);
            }
            else if (tileColor == MetroTileColor.Yellow)
            {
                color1 = GetColor(0xF8EA00);
                color2 = GetColor(0xE5DA00);
            }
            else if (tileColor == MetroTileColor.Gray)
            {
                color1 = GetColor(0x7B7F7E);
                color2 = GetColor(0x5E6160);
            }
            else if (tileColor == MetroTileColor.DarkGreen)
            {
                color1 = GetColor(0x172421);
                color2 = GetColor(0x141F1C);
            }
            else if (tileColor == MetroTileColor.MaroonWashed)
            {
                color1 = GetColor(0x965155);
                color2 = GetColor(0x8A4A4E);
            }
            else if (tileColor == MetroTileColor.PlumWashed)
            {
                color1 = GetColor(0x40374C);
                color2 = GetColor(0x362E40);
            }
            else // Default
            {
                color1 = GetColor(0x245375);
                color2 = GetColor(0x30679B);
            }

            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop(color1, 0));
            brush.GradientStops.Add(new GradientStop(color2, 1));

            return brush;
        }

        private static Color GetColor(int rgb)
        {
            if (rgb == -1) return new Color();
            return Color.FromRgb((byte)((rgb & 0xFF0000) >> 16), (byte)((rgb & 0xFF00) >> 8), (byte)(rgb & 0xFF));
        }
    }

}
