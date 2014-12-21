using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using DevComponents.WPF.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevComponents.WPF.Metro
{   
    /// <summary>
    /// Implementation of Metro Tile style button which is meant to go inside MetroStartControl.
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroTile : Button
    {
        private static MultiplicationConverter _MultiplicationConverter = new MultiplicationConverter();
        static MetroTile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTile), new FrameworkPropertyMetadata(typeof(MetroTile)));
        }

        private MetroTileFramesPresenter _FramesPresenter;

        /// <summary>
        ///  Gets an enumeration of the MetroTileFrames. If FramesSource is not set, then the enumeration will be empty.
        /// </summary>
        public IEnumerable<MetroTileFrame> Frames
        {
            get
            {
                if (_FramesPresenter != null)
                    return _FramesPresenter.Frames;
                return Enumerable.Empty<MetroTileFrame>();
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for FramesSource.
        /// </summary>       
        public static readonly DependencyProperty FramesSourceProperty =
            DependencyProperty.Register("FramesSource", typeof(IEnumerable), typeof(MetroTile), new FrameworkPropertyMetadata(null,
                (s,e)=>
                {
                    //((MetroTile)s).EnsureFramesPresenter();
                }));
        /// <summary>
        /// Get or set the source for the Frames.
        /// </summary>
        public IEnumerable FramesSource
        {
            get { return (IEnumerable)GetValue(FramesSourceProperty); }
            set { SetValue(FramesSourceProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HasFrames.
        /// </summary>
        internal static readonly DependencyProperty HasFramesProperty =
            DependencyProperty.Register("HasFrames", typeof(bool), typeof(MetroTile), new FrameworkPropertyMetadata(false));
        /// <summary>
        /// Gets whether the tile has multiple frames which are cycled.
        /// </summary>
        public bool HasFrames
        {
            get { return (bool)GetValue(HasFramesProperty); }
        }
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for ImageSource.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = MetroTileFrame.ImageSourceProperty.AddOwner(typeof(MetroTile));
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
        public static readonly DependencyProperty TileColorProperty = MetroTileFrame.TileColorProperty.AddOwner(typeof(MetroTile), new UIPropertyMetadata(MetroTileColor.Default,
            (s,e)=>
            {
                ((MetroTile)s).HandleTileColorChanged((MetroTileColor)e.NewValue);
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
        /// Using a DependencyProperty as the backing store for Title.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = MetroTileFrame.TitleProperty.AddOwner(typeof(MetroTile));
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
        /// Using a DependencyProperty as the backing store for TitleTemplate.
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty = MetroTileFrame.TitleTemplateProperty.AddOwner(typeof(MetroTile));
        /// <summary>
        /// Get or Set a DataTemplate which shows how to render the Title.
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetCurrentValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Type.
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(MetroTileType), typeof(MetroTile), new UIPropertyMetadata(MetroTileType.Large,
                (s, e) =>
                {
                    var tile = (MetroTile)s;
                    if(tile.IsLoaded)
                        tile.SetWidth((MetroTileType)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets the Type of the tile - either Large or Small. This is a dependency property. The default value is Large.
        /// </summary>
        public MetroTileType Type
        {
            get { return (MetroTileType)GetValue(TypeProperty); }
            set { SetCurrentValue(TypeProperty, value); }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (BackgroundProperty.IsUnsetValue(this))
                Background = MetroTileFrame.GetBackgroundBrushFromTileColor(TileColor);
            
            if (CommandParameterProperty.IsUnsetValue(this))
                CommandParameter = this;

            SetWidth(Type);

            var scaleTransform = GetTemplateChild("ScaleTransform") as ScaleTransform;
            if (scaleTransform != null)
            {
                scaleTransform.Bind(ScaleTransform.CenterXProperty, this, ActualWidthProperty, BindingMode.OneWay, _MultiplicationConverter, 0.5);
                scaleTransform.Bind(ScaleTransform.CenterYProperty, this, ActualHeightProperty, BindingMode.OneWay, _MultiplicationConverter, 0.5);
            }

            var content3d = GetTemplateChild("Content3D") as Content3D;
            if (content3d != null)
            {
                var transform = GetTemplateChild("RotateRightInwards") as RotateTransform3D;
                if (transform != null)
                    transform.Bind(RotateTransform3D.CenterXProperty, content3d, Content3D.ThreeDBoundsXProperty);
                transform = GetTemplateChild("RotateLeftInwards") as RotateTransform3D;
                if (transform != null)
                    transform.Bind(RotateTransform3D.CenterXProperty, content3d, Content3D.ThreeDBoundsXProperty, BindingMode.OneWay, _MultiplicationConverter, -1);
                transform = GetTemplateChild("RotateTopInwards") as RotateTransform3D;
                if (transform != null)
                    transform.Bind(RotateTransform3D.CenterYProperty, content3d, Content3D.ThreeDBoundsYProperty);
                transform = GetTemplateChild("RotateBottomInwards") as RotateTransform3D;
                if (transform != null)
                    transform.Bind(RotateTransform3D.CenterYProperty, content3d, Content3D.ThreeDBoundsYProperty, BindingMode.OneWay, _MultiplicationConverter, -1);
            }

            _FramesPresenter = GetTemplateChild("FramesPresenter") as MetroTileFramesPresenter;
            if(_FramesPresenter != null)
                this.Bind(HasFramesProperty, _FramesPresenter, "HasItems");
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (e.OriginalSource == this && e.LeftButton == MouseButtonState.Pressed)
            {
                if (Type == MetroTileType.Small)
                    Type = MetroTileType.Large;
                else
                    Type = MetroTileType.Small;
            }
        }        

        private void SetWidth(MetroTileType type)
        {
            var width = Width;
            if (double.IsNaN(width))
                width = ActualWidth;
            if (width == 0)
                return;

            if (type == MetroTileType.Small)
            {
                if(WidthProperty.IsUnsetValue(this, BaseValueSource.DefaultStyle, BaseValueSource.Style))
                    Width = width / 2 - Margin.Left / 2 - Margin.Right / 2;
            }
            else
            {
                ClearValue(WidthProperty);
            }

            if (IsLoaded)
            {
                var page = this.GetVisualParent<MetroStartPage>();
                if (page != null)
                    page.RefreshLayout();
            }
        }

        private void HandleTileColorChanged(MetroTileColor metroTileColor)
        {
            if (!IsLoaded)
                return;
            Background = MetroTileFrame.GetBackgroundBrushFromTileColor(TileColor);
        }
    }    
}
