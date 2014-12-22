using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jisons;
using WorkCommon.Plugin;

namespace ControlLib
{
    /// <summary>
    /// MultipleDragCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class MultipleDragCanvas : UserControl
    {

        public List<IActionControl> DragControls = new List<IActionControl>();

        public event EventHandler<AddControlArgs> AddControlEvent;
        protected void AddingControlEvent(IPluginObject po)
        {
            if (this.AddControlEvent != null)
            {
                this.AddControlEvent(this, new AddControlArgs() { PluginObject = po });
            }
        }

        Point? MousePoint = null;
        Point? DragPoint = null;
        IActionControl selectcontrol = null;

        public MultipleDragCanvas()
        {
            InitializeComponent();
            this.SizeChanged += MultipleDragCanvas_SizeChanged;

            this.Background = Brushes.Transparent;

            EndDrag();

            Test();

            this.frontcanvas.PreviewMouseLeftButtonUp += frontcanvas_PreviewMouseLeftButtonUp;

            this.bottomcanvas.PreviewMouseMove += bottomcanvas_PreviewMouseMove;
        }

        public void Test()
        {
            //CanvasButton cb = new CanvasButton() { Width = 50, Height = 50, Background = Brushes.Red };
            //AddDragControl(cb);
            //Canvas.SetLeft(cb, 161);
            //Canvas.SetTop(cb, 55);

            //cb = new CanvasButton() { Width = 40, Height = 40 };
            //AddDragControl(cb);
            //Canvas.SetLeft(cb, 61);
            //Canvas.SetTop(cb, 55);

            //cb = new CanvasButton() { Width = 40, Height = 40 };
            //AddDragControl(cb);
            //Canvas.SetLeft(cb, 1);
            //Canvas.SetTop(cb, 55);
        }

        void MultipleDragCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.frontcanvas.Width = this.bottomcanvas.Width = this.ActualWidth;
            this.frontcanvas.Height = this.bottomcanvas.Height = this.ActualHeight;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            var mousepoint = e.GetPosition(this.bottomcanvas);

            var hitvisual = VisualTreeHelper.HitTest(this.bottomcanvas, mousepoint);
            if (hitvisual != null && hitvisual.VisualHit != null)
            {
                selectcontrol = hitvisual.VisualHit.FindVisualParent<IActionControl>();
            }

            if (selectcontrol != null)
            {
                if (!selectcontrol.IsSelected)
                {
                    CheckSelected(selectcontrol);
                }

                DragPoint = mousepoint;

                this.bottomcanvas.BringToFront(selectcontrol.Element);
            }
            else
            {
                MousePoint = mousepoint;
                this.bottomcanvas.Start(MousePoint.Value);
                JudgeSelect(Rect.Empty);
                this.bottomcanvas.CaptureMouse();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            var p = e.GetPosition(this);
            if (DragPoint != null && Math.Abs(p.X - DragPoint.Value.X) > 3 && Math.Abs(p.Y - DragPoint.Value.Y) > 3)
            {
                StartDrag(selectcontrol, DragPoint.Value);
                DragPoint = null;
            }
        }

        void bottomcanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(this);
            if (MousePoint != null)
            {
                JudgeSelect(bottomcanvas.SelectRect);
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            this.DragPoint = null;
            this.MousePoint = null;
            this.selectcontrol = null;
            this.ReleaseMouseCapture();
        }

        void frontcanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNewPosition();
            EndDrag();
        }

        public void AddDragControl(IPluginObject iplugin)
        {
            CanvasButton cb = new CanvasButton(iplugin);
            cb.AddEvent += cb_AddEvent;
            DragControls.Add(cb);

            this.bottomcanvas.Children.Add(cb.Element);

            Canvas.SetLeft(cb.Element, 200);
            Canvas.SetTop(cb.Element, 200);
        }

        void cb_AddEvent(object sender, EventArgs e)
        {
            var cb = sender as CanvasButton;
            if (cb != null)
            {
                AddingControlEvent(cb.PluginObject);
            }
        }

        public bool CheckSelected(IActionControl ac)
        {
            bool recheck = ac == null ? false : !ac.IsSelected;
            this.DragControls.ForEach(i => i.IsSelected = i.Equals(ac));
            return recheck;
        }

        public List<IActionControl> GetSelectItems()
        {
            return this.DragControls.Where(i => i.IsSelected).ToList();
        }

