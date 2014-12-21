using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    [DesignTimeVisible(false)]
    public class MetroButtonBase : ButtonBase, ISupportQuickAccessToolBar
    {
        public virtual FrameworkElement CloneForQuickAccessToolbar()
        {
            var clone = new Button();

            var imageSource = ImageSource;
            if (imageSource == null)
                imageSource = new BitmapImage(new Uri(@"DevComponents.WPF.Metro;component/Images/DefaultQATImage.png", UriKind.Relative));

            clone.Content = new Image { Source = imageSource, Height = 16, Width = 16 };
            clone.Bind(ToolTipProperty, this, ToolTipProperty);

            if (Command != null)
            {
                clone.Command = Command;
                clone.CommandParameter = CommandParameter;
            }
            else
            {
                clone.Click += delegate
                {
                    OnClick();
                };
            }
            return clone;
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for AltImageSource.
        /// </summary>
        public static readonly DependencyProperty AltImageSourceProperty =
            DependencyProperty.Register("AltImageSource", typeof(ImageSource), typeof(MetroButtonBase), new UIPropertyMetadata(null));
        /// <summary>
        /// Optional source for image which is displayed when Foreground is white.
        /// </summary>
        [Description("Optional source for image which is displayed when Foreground is White.")]
        [Browsable(true)]
        [Category("Metro")]
        public ImageSource AltImageSource
        {
            get { return (ImageSource)GetValue(AltImageSourceProperty); }
            set { SetCurrentValue(AltImageSourceProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ImageSource.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MetroButtonBase), new UIPropertyMetadata(null));
        /// <summary>
        /// Imaga source for button.
        /// </summary>
        [Description("Sets source for image which is displayed by the Button.")]
        [Browsable(true)]
        [Category("Metro")]
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetCurrentValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (ContextMenuProperty.IsUnsetValue(this))
            {
                MetroChrome chrome = null;
                var window = Window.GetWindow(this);
                if (window != null && ((chrome = window.GetFirstDescendent<MetroChrome>()) != null) && chrome.QuickAccessToolBar != null)
                {
                    SetResourceReference(ContextMenuProperty, QuickAccessToolBar.AddItemContextMenuKey);
                }
            }
        }
    }

    /// <summary>
    /// Button which supports the QAT by virtue of implementing ISupportQuickAccessToolBar.
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroButton : MetroButtonBase
    {
        static MetroButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroButton), new FrameworkPropertyMetadata(typeof(MetroButton)));
        }
    }

    /// <summary>
    /// ToggleButton which supports the QAT by virtue of implementing ISupportQuickAccessToolBar.
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroToggleButton : MetroButtonBase
    {
        static MetroToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroToggleButton), new FrameworkPropertyMetadata(typeof(MetroToggleButton)));
        }

        public static readonly RoutedEvent CheckedEvent = ToggleButton.CheckedEvent.AddOwner(typeof(MetroToggleButton));
        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckedEvent, value); }
            remove { RemoveHandler(CheckedEvent, value); }
        }

        public static readonly RoutedEvent UncheckedEvent = ToggleButton.UncheckedEvent.AddOwner(typeof(MetroToggleButton));
        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckedEvent, value); }
            remove { RemoveHandler(UncheckedEvent, value); }
        }

        /// <summary>
        /// Clones for the QAT. Implementation of ISupportQuickAccessToolBar
        /// </summary>
        public override FrameworkElement CloneForQuickAccessToolbar()
        {
            var clone = base.CloneForQuickAccessToolbar();
            clone.Bind(IsCheckedProperty, this, IsCheckedProperty);
            return clone;
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsChecked.
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty = ToggleButton.IsCheckedProperty.AddOwner(typeof(MetroToggleButton));
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetCurrentValue(IsCheckedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsThreeState.
        /// </summary>
        public static readonly DependencyProperty IsThreeStateProperty = ToggleButton.IsThreeStateProperty.AddOwner(typeof(MetroToggleButton));
        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetCurrentValue(IsThreeStateProperty, value); }
        }

        /// <summary>
        /// Overriding to set IsChecked.
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();
            if (!IsThreeState && IsChecked.HasValue)
            {
                IsChecked = !IsChecked;
            }
            else
            {
                if (IsChecked == null)
                    IsChecked = true;
                else if (IsChecked == true)
                    IsChecked = false;
                else
                    IsChecked = null;
            }
        }

    }

}
