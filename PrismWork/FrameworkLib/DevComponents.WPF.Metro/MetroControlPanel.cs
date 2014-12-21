using System.Windows;
using System.Windows.Controls;
using System;
using System.ComponentModel;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Layout panel for items of MetroControl.
    /// </summary>
    [DesignTimeVisible(false)]
    public class MetroControlPanel : Canvas
    {
        /// <summary>
        /// Overriding to make sure each Page inside the MetroControl is re-measured when there
        /// is a change in available height.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            foreach (UIElement element in Children)
                element.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// The purpose here is to ensure that all pages are rendered with the same height
        /// regardless of the number of tiles in them, which is important when dragging tiles
        /// between pages.
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Rect rect = new Rect(0, 0, 0, arrangeSize.Height);
            foreach (UIElement element in Children)
            {
                rect.X = Canvas.GetLeft(element);
                if (Double.IsNaN(rect.X))
                    rect.X = 0;
                rect.Width = element.DesiredSize.Width;
                element.Arrange(rect);
            }
            return arrangeSize;
        }
    }
}
