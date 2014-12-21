using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of custom panel for MetroStatusBar.
    /// </summary>
    [DesignTimeVisible(false)]
    public class MetroStatusBarPanel : Grid
    {
        private bool _ShouldSetupColumns;

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var parentStatusBar = ItemsControl.GetItemsOwner(this) as MetroStatusBar;
            if (parentStatusBar != null)
            {
                parentStatusBar.ItemsChanged += delegate
                {
                    _ShouldSetupColumns = true;
                };
            }
            _ShouldSetupColumns = true;
        }

        /// <summary>
        /// Overriding to ensure columns are set up correctly.
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (_ShouldSetupColumns)
            {
                SetupColumns();
                _ShouldSetupColumns = false;
            }
            return base.MeasureOverride(availableSize);
        }

        private void SetupColumns()
        {
            var children = Children.OfType<FrameworkElement>().ToList();

            if (ColumnDefinitions.Count < children.Count)
            {
                for (int i = ColumnDefinitions.Count; i < children.Count; i++)
                    ColumnDefinitions.Add(new ColumnDefinition());
            }
            else if (ColumnDefinitions.Count > children.Count)
            {
                for (int i = ColumnDefinitions.Count - 1; i >= children.Count; i--)
                {
                    ColumnDefinitions.RemoveAt(i);
                }
            }

            for (int i = 0; i < children.Count; i++)
            {
                bool firstRightAligned = true;
                var col = ColumnDefinitions[i];
                var fe = children[i] as FrameworkElement;
                Grid.SetColumn(fe, i);

                if (fe.HorizontalAlignment == HorizontalAlignment.Left)
                {
                    col.Width = new GridLength(1, GridUnitType.Auto);
                }
                else if (fe.HorizontalAlignment == HorizontalAlignment.Center)
                {
                    col.Width = new GridLength(1, GridUnitType.Star);
                }
                else if (fe.HorizontalAlignment == HorizontalAlignment.Right)
                {
                    if (firstRightAligned)
                        col.Width = new GridLength(1, GridUnitType.Star);
                    else
                        col.Width = new GridLength(1, GridUnitType.Auto);
                    firstRightAligned = false;
                }
                else if (fe.HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    col.Width = new GridLength(1, GridUnitType.Star);
                }
            }
        }
    }
}
