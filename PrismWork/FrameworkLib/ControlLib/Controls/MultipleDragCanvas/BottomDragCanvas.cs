using ControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ControlLib
{
    public class BottomDragCanvas : Canvas
    {

        public int ItemWidth
        {
            get { return (int)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(int), typeof(BottomDragCanvas), new PropertyMetadata(65));

        public int ItemHeight
        {
            get { return (int)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(int), typeof(BottomDragCanvas), new PropertyMetadata(65));

        public static readonly Brush StartBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6BE0E0C9"));

        Rectangle CenterControl = new Rectangle() { IsHitTestVisible = false };

        public Rect SelectRect
        {
            get { return new Rect(Canvas.GetLeft(CenterControl), Canvas.GetTop(CenterControl), CenterControl.ActualWidth, CenterControl.ActualHeight); }
        }

        public BottomDragCanvas()
        {

            this.Children.Add(CenterControl);

            this.SizeChanged += BottomDragCanvas_SizeChanged;
        }

        void BottomDragCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Height == 0 || e.PreviousSize.Width == 0)
            {
                if (e.NewSize.Height != 0 && e.NewSize.Width != 0)
                {
                    this.OrderAllControls();
                }
            }
        }

        void BottomDragCanvas_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnPreviewMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            Move(e.GetPosition(this));
        }

        protected override void OnPreviewMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            if (startPoint != null && startPoint.Value.Equals(e.GetPosition(this)))
            {
                ResetShow();
            }
            End();

            this.ReleaseMouseCapture();
        }

        private Point? startPoint;

        public void Start(Point point)
        {
            startPoint = point;
            this.BringToFront(CenterControl);
        }

        private void Move(Point point)
        {
            if (startPoint != null)
            {
                this.CenterControl.Fill = StartBrush;
                if (startPoint.Value.X <= point.X && startPoint.Value.Y <= point.Y)
                {
                    SetShowPosition(startPoint.Value.X, startPoint.Value.Y, this.Width - point.X, this.Height - point.Y);
                }
                else if (startPoint.Value.X <= point.X && startPoint.Value.Y >= point.Y)
                {
                    SetShowPosition(startPoint.Value.X, point.Y, this.Width - point.X, this.Height - startPoint.Value.Y);
                }
                else if (startPoint.Value.X >= point.X && startPoint.Value.Y >= point.Y)
                {
                    SetShowPosition(point.X, point.Y, this.Width - startPoint.Value.X, this.Height - startPoint.Value.Y);
                }
                else if (startPoint.Value.X >= point.X && startPoint.Value.Y <= point.Y)
                {
                    SetShowPosition(point.X, startPoint.Value.Y, this.Width - startPoint.Value.X, this.Height - point.Y);
                }
            }
        }

        private void SetShowPosition(double lw, double th, double rw, double bh)
        {
            this.CenterControl.Width = this.Width - rw - lw;
            this.CenterControl.Height = this.Height - bh - th;

            Canvas.SetLeft(this.CenterControl, lw);
            Canvas.SetTop(this.CenterControl, th);
        }

        public void End()
        {
            startPoint = null;
            this.CenterControl.Fill = null;
        }

        private void ResetShow()
        {
            this.CenterControl.Width = 0;
            this.CenterControl.Height = 0;
        }

        public void OrderAllControls()
        {
            int n = 0;
            int m = 0;
            foreach (var item in this.Children)
            {
                var ac = item as IActionControl;
                if (ac != null)
                {
                    int number = (int)this.ActualHeight / ItemWidth;
                    if (n * ItemHeight + ItemHeight <= this.ActualHeight)
                    {
                        Canvas.SetLeft(ac.Element, 0 + m * ItemWidth);
                        Canvas.SetTop(ac.Element, 0 + n * ItemHeight);
                    }
                    else
                    {
                        m++;
                        n = 0;
                        Canvas.SetLeft(ac.Element, 0 + m * ItemWidth);
                        Canvas.SetTop(ac.Element, 0 + n * ItemHeight);
                    }
                    n++;
                }
            }
        }

        List<Point> newpoint = new List<Point>();
        public void AlignmentAllControls()
        {
            newpoint.Clear();
            foreach (var item in this.Children)
            {
                var ac = item as IActionControl;
                if (ac != null)
                {
                    double leftpoint = Canvas.GetLeft(ac.Element);
                    double toppoint = Canvas.GetTop(ac.Element);
                    double n = leftpoint / ItemWidth;
                    int n1 = (int)n;
                    double m = toppoint / ItemHeight;
                    int m1 = (int)m;
                    Point iteams = new Point(n1, m1);
                    if (!newpoint.Contains(iteams))
                    {
                        Canvas.SetLeft(ac.Element, n1 * ItemWidth);
                        Canvas.SetTop(ac.Element, m1 * ItemHeight);
                    }
                    else
                    {
                        while (true)
                        {
                            iteams = new Point(n1, m1);
                            var a = m1 * ItemHeight + ItemHeight <= this.ActualHeight;
                            if (a)
                            {
                                if (!newpoint.Contains(iteams))
                                {
                                    Canvas.SetLeft(ac.Element, n1 * ItemWidth);
                                    Canvas.SetTop(ac.Element, m1 * ItemHeight);
                                    break;
                                }
                                else
                                {
                                    m1++;
                                }
                            }
                            else
                            {
                                n1++;
                                m1 = 0;
                            }
                        }

                    }
                    newpoint.Add(iteams);
                }
            }
        }

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
    }
}