        public void StartDrag(IActionControl mousecontrol, Point startpoint)
        {
            this.frontcanvas.Visibility = System.Windows.Visibility.Visible;

            this.frontcanvas.Children.Clear();

            FrameworkElement dragcontrol = null;

            foreach (var item in GetSelectItems())
            {
                var x = Canvas.GetLeft(item.Element);
                var y = Canvas.GetTop(item.Element);

                Image image = new Image();
                image.DataContext = item;
                image.Source = item.DefaultView;
                image.Width = item.Element.ActualWidth;
                image.Height = item.Element.ActualHeight;
                this.frontcanvas.Children.Add(image);
                Canvas.SetLeft(image, x);
                Canvas.SetTop(image, y);

                if (mousecontrol.Equals(item))
                {
                    dragcontrol = image;
                }
            }

            this.frontcanvas.ElementBeingDragged = dragcontrol;
            this.frontcanvas.StartDrag(startpoint);

            dragcontrol.Loaded += (s, e) => dragcontrol.CaptureMouse();
        }

        public void EndDrag()
        {
            this.frontcanvas.Visibility = System.Windows.Visibility.Collapsed;

            this.frontcanvas.Children.Clear();
            this.frontcanvas.ReleaseMouseCapture();

            this.ReleaseMouseCapture();
        }

        private void SetNewPosition()
        {
            foreach (var item in this.frontcanvas.Children)
            {
                var control = item as FrameworkElement;
                if (control != null)
                {
                    var content = control.DataContext as IActionControl;
                    if (content != null)
                    {
                        var x = Canvas.GetLeft(control);
                        var y = Canvas.GetTop(control);

                        Canvas.SetLeft(content.Element, x);
                        Canvas.SetTop(content.Element, y);
                    }
                }
            }

        }

        private void JudgeSelect(Rect selectrect)
        {
            foreach (IActionControl ac in this.DragControls)
            {
                if (!selectrect.Equals(Rect.Empty))
                {
                    var hitrect = new Rect(selectrect.Left, selectrect.Top, selectrect.Width, selectrect.Height);
                    var r = new Rect(Canvas.GetLeft(ac.Element), Canvas.GetTop(ac.Element), ac.Element.ActualWidth, ac.Element.ActualHeight);
                    hitrect.Intersect(r);
                    ac.IsSelected = !hitrect.Equals(Rect.Empty);
                }
                else
                {
                    ac.IsSelected = false;
                }
            }
        }

        public void OrderAllControls()
        {
            this.bottomcanvas.OrderAllControls();
        }

        public void AlignmentAllControls()
        {
            this.bottomcanvas.AlignmentAllControls();
        }

    }
}



//using System;
//using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Media3D;
//using dragcanvas;
//using Jisons;

//namespace ControlLib
//{

//    public class MultipleDragCanvas1 : Canvas
//    {
//        #region Data

//        private UIElement elementBeingDragged;

//        private Point origCursorLocation;

//        private double origHorizOffset, origVertOffset;

//        private bool modifyLeftOffset, modifyTopOffset;

//        private bool isDragInProgress;

//        #endregion // Data

//        #region Attached Properties

//        public static bool GetCanBeDragged(UIElement uiElement)
//        {
//            if (uiElement == null)
//            {
//                return false;
//            }
//            return (bool)uiElement.GetValue(CanBeDraggedProperty);
//        }

//        public static void SetCanBeDragged(UIElement uiElement, bool value)
//        {
//            if (uiElement != null)
//            {
//                uiElement.SetValue(CanBeDraggedProperty, value);
//            }
//        }
//        public static readonly DependencyProperty CanBeDraggedProperty =
//            DependencyProperty.RegisterAttached("CanBeDragged", typeof(bool), typeof

//(MultipleDragCanvas1), new UIPropertyMetadata(true));

//        #endregion

//        public bool AllowDragging
//        {
//            get { return (bool)GetValue(AllowDraggingProperty); }
//            set { SetValue(AllowDraggingProperty, value); }
//        }
//        public static readonly DependencyProperty AllowDraggingProperty =
//            DependencyProperty.Register("AllowDragging", typeof(bool), typeof

//(MultipleDragCanvas1), new PropertyMetadata(true));

//        private DragViewControl DragViewControl = new DragViewControl() { Visibility = 

//Visibility.Collapsed, Width = 100, Height = 100 };

//        //private List<IActionControl> dragControlItems = new List<IActionControl>();
//        //public List<IActionControl> DragControlItems
//        //{
//        //    get { return dragControlItems; }
//        //    set
//        //    {
//        //        dragControlItems = value;
//        //    }
//        //}

//        public MultipleDragCanvas1()
//        {

//            this.Background = Brushes.Transparent;

//            this.Children.Add(this.DragViewControl);
//        }

//        #region ElementBeingDragged

//        public UIElement ElementBeingDragged
//        {
//            get
//            {
//                if (!this.AllowDragging)
//                {
//                    return null;
//                }
//                else
//                {
//                    return this.elementBeingDragged;
//                }
//            }
//            protected set
//            {
//                if (this.elementBeingDragged != null)
//                {
//                    this.elementBeingDragged.ReleaseMouseCapture();
//                }

