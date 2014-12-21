using System.Windows;
using System.Windows.Controls;
using System;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of MetroStatusBar. Default style uses MetroStatusBarPanel for Items panel. 
    /// </summary>
    public class MetroStatusBar : ItemsControl
    {
        static MetroStatusBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroStatusBar), new FrameworkPropertyMetadata(typeof(MetroStatusBar)));
        }

        public event EventHandler ItemsChanged;

        /// <summary>
        // Using a DependencyProperty as the backing store for InnerMargin.
        /// </summary>
        internal static readonly DependencyProperty InnerMarginProperty =
            DependencyProperty.Register("InnerMargin", typeof(Thickness), typeof(MetroStatusBar), new UIPropertyMetadata(new Thickness(0)));
        /// <summary>
        /// Used internally to make room for window resize handle.
        /// </summary>
        internal Thickness InnerMargin
        {
            get { return (Thickness)GetValue(InnerMarginProperty); }
            set { SetCurrentValue(InnerMarginProperty, value); }
        }
        
        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Foreground gets inherited even though there is a property setter for it in the default style.
            // (And the setter is a dynamic reference to metro colors.) Fix this here...
            var source = DependencyPropertyHelper.GetValueSource(this, ForegroundProperty);
            if (source.BaseValueSource == BaseValueSource.Inherited)
            {
                ClearValue(ForegroundProperty);
            }
        }

        /// <summary>
        /// Overriding to raise the ItemsChanged internal event.
        /// </summary>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (ItemsChanged != null)
                ItemsChanged(this, EventArgs.Empty);
        }
    }
}
