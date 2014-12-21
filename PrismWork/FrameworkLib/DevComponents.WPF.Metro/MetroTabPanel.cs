using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of a custom Panel for displaying the MetroTabItem headers in a MetroShell. 
    /// This class is derived from VirtualizingPanel only to make it possible to insert the scroll buttons into the 
    /// panel children. 
    /// </summary>
    [DesignTimeVisible(false)]
    public class MetroTabPanel : VirtualizingPanel
    {
        private int _Offset;

        public Button ShiftRightButton { get; set; }
        public Button ShiftLeftButton { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (ShiftLeftButton != null)
            {
                ShiftLeftButton.Click += delegate
                {
                    _Offset++;
                    InvalidateMeasure();
                };
            }

            if (ShiftRightButton != null)
            {
                ShiftRightButton.Click += delegate
                {
                    _Offset--;
                    InvalidateMeasure();
                };
            }

            // Bug in WPF - need to access InternalChildren so that ItemContainerGenerator gets set internally.
            var children = InternalChildren;

            var shell = ItemsControl.GetItemsOwner(this);
            if (shell != null)
            {
                shell.ItemContainerGenerator.ItemsChanged += delegate
                {
                    Dispatcher.BeginInvoke(new Action(InvalidateMeasure), DispatcherPriority.Loaded);
                };
            }
        }

        /// <summary>
        /// Overriding to perform custom measurment.
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            var generator = ItemContainerGenerator;
            if (generator == null)
                return Size.Empty;

            bool showShiftLeft = false, showShiftRight = _Offset > 0;
            double measuredHeight = 0, measuredWidth = 0;

            if (ShiftRightButton != null)
            {
                if (showShiftRight)
                {
                    if (!Children.Contains(ShiftRightButton))
                        AddInternalChild(ShiftRightButton);
                    ShiftRightButton.HorizontalAlignment = HorizontalAlignment.Left;
                    ShiftRightButton.Visibility = Visibility.Visible;
                    ShiftRightButton.Measure(availableSize);
                    measuredWidth = ShiftRightButton.DesiredSize.Width;
                }
                else
                {
                    ShiftRightButton.Visibility = Visibility.Collapsed;
                }
            }
            using (var disposable = generator.StartAt(generator.GeneratorPositionFromIndex(-1), GeneratorDirection.Forward))
            {
                int index = 0;
                FrameworkElement element = null;
                bool newlyCreated;
                while ((element = (FrameworkElement)generator.GenerateNext(out newlyCreated)) != null)
                {
                    if (newlyCreated)
                    {
                        AddInternalChild(element);
                        generator.PrepareItemContainer(element);
                    }

                    bool showElement = _Offset <= index && !showShiftLeft;
                    if (showElement)
                    {
                        element.Visibility = Visibility.Visible;
                        element.Measure(availableSize);
                        measuredWidth += element.DesiredSize.Width;
                        if (measuredWidth > availableSize.Width)
                        {
                            measuredWidth -= element.DesiredSize.Width;
                            showShiftLeft = true;
                            showElement = false;
                        }
                        else
                            measuredHeight = Math.Max(measuredHeight, element.DesiredSize.Height);
                    }

                    if (!showElement)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }

                    index++;
                }
            }

            if (ShiftLeftButton != null)
            {
                if (showShiftLeft)
                {
                    if (!InternalChildren.Contains(ShiftLeftButton))
                        AddInternalChild(ShiftLeftButton);
                    ShiftLeftButton.HorizontalAlignment = HorizontalAlignment.Right;
                    ShiftLeftButton.Visibility = Visibility.Visible;
                    ShiftLeftButton.Measure(availableSize);
                }
                else
                {
                    ShiftLeftButton.Visibility = Visibility.Collapsed;
                }
            }

            return new Size(measuredWidth, measuredHeight);
        }

        /// <summary>
        /// Overriding to perform custom arrangement.
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0)
                return finalSize;

            var generator = (ItemContainerGenerator)ItemContainerGenerator;
            if (generator == null)
                return finalSize;

            Rect rect = new Rect(0, 0, 0, finalSize.Height);
            double availableWidth = finalSize.Width;

            if (ShiftLeftButton != null && ShiftLeftButton.Visibility == Visibility.Visible)
            {
                rect.Width = ShiftLeftButton.DesiredSize.Width;
                rect.X = finalSize.Width - rect.Width;
                ShiftLeftButton.Arrange(rect);
                availableWidth -= rect.Width;
                rect.X = 0;
            }

            if (ShiftRightButton != null && ShiftRightButton.Visibility == Visibility.Visible)
            {
                rect.Width = ShiftRightButton.DesiredSize.Width;
                ShiftRightButton.Arrange(rect);
                rect.X = rect.Width;
                availableWidth -= rect.Width;
            }

            var itemsControl = ItemsControl.GetItemsOwner(this);
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                var element = (FrameworkElement)generator.ContainerFromIndex(i);
                if (element.Visibility == Visibility.Collapsed)
                    continue;

                rect.Width = element.DesiredSize.Width;

                if (rect.Width + rect.X > availableWidth)
                    rect.Width = 0;
                element.Arrange(rect);
                rect.X += rect.Width;
            }

            return finalSize;
        }
    }
}
