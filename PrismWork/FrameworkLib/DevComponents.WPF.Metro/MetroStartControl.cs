using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevComponents.WPF.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// A MetroControl with addition of sliding Tiles view - the start page is able to slide in and out of view, independently of the items in the TransitioningSelector. 
    /// This control is meant to be used in conjunction with MetroShell for adding Metro style start view to MetroShell.
    /// </summary>
    [TemplatePart(Name = "ThreeDRectangle", Type = typeof(Rectangle2D3D))]
    [TemplatePart(Name = "TileView", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "TransitioningSelector", Type = typeof(TransitioningSelector))]
    [DesignTimeVisible(false)]
    public class MetroStartControl : MetroControl
    {
        public static readonly RoutedUICommand DockCommand = new RoutedUICommand("Dock", "DockCommand", typeof(MetroStartControl));

        static MetroStartControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroStartControl), new FrameworkPropertyMetadata(typeof(MetroStartControl)));
        }

        public MetroStartControl()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MetroStartControl.DockCommand, DockCommandExecuted, DockCommandCanExecute));
        }

        #region Clr Properties

        /// <summary>
        /// Returns true if the start control content is fully in position, whether Docked or Undocked.
        /// </summary>
        public bool IsInPosition
        {
            get
            {
                var slider = GetTemplateChild("Slider") as SlidingContentControl;
                if (slider != null)
                    return slider.IsInPosition;
                return true;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Using a RoutedEvent as backing for Docked.
        /// </summary>
        public static readonly RoutedEvent DockedEvent =
            EventManager.RegisterRoutedEvent("Docked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroStartControl));
        /// <summary>
        /// Event raised when the value of IsDocked changes to true. This is a routed event.
        /// </summary>
        public event RoutedEventHandler Docked
        {
            add { AddHandler(DockedEvent, value); }
            remove { RemoveHandler(DockedEvent, value); }
        }

        /// <summary>
        /// Using a RoutedEvent as backing for Undocked.
        /// </summary>
        public static readonly RoutedEvent UndockedEvent =
            EventManager.RegisterRoutedEvent("Undocked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroStartControl));
        /// <summary>
        /// Routed event raised when the value of IsDocked changes to false.
        /// </summary>
        public event RoutedEventHandler Undocked
        {
            add { AddHandler(UndockedEvent, value); }
            remove { RemoveHandler(UndockedEvent, value); }
        }

        #endregion // Events

        #region Dependency Properties

        /// <summary>
        // Using a DependencyProperty as the backing store for DockingSide.
        /// </summary>
        public static readonly DependencyProperty DockingSideProperty =
            DependencyProperty.Register("DockingSide", typeof(Dock), typeof(MetroStartControl), new UIPropertyMetadata(Dock.Right));
        /// <summary>
        /// Get or set the side to which the control's contents slide when docked.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines the side to which the control's contents slide when docked")]
        public Dock DockingSide
        {
            get { return (Dock)GetValue(DockingSideProperty); }
            set { SetCurrentValue(DockingSideProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for DockOnClick.
        /// </summary>
        public static readonly DependencyProperty DockOnClickProperty =
            DependencyProperty.Register("DockOnClick", typeof(bool), typeof(MetroStartControl), new UIPropertyMetadata(true));
        /// <summary>
        /// Get or set whether the StartControl docks itself when clicked on outside of a MetroTile. Default value is true.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines whether the StartControl docks itself when clicked on outside of a MetroTile")]
        public bool DockOnClick
        {
            get { return (bool)GetValue(DockOnClickProperty); }
            set { SetCurrentValue(DockOnClickProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsDocked.
        /// </summary>
        public static readonly DependencyProperty IsDockedProperty =
            DependencyProperty.Register("IsDocked", typeof(bool), typeof(MetroStartControl), new UIPropertyMetadata(false,
                (s, e) =>
                {
                    ((MetroStartControl)s).HandleIsDockedChanged((bool)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets whether the StartControl's content are docked - i.e. out of view, off to one side.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines whether the StartControl's content are docked - i.e. out of view, off to one side")]
        public bool IsDocked
        {
            get { return (bool)GetValue(IsDockedProperty); }
            set { SetCurrentValue(IsDockedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for MetroAppAnimator.
        /// </summary>
        public static readonly DependencyProperty MetroAppAnimatorProperty =
            DependencyProperty.Register("MetroAppAnimator", typeof(TransitionAnimationProvider), typeof(MetroStartControl), new UIPropertyMetadata(null,
                (s, e) =>
                {
                    ((MetroStartControl)s).HandleMetroAppAnimatorChanged((TransitionAnimationProvider)e.OldValue, (TransitionAnimationProvider)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets an animation provider to be used to animate the main application view when the MetroStartControl
        /// is docked and undocked.
        /// </summary>
        public TransitionAnimationProvider MetroAppAnimator
        {
            get { return (TransitionAnimationProvider)GetValue(MetroAppAnimatorProperty); }
            set { SetCurrentValue(MetroAppAnimatorProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ShowSlidThumbWhenDocked.
        /// </summary>
        public static readonly DependencyProperty ShowSlideThumbWhenDockedProperty = SlidingContentControl.ShowSlideThumbWhenDockedProperty.AddOwner(typeof(MetroStartControl), new UIPropertyMetadata(true));
        public bool ShowSlideThumbWhenDocked
        {
            get { return (bool)GetValue(ShowSlideThumbWhenDockedProperty); }
            set { SetCurrentValue(ShowSlideThumbWhenDockedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Placement.
        /// </summary>
        internal static readonly DependencyProperty SlidingContentControlStateProperty =
            DependencyProperty.Register("SlidingContentControlState", typeof(SlidingContentControlState), typeof(MetroStartControl), new UIPropertyMetadata(SlidingContentControlState.Normal,
                (s, e) =>
                {
                    ((MetroStartControl)s).HandleSlidingContentControlStateChanged((SlidingContentControlState)e.NewValue);
                }));
        internal SlidingContentControlState SlidingContentControlState
        {
            get { return (SlidingContentControlState)GetValue(SlidingContentControlStateProperty); }
            set { SetCurrentValue(SlidingContentControlStateProperty, value); }
        }


        #endregion // Dependency Properties

        #region Overrides

        private MetroShell _ParentShell;

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ParentShell = this.GetVisualParent<MetroShell>();
            if (_ParentShell == null)
            {
                // Enables the start control to be outside of the MetroShell and have the 3-D animation still work.
                var parent = this.GetVisualParent<Grid>();
                if (parent != null)
                    _ParentShell = parent.GetFirstDescendent<MetroShell>();
            }

            SetupMetroAppAnimator(MetroAppAnimator);
            GoToDockedState(IsDocked);
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!DockOnClick)
                return;

            // We'll dock on mouse left button down only if the mouse position is not 
            // on top of an element contained by this, or if the element is a MetroStartPage,
            // don't dock if on top of element contained by it.
            bool dock = false;
            var element = GetElementAtMousePosition(this, e);
            if (element != null)
            {
                var page = element as MetroStartPage;
                if (page != null)
                {
                    var pageElement = GetElementAtMousePosition(page, e);
                    if (pageElement == null)
                        dock = true;
                }
            }
            else
            {
                dock = true;
            }

            if (dock)
                IsDocked = true;
        }

        /// <summary>
        /// Overriding to handle case where Tile view has been slid out of view to make way for 
        /// App view. In this case the App view is animated to make way for transitioning selector item.
        /// </summary>
        protected override void AnimateTileView(bool makeVisible, Dock? side)
        {
            if (!IsDocked)
                base.AnimateTileView(makeVisible, side);
            else
            {
                AnimateMetroAppView(makeVisible, side);
            }
        }

        #endregion // Overrides

        #region Protected

        /// <summary>
        /// Virtual method called to raise the Docked event.
        /// </summary>
        protected virtual void OnDocked()
        {
            var args = new RoutedEventArgs(DockedEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// Virtual method called to raise the Undocked event.
        /// </summary>
        protected virtual void OnUndocked()
        {
            var args = new RoutedEventArgs(UndockedEvent, this);
            RaiseEvent(args);
        }

        #endregion // Protected

        #region Private

        private void DockCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            IsDocked = true;
        }

        private void DockCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
            e.CanExecute = !IsDocked;
        }

        private void SetupMetroAppAnimator(TransitionAnimationProvider animator)
        {
            if (animator == null || _ParentShell == null)
                return;

            animator.Bind(TransitionAnimationProvider.DurationProperty, this, AnimationDurationProperty);

            // Metro app here is basically MetroShell. It is content of MetroShell which is being animated. 
            // The animation logic has been located here in order to keep all animation properties
            // with MetroStartControl and because without MetroStartControl there would be no need to 
            // animate MetroShell content.
            var threeDAnimator = animator as TransitionAnimation3DProvider;
            if (threeDAnimator != null)
            {
                var threeDRectangle = _ParentShell.GetFirstDescendent<Rectangle2D3D>();
                if (threeDRectangle != null)
                    threeDAnimator.Container = threeDRectangle;
                else
                    threeDAnimator.Container = _ParentShell.GetFirstDescendent<Grid>();
            }
            else
            {
                animator.Container = _ParentShell.GetFirstDescendent<Grid>();
            }
           
            animator.Initialize();
            animator.LeavingViewCompleted += MetroAppAnimator_LeavingViewCompleted;
            animator.EnteringViewCompleted += MetroAppAnimator_EnteringViewCompleted;
        }

        private void MetroAppAnimator_EnteringViewCompleted(object sender, TransitionAnimationProvider.AnimationCompleteEventArgs e)
        {
            e.Target.Visibility = Visibility.Visible;
            ShowSlideThumbWhenDocked = _ShowSlideThumbWhenDockedInternal.Value;
        }

        private void MetroAppAnimator_LeavingViewCompleted(object sender, TransitionAnimationProvider.AnimationCompleteEventArgs e)
        {
            e.Target.Visibility = Visibility.Hidden;
        }

        private void HandleMetroAppAnimatorChanged(TransitionAnimationProvider oldAnimator, TransitionAnimationProvider newAnimator)
        {
            if (oldAnimator != null)
            {
                oldAnimator.LeavingViewCompleted -= MetroAppAnimator_LeavingViewCompleted;
                oldAnimator.EnteringViewCompleted -= MetroAppAnimator_EnteringViewCompleted;
            }
            SetupMetroAppAnimator(newAnimator);
        }

        private UIElement GetElementAtMousePosition(ItemsControl itemsControl, MouseButtonEventArgs e)
        {
            foreach (var element in itemsControl.GetItemContainers().OfType<UIElement>())
            {
                var elementPosition = e.GetPosition(element);
                if (element.IsPointWithin(e.GetPosition(element)))
                {
                    return element;
                }
            }

            return null;
        }

        private void HandleSlidingContentControlStateChanged(SlidingContentControlState newState)
        {
            if (newState == SlidingContentControlState.Normal)
                IsDocked = false;
            else
                IsDocked = true;
        }

        private void HandleIsDockedChanged(bool isDocked)
        {
            GoToDockedState(isDocked);

            if (isDocked)
            {
                DockControl();
                AnimateMetroAppView(true, GetOpposingSide(DockingSide));
                OnDocked();
            }
            else
            {
                AnimateMetroAppView(false, GetOpposingSide(DockingSide));
                SlidingContentControlState = SlidingContentControlState.Normal;
                OnUndocked();
            }
        }

        private void GoToDockedState(bool isDocked)
        {
            VisualStateManager.GoToState(this, isDocked ? "Docked" : "Undocked", true);
            if (_ParentShell != null)
                VisualStateManager.GoToState(_ParentShell, isDocked ? "StartDocked" : "StartUndocked", true);
        }

        private void DockControl()
        {
            if (DockingSide == Dock.Right)
                SlidingContentControlState = SlidingContentControlState.DockedRight;
            else if (DockingSide == Dock.Left)
                SlidingContentControlState = SlidingContentControlState.DockedLeft;
            else if (DockingSide == Dock.Top)
                SlidingContentControlState = SlidingContentControlState.DockedTop;
            else if (DockingSide == Dock.Bottom)
                SlidingContentControlState = SlidingContentControlState.DockedBottom;
        }

        private bool? _ShowSlideThumbWhenDockedInternal;

        private void AnimateMetroAppView(bool makeVisible, Dock? side = null)
        {
            if (!IsLoaded || _ParentShell == null)
                return;

            var animator = MetroAppAnimator;
            if (animator == null)
                return;

            var appView = _ParentShell.AppView;
            if (appView == null)
                return;

            if (side.HasValue)
                animator.Side = side.Value;

            if (!_ShowSlideThumbWhenDockedInternal.HasValue)
                _ShowSlideThumbWhenDockedInternal = ShowSlideThumbWhenDocked;

            if (!makeVisible && ShowSlideThumbWhenDocked)
            {
                _ShowSlideThumbWhenDockedInternal = true;
                ShowSlideThumbWhenDocked = false;
            }

            if (makeVisible)
                animator.AnimateEnteringView(appView);
            else
                animator.AnimateLeavingView(appView);
        }

        #endregion // Private
    }
}
