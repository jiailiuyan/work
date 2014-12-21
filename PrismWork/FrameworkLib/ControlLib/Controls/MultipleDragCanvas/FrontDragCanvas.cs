using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using ControlLib;
using Jisons;

namespace ControlLib
{

    public class FrontDragCanvas : Canvas
    {
        #region Data

        private UIElement elementBeingDragged;

        private Point origCursorLocation;

        //private double origHorizOffset, origVertOffset;

        //private bool modifyLeftOffset, modifyTopOffset;

        //private bool isDragInProgress;

        #endregion // Data

        #region Attached Properties

        public static bool GetCanBeDragged(UIElement uiElement)
        {
            if (uiElement == null)
            {
                return false;
            }
            return (bool)uiElement.GetValue(CanBeDraggedProperty);
        }

        public static void SetCanBeDragged(UIElement uiElement, bool value)
        {
            if (uiElement != null)
            {
                uiElement.SetValue(CanBeDraggedProperty, value);
            }
        }
        public static readonly DependencyProperty CanBeDraggedProperty =
            DependencyProperty.RegisterAttached("CanBeDragged", typeof(bool), typeof(FrontDragCanvas), new UIPropertyMetadata(true));

        #endregion

        public bool AllowDragging
        {
            get { return (bool)GetValue(AllowDraggingProperty); }
            set { SetValue(AllowDraggingProperty, value); }
        }
        public static readonly DependencyProperty AllowDraggingProperty =
            DependencyProperty.Register("AllowDragging", typeof(bool), typeof(FrontDragCanvas), new PropertyMetadata(true));

        public FrontDragCanvas()
        {
            //this.Background = Brushes.Yellow;
        }

        #region Interface

        #region ElementBeingDragged

