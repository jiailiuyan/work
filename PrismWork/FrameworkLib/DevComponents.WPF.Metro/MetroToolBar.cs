using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of MetroToolbar.
    /// </summary>
    public class MetroToolBar : ItemsControl
    {
        #region Construction / Fields

        public static readonly ComponentResourceKey MetroToolBarButtonStyleKey = new ComponentResourceKey(typeof(MetroToolBar), "MetroToolBarButtonStyleKey");

        /// <summary>
        /// Identifies the default context menu for MetroToolbar.
        /// </summary>
        public static readonly ComponentResourceKey ContextMenuKey = new ComponentResourceKey(typeof(MetroToolBar), "ContextMenuKey");

        static MetroToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroToolBar), new FrameworkPropertyMetadata(typeof(MetroToolBar)));
            ContextMenuProperty.OverrideMetadata(typeof(MetroToolBar), new FrameworkPropertyMetadata(null,
                (s, e) =>
                {
                    var tb = (MetroToolBar)s;
                    var cm = e.NewValue as ContextMenu;
                    // Set flag.
                    tb._UsingDefaultContextMenu = cm != null && cm == tb.TryFindResource(ContextMenuKey);
                }));
        }

        private Canvas _ResizableCanvas;
        private Button _ExpandButton;
        private bool _UsingDefaultContextMenu;

        #endregion // Construction / Fields

        #region CLR Properties

        /// <summary>
        /// Provides access to Items used to populate the ItemsControl which displays the Extra Items. 
        /// If ExtraItemsSource has been set then this property will return it (cast to IList) otherwise,
        /// a new ObservableCollection is created and returned. This new collection is also set as the value
        /// of ExtraItemsSource. The primary reason for this property is to facilitate building the collection in xaml.
        /// </summary>
        public IList ExtraItems
        {
            get
            {
                if (ExtraItemsSource == null)
                {
                    ExtraItemsSource = new ObservableCollection<object>();
                }
                return ExtraItemsSource as IList;
            }
        }

        #endregion // CLR Properties

        #region Events

        /// <summary>
        /// Using a RoutedEvent for the Expanded event.
        /// </summary>
        public static readonly RoutedEvent ExpandedEvent =
            EventManager.RegisterRoutedEvent("Expanded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroToolBar));
        /// <summary>
        /// Routed event raised when the value of IsExpanded changes to True.
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }
        /// <summary>
        /// Using a RoutedEvent as backing for Collapsed.
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent =
            EventManager.RegisterRoutedEvent("Collapsed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroToolBar));
        /// <summary>
        /// Routed event raised when the value of IsExpanded changes to False.
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        #endregion // Events

        #region Dependency Properties

        /// <summary>
        // Using a DependencyProperty as the backing store for AutoCollapse.
        /// </summary>
        public static readonly DependencyProperty AutoCollapseProperty =
            DependencyProperty.Register("AutoCollapse", typeof(bool), typeof(MetroToolBar), new UIPropertyMetadata(true));
        /// <summary>
        /// Get or set whether IsExpanded is changed from True to false when button is clicked within it or when toolbar loses inupt focus.
        /// </summary>
        public bool AutoCollapse
        {
            get { return (bool)GetValue(AutoCollapseProperty); }
            set { SetCurrentValue(AutoCollapseProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ExpandDirection.
        /// </summary>
        public static readonly DependencyProperty ExpandDirectionProperty =
            DependencyProperty.Register("ExpandDirection", typeof(MetroToolBarExpandDirection), typeof(MetroToolBar), new UIPropertyMetadata(MetroToolBarExpandDirection.Auto));
        /// <summary>
        /// Get or set the direction in which the toolbar expands when IsExpanded is set to true.
        /// </summary>
        public MetroToolBarExpandDirection ExpandDirection
        {
            get { return (MetroToolBarExpandDirection)GetValue(ExpandDirectionProperty); }
            set { SetCurrentValue(ExpandDirectionProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ExtraItemsSource.
        /// </summary>
        public static readonly DependencyProperty ExtraItemsSourceProperty =
            DependencyProperty.Register("ExtraItemsSource", typeof(IEnumerable), typeof(MetroToolBar), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set the collection of items which are displayed when IsExpanded is true.
        /// </summary>
        public IEnumerable ExtraItemsSource
        {
            get { return (IEnumerable)GetValue(ExtraItemsSourceProperty); }
            set { SetCurrentValue(ExtraItemsSourceProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsExpanded.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(MetroToolBar), new UIPropertyMetadata(false,
                (s, e) =>
                {
                    ((MetroToolBar)s).HandleIsExpandedChanged((bool)e.NewValue);
                }));

        /// <summary>
        /// Gets or sets whether the toolbar is expanded. When expanded, the extra items are visible. This is a dependency property. The default value is false.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetCurrentValue(IsExpandedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for PopupVerticalOffset.
        /// </summary>
        internal static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(MetroToolBar));
        internal double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetCurrentValue(VerticalOffsetProperty, value); }
        }

        #endregion // Dependency Properties

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ExpandButton = GetTemplateChild("ExpandButton") as Button;
            if (_ExpandButton != null)
            {
                _ExpandButton.Click += delegate
                {
                    IsExpanded = !IsExpanded;
                };
            }

            _ResizableCanvas = GetTemplateChild("ResizableCanvas") as Canvas;

        }

        #endregion // Overrides

        #region Protected

        /// <summary>
        /// Virtual method raises Expanded event.
        /// </summary>
        protected virtual void OnExpanded()
        {
            var args = new RoutedEventArgs(ExpandedEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// Virtual method raises Collapsed event.
        /// </summary>
        protected virtual void OnCollapsed()
        {
            var args = new RoutedEventArgs(CollapsedEvent, this);
            RaiseEvent(args);
        }

        #endregion // Protected

        #region Private

        private void HandleIsExpandedChanged(bool isExpanded)
        {
            if (isExpanded)
            {
                Expand();
                OnExpanded();
            }
            else
            {
                Collapse();
                OnCollapsed();
            }
        }

        private double GetExtraItemsHeight()
        {
            if (_ResizableCanvas != null && _ResizableCanvas.Children.Count > 0)
                return ((FrameworkElement)_ResizableCanvas.Children[0]).ActualHeight;
            return 0;
        }

        private MetroToolBarExpandDirection GetEffectiveExpandDirection()
        {
            var direction = ExpandDirection;
            if (direction != MetroToolBarExpandDirection.Auto)
                return direction;

            var window = Window.GetWindow(this);
            if (window == null) return MetroToolBarExpandDirection.Down;

            var midPoint = TranslatePoint(new Point(0, ActualHeight / 2), window);

            if (midPoint.Y < window.ActualHeight / 2)
                return MetroToolBarExpandDirection.Down;
            return MetroToolBarExpandDirection.Up;
        }

        private void Expand()
        {
            // NOTE: Running animation via code instead of Xaml because of issues binding the To property of DoubleAnimation.

            if (_ResizableCanvas == null)
                return;

            var duration = new Duration(TimeSpan.FromSeconds(0.25));
            var storyboard = new Storyboard();

            var expandDirection = GetEffectiveExpandDirection();
            if (expandDirection == MetroToolBarExpandDirection.Up)
            {
                DoubleAnimation offsetAnimation = new DoubleAnimation
                    {
                        Duration = duration,
                        From = 0,
                        To = -GetExtraItemsHeight(),
                    };
                Storyboard.SetTarget(offsetAnimation, this);
                Storyboard.SetTargetProperty(offsetAnimation, new PropertyPath(VerticalOffsetProperty));
                storyboard.Children.Add(offsetAnimation);
            }

            var extraItems = _ResizableCanvas.Children[0] as FrameworkElement;
            var heightAnimation = new DoubleAnimation
            {
                Duration = duration,
                To = GetExtraItemsHeight(),
                From = 0
            };
            Storyboard.SetTarget(heightAnimation, _ResizableCanvas);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));

            storyboard.Children.Add(heightAnimation);

            storyboard.Begin();

            if (AutoCollapse)
            {
                var window = Window.GetWindow(this);
                window.Deactivated += HandleWindowDeactivated;
                window.AddHandler(MouseRightButtonDownEvent, new MouseButtonEventHandler(HandleMouseButtonEvent), true);
                window.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(HandleMouseButtonEvent), true);
                window.AddHandler(ChromelessWindow.ResizeBorderClickEvent, new RoutedEventHandler(HandleWindowResizeBorderClick));
                AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(HandleMouseButtonEvent), true);
            }
        }

        private void Collapse()
        {
            if (_ResizableCanvas == null)
                return;

            if (AutoCollapse)
                RemoveAutoCollapseEventHandlers();

            var duration = new Duration(TimeSpan.FromSeconds(0.25));

            double expandedVerticalOffset = -ActualHeight;
            DoubleAnimation offsetAnimation = new DoubleAnimation
            {
                Duration = duration,
                To = 0,
            };
            Storyboard.SetTarget(offsetAnimation, this);
            Storyboard.SetTargetProperty(offsetAnimation, new PropertyPath(VerticalOffsetProperty));

            var heightAnimation = new DoubleAnimation
            {
                Duration = duration,
                To = 0,
            };
            Storyboard.SetTarget(heightAnimation, _ResizableCanvas);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(offsetAnimation);
            storyboard.Children.Add(heightAnimation);

            storyboard.Begin();
        }

        private void HandleMouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            var border = GetTemplateChild("Border") as FrameworkElement;
            var pos = e.GetPosition(border);

            if (e.RoutedEvent == MouseLeftButtonDownEvent)
            {
                if (pos.X < 0 || pos.Y < 0 || pos.X > border.ActualWidth || pos.Y > border.ActualHeight)
                {
                    IsExpanded = false;
                }
            }
            else if (e.RoutedEvent == MouseLeftButtonUpEvent)
            {
                if (e.Handled && e.OriginalSource != _ExpandButton)
                {
                    IsExpanded = false;
                }
            }
        }

        private void HandleWindowResizeBorderClick(object sender, RoutedEventArgs e)
        {
            IsExpanded = false;
        }

        private void HandleWindowDeactivated(object sender, EventArgs e)
        {
            IsExpanded = false;
        }

        private void RemoveAutoCollapseEventHandlers()
        {
            RemoveHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(HandleMouseButtonEvent));

            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Deactivated -= HandleWindowDeactivated;
                window.RemoveHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(HandleMouseButtonEvent));
                window.RemoveHandler(MouseRightButtonDownEvent, new MouseButtonEventHandler(HandleMouseButtonEvent));
                window.RemoveHandler(ChromelessWindow.ResizeBorderClickEvent, new RoutedEventHandler(HandleWindowResizeBorderClick));
            }
        }

        #endregion // Private
    }

}
