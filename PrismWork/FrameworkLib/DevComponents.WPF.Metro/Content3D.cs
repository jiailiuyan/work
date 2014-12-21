using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    internal class Content3D : ContentControl
    {
        static Content3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Content3D), new FrameworkPropertyMetadata(typeof(Content3D)));
        }

        private ContentPresenter _ContentPresenter;
        private Rectangle2D3D _Rectangle3D;


        /// <summary>
        /// Using a DependencyProperty as the backing store for IsThreeDVisualState.
        /// </summary>
        public static readonly DependencyProperty IsThreeDVisualStateProperty =
            DependencyProperty.RegisterAttached("IsThreeDVisualState", typeof(bool), typeof(Content3D), new UIPropertyMetadata(false));
        public static bool GetIsThreeDVisualState(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsThreeDVisualStateProperty);
        }
        public static void SetIsThreeDVisualState(DependencyObject obj, bool value)
        {
            obj.SetValue(IsThreeDVisualStateProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Is3DStoryboard.
        /// </summary>
        public static readonly DependencyProperty Is3DStoryboardProperty =
            DependencyProperty.RegisterAttached("Is3DStoryboard", typeof(bool), typeof(Content3D), new UIPropertyMetadata(false));
        public static bool GetIs3DStoryboard(DependencyObject obj)
        {
            return (bool)obj.GetValue(Is3DStoryboardProperty);
        }
        public static void SetIs3DStoryboard(DependencyObject obj, bool value)
        {
            obj.SetValue(Is3DStoryboardProperty, value);
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ThreeDBoundsX.
        /// </summary>
        public static readonly DependencyProperty ThreeDBoundsXProperty =
            DependencyProperty.Register("ThreeDBoundsX", typeof(double), typeof(Content3D), new UIPropertyMetadata(0d));
        public double ThreeDBoundsX
        {
            get { return (double)GetValue(ThreeDBoundsXProperty); }
            set { SetCurrentValue(ThreeDBoundsXProperty, value); }
        }
        /// <summary>
        // Using a DependencyProperty as the backing store for ThreeDBoundsX.
        /// </summary>
        public static readonly DependencyProperty ThreeDBoundsYProperty =
            DependencyProperty.Register("ThreeDBoundsY", typeof(double), typeof(Content3D), new UIPropertyMetadata(0d));
        public double ThreeDBoundsY
        {
            get { return (double)GetValue(ThreeDBoundsYProperty); }
            set { SetCurrentValue(ThreeDBoundsYProperty, value); }
        }
        /// <summary>
        // Using a DependencyProperty as the backing store for ThreeDBoundsX.
        /// </summary>
        public static readonly DependencyProperty ThreeDBoundsZProperty =
            DependencyProperty.Register("ThreeDBoundsZ", typeof(double), typeof(Content3D), new UIPropertyMetadata(0d));
        public double ThreeDBoundsZ
        {
            get { return (double)GetValue(ThreeDBoundsZProperty); }
            set { SetCurrentValue(ThreeDBoundsZProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Transform3D.
        /// </summary>
        public static readonly DependencyProperty Transform3DProperty =
            DependencyProperty.Register("Transform3D", typeof(Transform3D), typeof(Content3D), new UIPropertyMetadata(null));
        public Transform3D Transform3D
        {
            get { return (Transform3D)GetValue(Transform3DProperty); }
            set { SetCurrentValue(Transform3DProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _ContentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;
            _Rectangle3D = GetTemplateChild("Rectangle3D") as Rectangle2D3D;
            if (_Rectangle3D != null)
            {
                _Rectangle3D.Material = new DiffuseMaterial();
            }

            SetupVisualStateGroupEventHandlers();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (_Rectangle3D != null)
            {
                Dispatcher.BeginInvoke((Action)delegate
                {
                    ThreeDBoundsX = _Rectangle3D[0].Geometry.Bounds.X;
                    ThreeDBoundsY = _Rectangle3D[0].Geometry.Bounds.Y;
                    ThreeDBoundsZ = _Rectangle3D[0].Geometry.Bounds.Z;
                }, DispatcherPriority.Render);
            }
        }

        private void SetupVisualStateGroupEventHandlers()
        {
            var groups = VisualStateManager.GetVisualStateGroups(this);
            if (groups.Count == 0 && TemplatedParent != null)
                groups = VisualStateManager.GetVisualStateGroups(((FrameworkElement)TemplatedParent).GetFirstDescendent<FrameworkElement>());
            foreach (VisualStateGroup group in groups)
            {
                group.CurrentStateChanged += HandleVisualStateChanged;
                foreach (VisualState state in group.States)
                {
                    if (state.Storyboard != null)
                        this.AddLogicalChild(state.Storyboard);
                }
            }
        }

        private void HandleVisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            var state = e.NewState;

            bool isThreeDState = GetIsThreeDVisualState(state);
            bool isThreeDStoryboard = false;

            if (isThreeDState)
                PrepareFor3DAnimation();

            if (state.Storyboard != null)
            {
                isThreeDStoryboard = GetIs3DStoryboard(state.Storyboard);
                if (isThreeDStoryboard && !isThreeDState)
                {
                    state.Storyboard.Completed += delegate { Complete3DAnimation(); };
                    PrepareFor3DAnimation();
                }
            }

            if (!isThreeDState && !isThreeDStoryboard)
                Complete3DAnimation();
        }

        private bool _Is3DAnimationRunning;

        private void Complete3DAnimation()
        {
            _ContentPresenter.Visibility = Visibility.Visible;
            _Rectangle3D.Visibility = Visibility.Hidden;
            _Is3DAnimationRunning = false;
        }

        private void PrepareFor3DAnimation()
        {
            if (_Is3DAnimationRunning)
                return;

            if (_Rectangle3D == null || _ContentPresenter == null)
                return;

            var diffuseMaterial = (DiffuseMaterial)_Rectangle3D.Material;
            diffuseMaterial.Brush = TargetToVisualBrush(_ContentPresenter);

            _ContentPresenter.Visibility = Visibility.Hidden;
            _Rectangle3D.Visibility = Visibility.Visible;

            _Is3DAnimationRunning = true;
        }

        private VisualBrush TargetToVisualBrush(UIElement target)
        {
            var visibility = target.Visibility;
            target.Visibility = Visibility.Visible;
            var image = target.ToImage();
            target.Visibility = visibility;

            // Wrap the image in a border which has padding so as to offset the positional difference
            // between main content and the threeD rectangle which is performing the animation. 
            // Setting transparent background is necessary or the padding is ignored for some reason.
            var bd = new Border { Child = image, Background = Brushes.Transparent };
            var pos = target.TranslatePoint(new Point(0, 0), _Rectangle3D);

            bd.Padding = new Thickness(pos.X, pos.Y, _Rectangle3D.ActualWidth - target.RenderSize.Width - pos.X, _Rectangle3D.ActualHeight - target.RenderSize.Height - pos.Y);
            var visualBrush = new VisualBrush(bd);
            return visualBrush;
        }
    }
}
