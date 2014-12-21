using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Defines a tab item explicitly meant for use in MetroShell.
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroTabItem : TabItem
    {
        static MetroTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTabItem), new FrameworkPropertyMetadata(typeof(MetroTabItem)));
        }

        #region Dependency Properties

        private static readonly DependencyPropertyKey IsHighlightedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsHighlighted", typeof(bool), typeof(MetroTabItem), new UIPropertyMetadata(false));
        /// <summary>
        // Using a DependencyProperty as the backing store for IsHighlighted.
        /// </summary>
        public static readonly DependencyProperty IsHighlightedProperty = IsHighlightedPropertyKey.DependencyProperty;
        /// <summary>
        /// Gets whether the tab item is highlighted. This is a read-only dependency property.
        /// </summary>
        [Browsable(false)]
        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            private set { SetValue(IsHighlightedPropertyKey, value); }
        }

        #endregion // Dependency Properties

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var mainGrid = GetTemplateChild("MainGrid") as Grid;
            if (mainGrid != null)
            {
                // Use main grid as trigger for highlight, because selected content also raises mouse enter.
                mainGrid.MouseEnter += delegate
                {
                    if (!IsSelected)
                        IsHighlighted = true;
                };
                mainGrid.MouseLeave += delegate
                {
                    IsHighlighted = false;
                };
            }
        }

        #endregion // Overrides

    }
}
