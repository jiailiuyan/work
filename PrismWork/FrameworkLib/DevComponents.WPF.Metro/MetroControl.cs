using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of a control which contains pages of MetroTiles (i.e. MetroStartPage) and has a built-in TransitioningSelectorControl
    /// which is used to bring different UI elements into the main view, using animation. Can be used to simulate Metro UI in Windows 8.
    /// </summary>
    [TemplatePart(Name = "ThreeDRectangle", Type = typeof(Rectangle2D3D))]
    [TemplatePart(Name = "TileView", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "TransitioningSelector", Type = typeof(TransitioningSelector))]
    public class MetroControl : ItemsControl
    {
        #region Construction, Fields

        public static readonly ComponentResourceKey DragAdornerDataTemplateKey = new ComponentResourceKey(typeof(MetroControl), "DragAdornerDataTemplateKey");

        static MetroControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroControl), new FrameworkPropertyMetadata(typeof(MetroControl)));
        }

        private MetroControlDragDropHelper _DragDropHelper;
        private Storyboard _PagePositioningStoryboard = new Storyboard();

        public MetroControl()
        {
            Loaded += delegate
            {
                SetPageCanvasPositions();
                SetPagePositionStates();
                SetupTileViewAnimator(TileViewAnimator);
            };
        }

        /// <summary>
        /// Returns an enumeration of MetroStartPages being the containers of the items in the Items collection.
        /// </summary>
        public IEnumerable<MetroStartPage> Pages
        {
            get
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var page = ItemContainerGenerator.ContainerFromIndex(i) as MetroStartPage;
                    if (page != null)
                        yield return page;
                }
            }
        }

        private IList _TransitioningSelectorItems;
        /// <summary>
        /// Gets a reference to the list of items in the TransitioningSelector.
        /// </summary>
        public IList TransitioningSelectorItems
        {
            get
            {
                if (_TransitioningSelectorItems == null)
                {
                    _TransitioningSelectorItems = new ObservableCollection<TransitioningSelectorItem>();
                    TransitioningSelectorItemsSource = _TransitioningSelectorItems;
                }
                return _TransitioningSelectorItems;
            }
        }

        #endregion // Construction

        #region Dependency Properties

        // Using a DependencyProperty as the backing store for CanUserCreateNewStartPage.
        public static readonly DependencyProperty CanUserCreateNewStartPageProperty =
            DependencyProperty.Register("CanUserCreateNewStartPage", typeof(bool), typeof(MetroControl), new FrameworkPropertyMetadata(true));
        /// <summary>
        /// Get or sets whether the user is able to create new Start pages by dragging a tile out of its current page.
        /// This is a dependency property. The default value is true.
        /// </summary>
        public bool CanUserCreateNewStartPage
        {
            get { return (bool)GetValue(CanUserCreateNewStartPageProperty); }
            set { SetValue(CanUserCreateNewStartPageProperty, value); }
        }
        
        /// <summary>
        // Using a DependencyProperty as the backing store for AnimationDuration.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(Duration), typeof(MetroControl), new UIPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.4))));
        /// <summary>
        /// Get or set the duration of the slide animation.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines the duration of the slide animation")]
        public Duration AnimationDuration
        {
            get { return (Duration)GetValue(AnimationDurationProperty); }
            set { SetCurrentValue(AnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentSelection.
        public static readonly DependencyProperty CurrentSelectionProperty =
            DependencyProperty.Register("CurrentSelection", typeof(object), typeof(MetroControl), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// Get or set the current selection. Normally, when user clicks a MetroTile, the tile's command will set this value.
        /// </summary>
        public object CurrentSelection
        {
            get { return (object)GetValue(CurrentSelectionProperty); }
            set { SetValue(CurrentSelectionProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Identity.
        /// </summary>
        public static readonly DependencyProperty IdentityProperty =
            DependencyProperty.Register("Identity", typeof(MetroIdentity), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Gets or Sets an Identity or Current User for the control.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Represents current user. This is a wrapper for FirstName, LastName and ImageSource")]
        public MetroIdentity Identity
        {
            get { return (MetroIdentity)GetValue(IdentityProperty); }
            set { SetCurrentValue(IdentityProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IdentityTemplate.
        /// </summary>
        public static readonly DependencyProperty IdentityTemplateProperty =
            DependencyProperty.Register("IdentityTemplate", typeof(DataTemplate), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set a DataTemplate which shows how to render the Identity.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Shows how to render Identity")]
        public DataTemplate IdentityTemplate
        {
            get { return (DataTemplate)GetValue(IdentityTemplateProperty); }
            set { SetCurrentValue(IdentityTemplateProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ImageSource.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set the source for the image part of the current user identity.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The source for the image part of the current user identity")]
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetCurrentValue(ImageSourceProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsDragDropEnabled.
        /// </summary>
        public static readonly DependencyProperty IsDragDropEnabledProperty =
            DependencyProperty.Register("IsDragDropEnabled", typeof(bool), typeof(MetroControl), new UIPropertyMetadata(true));
        public bool IsDragDropEnabled
        {
            get { return (bool)GetValue(IsDragDropEnabledProperty); }
            set { SetCurrentValue(IsDragDropEnabledProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for FirstName.
        /// </summary>
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register("FirstName", typeof(string), typeof(MetroControl), new UIPropertyMetadata(null));

        /// <summary>
        /// Get or set the first name of the current user identity.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The first name of the current user identity")]
        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetCurrentValue(FirstNameProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for LastName.
        /// </summary>
        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set the last name of the current user identity.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The last name of the current user identity")]
        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetCurrentValue(LastNameProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for MainViewAnimator.
        /// </summary>
        public static readonly DependencyProperty TileViewAnimatorProperty =
            DependencyProperty.Register("TileViewAnimator", typeof(TransitionAnimationProvider), typeof(MetroControl), new UIPropertyMetadata(null,
                (s, e) =>
                {
                    ((MetroControl)s).HandleTileViewAnimatorChanged((TransitionAnimationProvider)e.OldValue, (TransitionAnimationProvider)e.NewValue);
                }));
        /// <summary>
        /// Get or set the animation provider which provides animation for the tile view.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Provides animation for Tiles view.")]
        public TransitionAnimationProvider TileViewAnimator
        {
            get { return (TransitionAnimationProvider)GetValue(TileViewAnimatorProperty); }
            set { SetCurrentValue(TileViewAnimatorProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Title.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set the title.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The title")]
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetCurrentValue(TitleProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TitleTemplate.
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(MetroControl), new UIPropertyMetadata(null));
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetCurrentValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// Using a dependency Property as the backing store for KeepContentLoadedOnceItemSelected.
        /// </summary>
        public static readonly DependencyProperty KeepContentLoadedOnceItemSelectedProperty = TransitioningSelector.KeepContentLoadedOnceItemSelectedProperty.AddOwner(typeof(MetroControl));
        /// <summary>
        /// Determines whether the visual elemnent used to display selected content should be kept in memory after the content item
        /// is deselected. By default, visual element which displays the selected content is unloaded when the item is unselected.
        /// This value is passed directly to the underlying TransitioningSelector.
        /// This is a dependency property, the default value it false.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("If true, elements displayed by the transitioning selector are not unloaded after being displayed then hidden.")]
        public bool KeepContentLoadedOnceItemSelected
        {
            get { return (bool)GetValue(KeepContentLoadedOnceItemSelectedProperty); }
            set { SetValue(KeepContentLoadedOnceItemSelectedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TransitioningSelectorItemsSource.
        /// </summary>
        public static readonly DependencyProperty TransitioningSelectorItemsSourceProperty =
            DependencyProperty.Register("TransitioningSelectorItemsSource", typeof(IEnumerable), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Bindable source for items in the embedded TransitioningSelector, which is used to present modal UI elements.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Items source for the embedded transitioning selector.")]
        public IEnumerable TransitioningSelectorItemsSource
        {
            get { return (IEnumerable)GetValue(TransitioningSelectorItemsSourceProperty); }
            set { SetCurrentValue(TransitioningSelectorItemsSourceProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TransitioningSelectorItemContainerStyle.
        /// </summary>
        public static readonly DependencyProperty TransitioningSelectorItemContainerStyleProperty =
            DependencyProperty.Register("TransitioningSelectorItemContainerStyle", typeof(Style), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// ItemContainerStyle for the embedded TransitioningSelector.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Item container style for the embedded transitioning selector.")]
        public Style TransitioningSelectorItemContainerStyle
        {
            get { return (Style)GetValue(TransitioningSelectorItemContainerStyleProperty); }
            set { SetCurrentValue(TransitioningSelectorItemContainerStyleProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for TransitioningViewAnimator.
        /// </summary>
        public static readonly DependencyProperty TransitioningViewAnimatorProperty =
            DependencyProperty.Register("TransitioningViewAnimator", typeof(TransitionAnimationProvider), typeof(MetroControl), new UIPropertyMetadata(null));
        /// <summary>
        /// Get or set the animation provider which animates the items in the embedded TransitioningSelector when they are selected and unselected.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Provides animation for the embedded transitioning selector.")]
        public TransitionAnimationProvider TransitioningViewAnimator
        {
            get { return (TransitionAnimationProvider)GetValue(TransitioningViewAnimatorProperty); }
            set { SetCurrentValue(TransitioningViewAnimatorProperty, value); }
        }

        #endregion // Dependency Properties

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MetroUI.EnsureTheme(this);

            var transitioningSelector = GetTemplateChild("TransitioningSelector") as TransitioningSelector;
            if (transitioningSelector != null)
            {
                transitioningSelector.SelectionChanged += HandleTransitioningSelectorSelectionChanged;
            }

            SetupIdentity();

            SetupDragDropHelper();

            var visualStateGroups = VisualStateManager.GetVisualStateGroups(this);
            var animationStateGroup = visualStateGroups.OfType<VisualStateGroup>().Where(g => g.Name == "AnimationStates").FirstOrDefault();
            if (animationStateGroup != null)
            {
                var tileLeavingState = animationStateGroup.States.OfType<VisualState>().Where(s => s.Name == "TileViewLeaving").FirstOrDefault();
                if (tileLeavingState != null)
                {
                    foreach (var animation in tileLeavingState.Storyboard.Children)
                        animation.Bind(Timeline.DurationProperty, this, AnimationDurationProperty);
                }

                var tileEnteringState = animationStateGroup.States.OfType<VisualState>().Where(s => s.Name == "TileViewEntering").FirstOrDefault();
                if (tileEnteringState != null)
                {
                    foreach (var animation in tileEnteringState.Storyboard.Children)
                        animation.Bind(Timeline.DurationProperty, this, AnimationDurationProperty);
                }
            }

            AddHandler(MetroStartPage.RepositionedEvent, new RoutedEventHandler(HandlePageRepositioned));
            SetPageCanvasPositions();

#if TRIAL
            InsertTrialMessage();
#endif
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            SetPageCanvasPositions();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == FirstNameProperty && Identity != null)
                Identity.FirstName = (string)e.NewValue;
            else if (e.Property == LastNameProperty && Identity != null)
                Identity.LastName = (string)e.NewValue;
            else if (e.Property == ImageSourceProperty && Identity != null)
                Identity.ImageSource = (ImageSource)e.NewValue;
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MetroStartPage();
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MetroStartPage;
        }


        /// <summary>
        /// Overriding to ensure pages are properly positioned.
        /// </summary>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (IsLoaded)
            {
                // Dispatch to give time for container to be generated.
                Dispatcher.BeginInvoke((Action)delegate
                {
                    // Need only set positions in response to items changed when items removed. When added,
                    // positions will be adjusted when the new page is resized to its full size...
                    if (e.Action == NotifyCollectionChangedAction.Remove)
                        SetPageCanvasPositions();
                    SetPagePositionStates();
                }, DispatcherPriority.ContextIdle);
            }
        }

        #endregion  // Overrides

        #region Protected

        /// <summary>
        /// Performs the logic associated with animating the tile view.
        /// </summary>
        protected virtual void AnimateTileView(bool makeVisible, Dock? side)
        {
            var animator = TileViewAnimator;
            if (!IsLoaded || animator == null)
                return;

            var tileView = GetTemplateChild("TileView") as FrameworkElement;
            if (tileView == null)
                return;

            if (side != null)
            {
                animator.Side = side.Value;
            }

            if (makeVisible)
            {
                VisualStateManager.GoToState(this, "TileViewEntering", true);
                animator.AnimateEnteringView(tileView);
            }
            else
            {
                VisualStateManager.GoToState(this, "TileViewLeaving", true);
                animator.AnimateLeavingView(tileView);
            }
        }

        #endregion // Protected

        #region Private

        private void SetupDragDropHelper()
        {
            if (IsDragDropEnabled)
            {
                if (_DragDropHelper == null)
                    _DragDropHelper = new MetroControlDragDropHelper(this);
            }
            else if (_DragDropHelper != null)
            {
                _DragDropHelper.IsEnabled = false;
            }
        }

        private void HandlePageRepositioned(object sender, RoutedEventArgs e)
        {
            Dispatcher.SchedualInvoke(new Action(SetPageCanvasPositions), DispatcherPriority.Loaded);
        }

        private void SetPageCanvasPositions()
        {
            if (!IsLoaded)
                return;

            var canvas = this.GetItemsHost() as Canvas;
            if (canvas == null)
                return;

            double left = 0;
            double height = 0;

            if (_PagePositioningStoryboard.Children.Count > 0)
                _PagePositioningStoryboard.Children.Clear();

            foreach (var page in Pages)
            {
                var currLeft = Canvas.GetLeft(page);
                if (Double.IsNaN(currLeft))
                {
                    Canvas.SetLeft(page, left);
                }
                else if (currLeft != left)
                {
                    DoubleAnimation animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(300), To = left, From = currLeft };
                    Storyboard.SetTarget(animation, page);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));
                    _PagePositioningStoryboard.Children.Add(animation);
                }
                if (page.ActualWidth == 0)
                    page.Measure(new Size(Double.PositiveInfinity, ActualHeight));
                left += page.DesiredSize.Width;
                height = Math.Max(height, page.DesiredSize.Height);
            }

            canvas.Height = height;
            canvas.Width = left;

            if (_PagePositioningStoryboard.Children.Count > 0)
            {
                _PagePositioningStoryboard.Begin(this);
            }
        }

        private void SetupIdentity()
        {
            if (Identity == null)
            {
                Identity = new MetroIdentity();
                Identity.FirstName = FirstName;
                Identity.LastName = LastName;
                Identity.ImageSource = ImageSource;
            }

            if (FirstNameProperty.IsUnsetValue(this))
                this.Bind(FirstNameProperty, this, "Identity.FirstName", BindingMode.TwoWay);
            if (LastNameProperty.IsUnsetValue(this))
                this.Bind(LastNameProperty, this, "Identity.LastName", BindingMode.TwoWay);
            if (ImageSourceProperty.IsUnsetValue(this))
                this.Bind(ImageSourceProperty, this, "Identity.ImageSource", BindingMode.TwoWay);
        }

        private void SetPagePositionStates()
        {
            var count = Items.Count;
            if (count <= 0)
                return;

            FrameworkElement container = null;

            // First container
            container = ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement;
            if (container != null)
                VisualStateManager.GoToState(container, "FirstPage", false);

            // Last container
            container = ItemContainerGenerator.ContainerFromIndex(count - 1) as FrameworkElement;
            if (container != null)
                VisualStateManager.GoToState(container, "LastPage", false);

            // Middle containers.
            for (int i = 1; i < count - 1; i++)
            {
                container = ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement;
                if (container != null)
                {
                    VisualStateManager.GoToState(container, "MiddlePages", false);
                }
            }
        }

        internal static Dock GetOpposingSide(Dock side)
        {
            if (side == Dock.Left)
                return Dock.Right;
            else if (side == Dock.Right)
                return Dock.Left;
            else if (side == Dock.Top)
                return Dock.Bottom;
            else
                return Dock.Top;
        }

        internal FrameworkElement GetTileView()
        {
            return GetTemplateChild("TileView") as FrameworkElement;
        }

        private void SetupTileViewAnimator(TransitionAnimationProvider animator)
        {
            if (!IsLoaded || animator == null)
                return;

            animator.Bind(TransitionAnimationProvider.DurationProperty, this, AnimationDurationProperty);

            var threeDRect = GetTemplateChild("ThreeDRectangle") as Rectangle2D3D;
            if (threeDRect == null)
                threeDRect = this.GetFirstDescendent<Rectangle2D3D>();

            var threeDAnimator = animator as TransitionAnimation3DProvider;
            if (threeDAnimator != null)
            {
                if (threeDRect != null)
                    threeDAnimator.Container = threeDRect;
                else
                    threeDAnimator.Container = this.GetFirstDescendent<Panel>();
            }
            else
            {
                if (threeDRect != null)
                    animator.Container = threeDRect.Parent as Panel;
                if (animator.Container == null)
                    animator.Container = this.GetFirstDescendent<Panel>();
            }

            animator.Initialize();
            animator.LeavingViewCompleted += TileViewAnimator_LeavingViewCompleted;
            animator.EnteringViewCompleted += TileViewAnimator_EnteringViewCompleted;
        }

        private void TileViewAnimator_EnteringViewCompleted(object sender, TransitionAnimationProvider.AnimationCompleteEventArgs e)
        {
            VisualStateManager.GoToState(this, "TileViewEnteringComplete", true);
        }

        private void TileViewAnimator_LeavingViewCompleted(object sender, TransitionAnimationProvider.AnimationCompleteEventArgs e)
        {
            VisualStateManager.GoToState(this, "TileViewLeavingComplete", true);
        }

        private void HandleTileViewAnimatorChanged(TransitionAnimationProvider oldAnimator, TransitionAnimationProvider newAnimator)
        {
            if (oldAnimator != null)
            {
                oldAnimator.LeavingViewCompleted -= TileViewAnimator_LeavingViewCompleted;
                oldAnimator.EnteringViewCompleted -= TileViewAnimator_EnteringViewCompleted;
            }
            if (newAnimator != null)
                SetupTileViewAnimator(newAnimator);
        }

        private void HandleTransitioningSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender)
                return; // Possible to have nested Metro Controls...

            var transitioningSelector = (TransitioningSelector)sender;

            Dock? side = null;
            if (transitioningSelector.Animator != null)
            {
                side = GetOpposingSide(transitioningSelector.Animator.Side);
            }

            if (e.RemovedItems != null && e.RemovedItems.Count > 0 && transitioningSelector.SelectedItem == null)
                AnimateTileView(true, side);
            else if (e.AddedItems != null && e.AddedItems.Count > 0 && transitioningSelector.SelectedItems.Count == 1)
                AnimateTileView(false, side);
        }
#if TRIAL
        private void InsertTrialMessage()
        {
            var metroShell = this.GetVisualParent<MetroShell>();
            if (metroShell != null)
                return;

            var trialMessage = new TextBlock
            {
                Text = "TRIAL VERSION",
                Opacity = 0.5,
                IsHitTestVisible = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(20)
            };

            Panel.SetZIndex(trialMessage, int.MaxValue);
            Grid.SetRowSpan(trialMessage, 10);
            Grid.SetColumnSpan(trialMessage, 10);

            var grid = this.GetFirstDescendent<Grid>();
            if (grid == null)
                grid = this.GetVisualParent<Grid>();
            if (grid != null)
                grid.Children.Add(trialMessage);
        }
#endif

        #endregion // Private
    }
}