        public UIElement ElementBeingDragged
        {
            get
            {
                if (!this.AllowDragging)
                {
                    return null;
                }
                else
                {
                    return this.elementBeingDragged;
                }
            }
            set
            {
                if (this.elementBeingDragged != null)
                {
                    this.elementBeingDragged.ReleaseMouseCapture();
                }

                if (!this.AllowDragging)
                {
                    this.elementBeingDragged = null;
                }
                else
                {
                    if (FrontDragCanvas.GetCanBeDragged(value))
                    {
                        this.elementBeingDragged = value;
                        // this.elementBeingDragged.CaptureMouse();
                    }
                    else
                    {
                        this.elementBeingDragged = null;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Overrides

        public void StartDrag(Point point)
        {
            this.origCursorLocation = point; 
            foreach (var item in this.Children)
            {
                FrameworkElement control = item as FrameworkElement;
                if (control != null)
                {

                    double left = Canvas.GetLeft(control);
                    double right = Canvas.GetRight(control);
                    double top = Canvas.GetTop(control);
                    double bottom = Canvas.GetBottom(control);

                    bool modifyLeftOffset, modifyTopOffset;
                    var origHorizOffset = ResolveOffset(left, right, out modifyLeftOffset);
                    var origVertOffset = ResolveOffset(top, bottom, out modifyTopOffset);

                    control.Tag = new Rect(origHorizOffset, origVertOffset, modifyLeftOffset ? 1 : 0, modifyTopOffset ? 1 : 0);
                }
            }

        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (this.ElementBeingDragged == null)
            {
                return;
            }
            Point cursorLocation = e.GetPosition(this);

            foreach (var item in this.Children)
            {
                MoveControls(item as FrameworkElement, cursorLocation);
            }

        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            this.ElementBeingDragged = null;
        }

        #endregion

        #region Private Helpers

        #region CalculateDragElementRect

        private Rect CalculateDragElementRect(UIElement dragcontrol, double newHorizOffset, double newVertOffset, bool modifyLeftOffset, bool modifyTopOffset)
        {

            Size elemSize = dragcontrol.RenderSize;

            double x, y;
            if (modifyLeftOffset)
            {
                x = newHorizOffset;
            }
            else
            {
                x = this.ActualWidth - newHorizOffset - elemSize.Width;
            }

            if (modifyTopOffset)
            {
                y = newVertOffset;
            }
            else
            {
                y = this.ActualHeight - newVertOffset - elemSize.Height;
            }
            Point elemLoc = new Point(x, y);

            return new Rect(elemLoc, elemSize);
        }

        #endregion

        #region ResolveOffset

        private static double ResolveOffset(double side1, double side2, out bool useSide1)
        {
            useSide1 = true;
            double result;
            if (Double.IsNaN(side1))
            {
                if (Double.IsNaN(side2))
                {
                    result = 0;
                }
                else
                {
                    result = side2;
                    useSide1 = false;
                }
            }
            else
            {
                result = side1;
            }
            return result;
        }

        #endregion

        #endregion

        #region FindCanvasChild

        public UIElement FindCanvasChild(DependencyObject depObj)
        {
            while (depObj != null)
            {
                UIElement elem = depObj as UIElement;
                if (elem != null && base.Children.Contains(elem))
                {
                    break;
                }
                if (depObj is Visual || depObj is Visual3D)
                {
                    depObj = VisualTreeHelper.GetParent(depObj);
                }
                else
                {
                    depObj = LogicalTreeHelper.GetParent(depObj);
                }
            }
            return depObj as UIElement;
        }

        #endregion

        #region UpdateZOrder

        public void BringToFront(UIElement element)
        {
            this.UpdateZOrder(element, true);
        }

        public void SendToBack(UIElement element)
        {
            this.UpdateZOrder(element, false);
        }

        private void UpdateZOrder(UIElement element, bool bringToFront)
        {
            if (element == null || !base.Children.Contains(element))
            {
                return;
            }

            int elementNewZIndex = -1;
            if (bringToFront)
            {
                foreach (UIElement elem in base.Children)
                {
                    if (elem.Visibility != Visibility.Collapsed)
                    {
                        ++elementNewZIndex;
                    }
                }
            }
            else
            {
                elementNewZIndex = 0;
            }

            int offset = (elementNewZIndex == 0) ? +1 : -1;
            int elementCurrentZIndex = Canvas.GetZIndex(element);
            foreach (UIElement childElement in base.Children)
            {
                if (childElement == element)
                    Canvas.SetZIndex(element, elementNewZIndex);
                else
                {
                    int zIndex = Canvas.GetZIndex(childElement);
                    if (bringToFront && elementCurrentZIndex < zIndex ||
                        !bringToFront && zIndex < elementCurrentZIndex)
                    {
                        Canvas.SetZIndex(childElement, zIndex + offset);
                    }
                }
            }
        }

        #endregion

        private void MoveControls(FrameworkElement dragcontrol, Point cursorLocation)
        {
            if (dragcontrol == null)
            {
                return;
            }

            Rect r = (Rect)dragcontrol.Tag;

            var modifyLeftOffset = r.Width == 1;
            var modifyTopOffset = r.Height == 1;

            double newHorizontalOffset, newVerticalOffset;

            if (modifyLeftOffset)
            {
                newHorizontalOffset = r.Left + (cursorLocation.X - this.origCursorLocation.X);
            }
            else
            {
                newHorizontalOffset = r.Left - (cursorLocation.X - this.origCursorLocation.X);
            }
            if (modifyTopOffset)
            {
                newVerticalOffset = r.Top + (cursorLocation.Y - this.origCursorLocation.Y);
            }
            else
            {
                newVerticalOffset = r.Top - (cursorLocation.Y - this.origCursorLocation.Y);
            }

            Rect elemRect = this.CalculateDragElementRect(dragcontrol, newHorizontalOffset, newVerticalOffset, modifyLeftOffset, modifyTopOffset);

            bool leftAlign = elemRect.Left < 0;
            bool rightAlign = elemRect.Right > this.ActualWidth;

            if (leftAlign)
            {
                newHorizontalOffset = modifyLeftOffset ? 0 : this.ActualWidth - elemRect.Width;
            }
            else if (rightAlign)
            {
                newHorizontalOffset = modifyLeftOffset ? this.ActualWidth - elemRect.Width : 0;
            }

            if (rightAlign)
            {
                this.Width = newHorizontalOffset + elemRect.Width;
            }
            bool topAlign = elemRect.Top < 0;
            bool bottomAlign = elemRect.Bottom > this.ActualHeight;

            if (topAlign)
            {
                newVerticalOffset = modifyTopOffset ? 0 : this.ActualHeight - elemRect.Height;
            }
            else if (bottomAlign)
            {
                newVerticalOffset = modifyTopOffset ? this.ActualHeight - elemRect.Height : 0;
            }

            #region Move Drag Element

            if (modifyLeftOffset)
                Canvas.SetLeft(dragcontrol, newHorizontalOffset);
            else
                Canvas.SetRight(dragcontrol, newHorizontalOffset);

            if (modifyTopOffset)
                Canvas.SetTop(dragcontrol, newVerticalOffset);
            else
                Canvas.SetBottom(dragcontrol, newVerticalOffset);

            #endregion
        }

    }
}