//                if (!this.AllowDragging)
//                {
//                    this.elementBeingDragged = null;
//                }
//                else
//                {
//                    if (MultipleDragCanvas1.GetCanBeDragged(value))
//                    {
//                        this.elementBeingDragged = value;
//                        this.elementBeingDragged.CaptureMouse();
//                    }
//                    else
//                    {
//                        this.elementBeingDragged = null;
//                    }
//                }
//            }
//        }

//        #endregion

//        public Point? p;
//        IInputElement MouseCaptured;



//        #region Overrides

//        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
//        {
//            base.OnPreviewMouseLeftButtonDown(e);

//            this.isDragInProgress = false;

//            this.origCursorLocation = e.GetPosition(this);

//            IActionControl selectcontrol = null;

//            var hitvisual = VisualTreeHelper.HitTest(this, this.origCursorLocation);
//            if (hitvisual != null && hitvisual.VisualHit != null)
//            {
//                selectcontrol = hitvisual.VisualHit.FindVisualParent<IActionControl>();
//            }

//            CheckSelected(selectcontrol);

//            if (selectcontrol != null)
//            {
//                SetDragImage();
//                //this.ElementBeingDragged=     this.DragImage 
//            }
//            else
//            {
//                this.CaptureMouse();

//                p = this.origCursorLocation;
//            }

//            //this.ElementBeingDragged = selectcontrol as UIElement;
//            if (this.ElementBeingDragged == null)
//            {
//                return;
//            }

//            BringToFront(this.ElementBeingDragged);

//            double left = Canvas.GetLeft(this.ElementBeingDragged);
//            double right = Canvas.GetRight(this.ElementBeingDragged);
//            double top = Canvas.GetTop(this.ElementBeingDragged);
//            double bottom = Canvas.GetBottom(this.ElementBeingDragged);

//            this.origHorizOffset = ResolveOffset(left, right, out this.modifyLeftOffset);
//            this.origVertOffset = ResolveOffset(top, bottom, out this.modifyTopOffset);

//            e.Handled = true;

//            this.isDragInProgress = true;
//        }

//        protected override void OnPreviewMouseMove(MouseEventArgs e)
//        {
//            base.OnPreviewMouseMove(e);

//            if (this.ElementBeingDragged == null || !this.isDragInProgress)
//            {
//                return;
//            }
//            Point cursorLocation = e.GetPosition(this);

//            double newHorizontalOffset, newVerticalOffset;

//            if (this.modifyLeftOffset)
//            {
//                newHorizontalOffset = this.origHorizOffset + (cursorLocation.X - 

//this.origCursorLocation.X);
//            }
//            else
//            {
//                newHorizontalOffset = this.origHorizOffset - (cursorLocation.X - 

//this.origCursorLocation.X);
//            }
//            if (this.modifyTopOffset)
//            {
//                newVerticalOffset = this.origVertOffset + (cursorLocation.Y - 

//this.origCursorLocation.Y);
//            }
//            else
//            {
//                newVerticalOffset = this.origVertOffset - (cursorLocation.Y - 

//this.origCursorLocation.Y);
//            }

//            Rect elemRect = this.CalculateDragElementRect(newHorizontalOffset, 

//newVerticalOffset);

//            bool leftAlign = elemRect.Left < 0;
//            bool rightAlign = elemRect.Right > this.ActualWidth;

//            if (leftAlign)
//            {
//                newHorizontalOffset = modifyLeftOffset ? 0 : this.ActualWidth - 

//elemRect.Width;
//            }
//            else if (rightAlign)
//            {
//                newHorizontalOffset = modifyLeftOffset ? this.ActualWidth - elemRect.Width : 

//0;
//            }

//            if (rightAlign)
//            {
//                this.Width = newHorizontalOffset + elemRect.Width;
//            }
//            bool topAlign = elemRect.Top < 0;
//            bool bottomAlign = elemRect.Bottom > this.ActualHeight;

//            if (topAlign)
//            {
//                newVerticalOffset = modifyTopOffset ? 0 : this.ActualHeight - 

//elemRect.Height;
//            }
//            else if (bottomAlign)
//            {
//                newVerticalOffset = modifyTopOffset ? this.ActualHeight - elemRect.Height : 

//0;
//            }

//            #region Move Drag Element

//            if (this.modifyLeftOffset)
//                Canvas.SetLeft(this.ElementBeingDragged, newHorizontalOffset);
//            else
//                Canvas.SetRight(this.ElementBeingDragged, newHorizontalOffset);

