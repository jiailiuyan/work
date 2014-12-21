using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using DevComponents.WPF.Controls;
using System.Windows.Controls.Primitives;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Button especially for the Backstage.
    /// </summary>
    [DesignTimeVisible(true)]
    public class BackstageButton : Button
    {
        static BackstageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BackstageButton), new FrameworkPropertyMetadata(typeof(BackstageButton)));
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ImageSource.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(BackstageButton), new UIPropertyMetadata(null));
        /// <summary>
        /// Source for image.
        /// </summary>
        [Description("Source for image displyed on BackstageButton")]
        [Browsable(true)]
        [Category("Metro")]
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetCurrentValue(ImageSourceProperty, value); }
        }
        
    }

    /// <summary>
    /// TabItem especially for the Backstage.
    /// </summary>
    [DesignTimeVisible(true)]
    public class BackstageTabItem : TabItem
    {
        static BackstageTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BackstageTabItem), new FrameworkPropertyMetadata(typeof(BackstageTabItem)));
        }
    }

    /// <summary>
    /// Implementation of Backstage for Metro. 
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroBackstage : TabControl
    {
        static MetroBackstage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroBackstage), new FrameworkPropertyMetadata(typeof(MetroBackstage)));
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for CloseOnClick.
        /// </summary>
        public static readonly DependencyProperty CloseOnClickProperty =
            DependencyProperty.Register("CloseOnClick", typeof(bool), typeof(MetroBackstage), new UIPropertyMetadata(true));
        /// <summary>
        /// Determines whether the backstage closes automatically when it recieves a routed Click or MouseLeftButtonDown event from within it
        /// </summary>
        [Description("Determines whether the backstage closes automatically when it recieves a routed Click or MouseLeftButtonDown event from within it.")]
        [Browsable(true)]
        [Category("Metro")]
        public bool CloseOnClick
        {
            get { return (bool)GetValue(CloseOnClickProperty); }
            set { SetCurrentValue(CloseOnClickProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyCloseOnClickValue();
        }
        
        /// <summary>
        /// Overriding to allow any UIElement to be its own container.
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is UIElement;
        }

        /// <summary>
        /// Overriding to return ContentPresenter as default container.
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentPresenter();
        }

        private void ApplyCloseOnClickValue()
        {
            var metroShell = this.GetVisualParent<MetroShell>();
            if (metroShell != null)
            {
                var popup = metroShell.Template.FindName("Popup", metroShell) as Popup;
                if (popup != null)
                {
                    PopupBehavior.SetCloseOnInternalClick(popup, CloseOnClick);
                }
            }
        }
    }
}
