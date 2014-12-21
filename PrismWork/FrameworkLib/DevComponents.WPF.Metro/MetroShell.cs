using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of container for MetroTabItems. Also serves as glue for Metro controls in the app. 
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroShell : TabControl
    {
        #region Fields, Construction
        static MetroShell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroShell), new FrameworkPropertyMetadata(typeof(MetroShell)));
        }

        private FrameworkElement _AppView;
        internal FrameworkElement AppView
        {
            get
            {
                if (_AppView == null)
                    _AppView = GetTemplateChild("AppView") as FrameworkElement;
                return _AppView;
            }
        }

        #endregion // Fields, Construction

        #region Events

        /// <summary>
        /// Using a RoutedEvent as backing for BackstageOpened.
        /// </summary>
        public static RoutedEvent BackstageOpenedEvent =
            EventManager.RegisterRoutedEvent("BackstageOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroShell));
        /// <summary>
        /// Routed event raised when IsBackstageOpen changes to true.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Routed event raised when IsBackstageOpen changes to true.")]
        public event RoutedEventHandler BackstageOpened
        {
            add { AddHandler(BackstageOpenedEvent, value); }
            remove { RemoveHandler(BackstageOpenedEvent, value); }
        }

        /// <summary>
        /// Using a RoutedEvent as backing for BackstageOpened.
        /// </summary>
        public static RoutedEvent BackstageClosedEvent =
            EventManager.RegisterRoutedEvent("BackstageClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroShell));
        /// <summary>
        /// Routed event raised when IsBackstageOpen changes to false.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Routed event raised when IsBackstageOpen changes to false.")]
        public event RoutedEventHandler BackstageClosed
        {
            add { AddHandler(BackstageClosedEvent, value); }
            remove { RemoveHandler(BackstageClosedEvent, value); }
        }

        #endregion // Events

        #region Dependency Properties

        /// <summary>
        // Using a DependencyProperty as the backing store for BackstageButtonContent.
        /// </summary>
        public static readonly DependencyProperty BackstageButtonContentProperty =
            DependencyProperty.Register("BackstageButtonContent", typeof(object), typeof(MetroShell), new UIPropertyMetadata("FILE"));
        /// <summary>
        /// Defines the content rendered inside the backstage button (i.e. File button)
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("The content of the backstage button.")]
        public object BackstageButtonContent
        {
            get { return (object)GetValue(BackstageButtonContentProperty); }
            set { SetCurrentValue(BackstageButtonContentProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for BackstageContent.
        /// </summary>
        public static readonly DependencyProperty BackstageProperty =
            DependencyProperty.Register("Backstage", typeof(object), typeof(MetroShell), new UIPropertyMetadata(null));
        /// <summary>
        /// Gets or sets the object which will be shown when IsBackstageOpen is true. Normally, a MetroBackstage element, or an object with
        /// associated DataTemplate containing MetroBackstage. 
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Specifies the object/element which is shown when the backstage is opened.")]
        public object Backstage
        {
            get { return GetValue(BackstageProperty); }
            set { SetCurrentValue(BackstageProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Chrome.
        /// </summary>
        public static readonly DependencyProperty ChromeProperty =
            DependencyProperty.Register("Chrome", typeof(object), typeof(MetroShell), new FrameworkPropertyMetadata(null,
                (s, e) =>
                {
                    ((MetroShell)s).AddLogicalChild(e.NewValue);
                }));
        /// <summary>
        /// Get or set the object which is displayed as window chrome. Normally, a MetroChrome element, 
        /// or an object with associated DataTemplate containing MetroChrome.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Specifies the object/element which is put in the place of the window chrome.")]
        public object Chrome
        {
            get { return GetValue(ChromeProperty); }
            set { SetCurrentValue(ChromeProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsBackstageOpen.
        /// </summary>
        public static readonly DependencyProperty IsBackstageOpenProperty =
            DependencyProperty.Register("IsBackstageOpen", typeof(bool), typeof(MetroShell), new UIPropertyMetadata(false,
                (s, e) =>
                {
                    ((MetroShell)s).HandleIsBackstageOpenChanged((bool)e.NewValue);
                }));
        /// <summary>
        /// Get or set whether the backstage is opened.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines whether the backstage is opened.")]
        public bool IsBackstageOpen
        {
            get { return (bool)GetValue(IsBackstageOpenProperty); }
            set { SetCurrentValue(IsBackstageOpenProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ShowBackstageButton.
        /// </summary>
        public static readonly DependencyProperty ShowBackstageButtonProperty =
            DependencyProperty.Register("ShowBackstageButton", typeof(bool), typeof(MetroShell), new UIPropertyMetadata(true));
        /// <summary>
        /// Determines whether the backstage button (i.e. File button) is visible.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines whether the backstage button (i.e. File button) is visible.")]
        public bool ShowBackstageButton
        {
            get { return (bool)GetValue(ShowBackstageButtonProperty); }
            set { SetCurrentValue(ShowBackstageButtonProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ShowStatusBar.
        /// </summary>
        public static readonly DependencyProperty ShowStatusBarProperty =
            DependencyProperty.Register("ShowStatusBar", typeof(bool), typeof(MetroShell), new UIPropertyMetadata(true));
        /// <summary>
        /// Determines whether the status bar (if set) is visible.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Determines whether the status bar (if set) is visible.")]
        public bool ShowStatusBar
        {
            get { return (bool)GetValue(ShowStatusBarProperty); }
            set { SetCurrentValue(ShowStatusBarProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for StartControl.
        /// </summary>
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(object), typeof(MetroShell), new UIPropertyMetadata(null));
        /// <summary>
        /// Gets or Sets an object which represents the Metro start view. Normally, a MetroStartControl or
        /// an object that has an associated DataTemplate which contains a MetroStartControl.
        /// </summary>
        public object Start
        {
            get { return (object)GetValue(StartProperty); }
            set { SetCurrentValue(StartProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for StatusBar.
        /// </summary>
        public static readonly DependencyProperty StatusBarProperty =
            DependencyProperty.Register("StatusBar", typeof(object), typeof(MetroShell), new UIPropertyMetadata(null));
        /// <summary>
        /// Gets or sets the object/element which is presented in the place of the status bar. 
        /// Normally, a MetroStatusBar or an object that has an associated DataTemplate containing a MetroStatusBar.
        /// </summary>
        [Browsable(true)]
        [Category("Metro")]
        [Description("Specifies the object/element which is displayed at the location of the status bar.")]
        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetCurrentValue(StatusBarProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ThreeDAnimation.
        /// </summary>
        public static readonly DependencyProperty ThreeDAnimationProperty =
            DependencyProperty.Register("ThreeDAnimation", typeof(Metro3DAnimationType), typeof(MetroShell), new UIPropertyMetadata(Metro3DAnimationType.Default));
        public Metro3DAnimationType ThreeDAnimation
        {
            get { return (Metro3DAnimationType)GetValue(ThreeDAnimationProperty); }
            set { SetCurrentValue(ThreeDAnimationProperty, value); }
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
#if TRIAL
            InsertTrialMessage();
#endif
        }

        /// <summary>
        /// Overriding to ensure MetroTabItem is container.
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MetroTabItem;
        }

        /// <summary>
        /// Overriding to return instance of MetroTabItm.
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MetroTabItem();
        }

        #endregion // Overrides

        #region Protected

        /// <summary>
        /// Virtual method called when IsBackstageOpen changes to true.
        /// </summary>
        protected virtual void OnBackstageOpened()
        {
            RoutedEventArgs args = new RoutedEventArgs(BackstageOpenedEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// Virtual method called when IsBackstageOpen changes to false.
        /// </summary>
        protected virtual void OnBackstageClosed()
        {
            RoutedEventArgs args = new RoutedEventArgs(BackstageClosedEvent, this);
            RaiseEvent(args);
        }

        #endregion // Protected

        #region Private

        private void HandleIsBackstageOpenChanged(bool isOpen)
        {
            if (isOpen)
                OnBackstageOpened();
            else
                OnBackstageClosed();
        }

        private void InsertTrialMessage()
        {
            var trialMessage = new TextBlock
            {
                Text = "TRIAL VERSION",
                Opacity = 0.5,
                IsHitTestVisible = false,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 5, 20)
            };

            Panel.SetZIndex(trialMessage, int.MaxValue);
            Grid.SetRowSpan(trialMessage, 10);
            Grid.SetColumnSpan(trialMessage, 10);

            var mainGrid = this.GetFirstDescendent<Grid>();
            if (mainGrid != null)
                mainGrid.Children.Add(trialMessage);
        }

        #endregion // Private
    }
}