//            if (this.modifyTopOffset)
//                Canvas.SetTop(this.ElementBeingDragged, newVerticalOffset);
//            else
//                Canvas.SetBottom(this.ElementBeingDragged, newVerticalOffset);

//            #endregion // Move Drag Element
//        }

//        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
//        {
//            base.OnPreviewMouseUp(e);

//            this.CaptureMouse();
//            this.ElementBeingDragged = null;
//        }

//        #endregion

//        #region Private Helpers

//        #region CalculateDragElementRect

//        private Rect CalculateDragElementRect(double newHorizOffset, double newVertOffset)
//        {
//            if (this.ElementBeingDragged == null)
//                throw new InvalidOperationException("ElementBeingDragged is null.");

//            Size elemSize = this.ElementBeingDragged.RenderSize;

//            double x, y;
//            if (this.modifyLeftOffset)
//            {
//                x = newHorizOffset;
//            }
//            else
//            {
//                x = this.ActualWidth - newHorizOffset - elemSize.Width;
//            }

//            if (this.modifyTopOffset)
//            {
//                y = newVertOffset;
//            }
//            else
//            {
//                y = this.ActualHeight - newVertOffset - elemSize.Height;
//            }
//            Point elemLoc = new Point(x, y);

//            return new Rect(elemLoc, elemSize);
//        }

//        #endregion

//        #region ResolveOffset

//        private static double ResolveOffset(double side1, double side2, out bool useSide1)
//        {
//            useSide1 = true;
//            double result;
//            if (Double.IsNaN(side1))
//            {
//                if (Double.IsNaN(side2))
//                {
//                    result = 0;
//                }
//                else
//                {
//                    result = side2;
//                    useSide1 = false;
//                }
//            }
//            else
//            {
//                result = side1;
//            }
//            return result;
//        }

//        #endregion

//        #endregion

//        #region FindCanvasChild

//        public UIElement FindCanvasChild(DependencyObject depObj)
//        {
//            while (depObj != null)
//            {
//                UIElement elem = depObj as UIElement;
//                if (elem != null && base.Children.Contains(elem))
//                {
//                    break;
//                }
//                if (depObj is Visual || depObj is Visual3D)
//                {
//                    depObj = VisualTreeHelper.GetParent(depObj);
//                }
//                else
//                {
//                    depObj = LogicalTreeHelper.GetParent(depObj);
//                }
//            }
//            return depObj as UIElement;
//        }

//        #endregion

//        #region UpdateZOrder

//        public void BringToFront(UIElement element)
//        {
//            this.UpdateZOrder(element, true);
//        }

//        public void SendToBack(UIElement element)
//        {
//            this.UpdateZOrder(element, false);
//        }

//        private void UpdateZOrder(UIElement element, bool bringToFront)
//        {
//            if (element == null || !base.Children.Contains(element))
//            {
//                return;
//            }

//            int elementNewZIndex = -1;
//            if (bringToFront)
//            {
//                foreach (UIElement elem in base.Children)
//                {
//                    if (elem.Visibility != Visibility.Collapsed)
//                    {
//                        ++elementNewZIndex;
//                    }
//                }
//            }
//            else
//            {
//                elementNewZIndex = 0;
//            }

//            int offset = (elementNewZIndex == 0) ? +1 : -1;
//            int elementCurrentZIndex = Canvas.GetZIndex(element);
//            foreach (UIElement childElement in base.Children)
//            {
//                if (childElement == element)
//                    Canvas.SetZIndex(element, elementNewZIndex);
//                else
//                {
//                    int zIndex = Canvas.GetZIndex(childElement);
//                    if (bringToFront && elementCurrentZIndex < zIndex ||
//                        !bringToFront && zIndex < elementCurrentZIndex)
//                    {
//                        Canvas.SetZIndex(childElement, zIndex + offset);
//                    }
//                }
//            }
//        }

//        #endregion

//        public bool CheckSelected(IActionControl ac)
//        {
//            bool recheck = ac == null ? false : !ac.IsSelected;
//            GetActionControls().ForEach(i => i.IsSelected = i.Equals(ac));
//            return recheck;
//        }

//        public List<IActionControl> GetActionControls()
//        {
//            List<IActionControl> ics = new List<IActionControl>();
//            foreach (var item in base.Children)
//            {
//                var iactioncontrol = item as IActionControl;
//                if (iactioncontrol != null)
//                {
//                    ics.Add(iactioncontrol);
//                }
//            }
//            return ics;
//        }

//        private void SetDragImage(bool candrag)
//        {
//            if (candrag)
//            {
//                this.DragViewControl.Visibility = System.Windows.Visibility.Visible;
//                this.ElementBeingDragged = this.DragViewControl as UIElement;
//            }
//        }

//    }
//}
