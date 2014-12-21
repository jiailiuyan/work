using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implements Chrome for Metro application and Metro dialog window. Includes system buttons for Minimize, Maximize/Restore, Close. 
    /// Displays Title taken from parent window. Displays Icon taken from parent window. Is responsible for enabling drag of window by 
    /// caption area. Default context menu is system menu for Window.
    /// </summary>
    [TemplatePart(Name = "TitleColumn", Type = typeof(System.Windows.Controls.ColumnDefinition))]
    [TemplatePart(Name = "QATColumn", Type = typeof(System.Windows.Controls.ColumnDefinition))]
    [TemplatePart(Name = "QATPresenter", Type = typeof(FrameworkElement))]
    [DesignTimeVisible(true)]
    public class MetroChrome : ItemsControl
    {
        #region Fields, Construction

        /// <summary>
        /// Identifies the Style resource used for the system buttons inside the chrome.
        /// </summary>
        public static readonly ComponentResourceKey SystemButtonStyleKey = new ComponentResourceKey(typeof(MetroChrome), "SystemButtonStyleKey");
        public static readonly ComponentResourceKey ForegroundKey = new ComponentResourceKey(typeof(MetroChrome), "ForegroundKey");
        public static readonly ComponentResourceKey InactiveForegroundKey = new ComponentResourceKey(typeof(MetroChrome), "InactiveForegroundKey");
        
        static MetroChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroChrome), new FrameworkPropertyMetadata(typeof(MetroChrome)));
        }

        private System.Windows.Controls.ColumnDefinition _TitleColumn;
        private System.Windows.Controls.ColumnDefinition _QatColumn;
        private FrameworkElement _QATPresenter;
        private long _IconMouseDownTicks;
        private bool _IsContextMenuClosed = true;
        private bool _ShouldDragMoveWindow;

        #endregion // Fields, Construction

        #region Dependency Properties

        /// <summary>
        /// Using a DependencyProperty as the backing store for HideSystemButtons.
        /// </summary>
        public static readonly DependencyProperty HideSystemButtonsProperty =
            DependencyProperty.Register("HideSystemButtons", typeof(bool), typeof(MetroChrome), new PropertyMetadata(false));
        /// <summary>
        /// Get or set whether any system buttons are shown. If this is true, then property HideMinimizeMaximizeButtons has no effect.
        /// </summary>
        public bool HideSystemButtons
        {
            get { return (bool)GetValue(HideSystemButtonsProperty); }
            set { SetValue(HideSystemButtonsProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for HideMinimizeMaximizeButtons.
        public static readonly DependencyProperty HideMinimizeMaximizeButtonsProperty =
            DependencyProperty.Register("HideMinimizeMaximizeButtons", typeof(bool), typeof(MetroChrome), new FrameworkPropertyMetadata(false));
        /// <summary>
        /// If set, the minimize and maximize buttons are hidden. Note: these buttons are also hidden when the Window's ResizeMode is NoResize.
        /// </summary>
        public bool HideMinimizeMaximizeButtons
        {
            get { return (bool)GetValue(HideMinimizeMaximizeButtonsProperty); }
            set { SetValue(HideMinimizeMaximizeButtonsProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HideWindowIcon.
        /// </summary>
        public static readonly DependencyProperty HideWindowIconProperty =
            DependencyProperty.Register("HideWindowIcon", typeof(bool), typeof(MetroChrome), new PropertyMetadata(false));
        /// <summary>
        /// Get or set whether the window icon is hidden.
        /// </summary>
        public bool HideWindowIcon
        {
            get { return (bool)GetValue(HideWindowIconProperty); }
            set { SetValue(HideWindowIconProperty, value); }
        }
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for HideWindowTitle.
        /// </summary>
        public static readonly DependencyProperty HideWindowTitleProperty =
            DependencyProperty.Register("HideWindowTitle", typeof(bool), typeof(MetroChrome), new PropertyMetadata(false));
        /// <summary>
        /// Get or set whetherf the window title is hidden.
        /// </summary>
        public bool HideWindowTitle
        {
            get { return (bool)GetValue(HideWindowTitleProperty); }
            set { SetValue(HideWindowTitleProperty, value); }
        }
        
        /// <summary>
        // Using a DependencyProperty as the backing store for QuickAccessToolBar.
        /// </summary>
        public static readonly DependencyProperty QuickAccessToolBarProperty =
            DependencyProperty.Register("QuickAccessToolBar", typeof(object), typeof(MetroChrome), new UIPropertyMetadata(null));
        /// <summary>
        /// Gets or sets the Quick Access ToolBar. Can be either an instance of QuickAccessToolBar or 
        /// an object which has a data template defined containing a QuickAccessToolBar.
        /// </summary>
        [Description("Sets the Quick Access Tool for the application.")]
        [Browsable(true)]
        [Category("Metro")]
        public object QuickAccessToolBar
        {
            get { return GetValue(QuickAccessToolBarProperty); }
            set { SetCurrentValue(QuickAccessToolBarProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ShowSystemMenu.
        /// </summary>
        public static readonly DependencyProperty ShowSystemMenuProperty =
            DependencyProperty.Register("ShowSystemMenu", typeof(bool), typeof(MetroChrome), new UIPropertyMetadata(true));
        /// <summary>
        /// Get or set whether the system context menu is shown. This is a dependency property. The default value is true.
        /// </summary>
        [Description("Determies whether the System menu is shown.")]
        [Browsable(true)]
        [Category("Metro")]
        public bool ShowSystemMenu
        {
            get { return (bool)GetValue(ShowSystemMenuProperty); }
            set { SetCurrentValue(ShowSystemMenuProperty, value); }
        }

        #endregion // Dependency Properties

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var windowIcon = GetTemplateChild("WindowIcon") as FrameworkElement;
            if (windowIcon != null && ContextMenu != null)
            {
                windowIcon.MouseLeftButtonDown += HandleIconMouseLeftButtonDown;
                ContextMenu.Closed += delegate
                {
                    _IsContextMenuClosed = true;
                };
            }

            _TitleColumn = GetTemplateChild("TitleColumn") as System.Windows.Controls.ColumnDefinition;
            _QatColumn = GetTemplateChild("QATColumn") as System.Windows.Controls.ColumnDefinition;
            _QATPresenter = GetTemplateChild("QATPresenter") as FrameworkElement;
        }

        /// <summary>
        /// Overriding to affect desired resize behavior.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            if (ActualWidth == 0 || QuickAccessToolBar == null || _QatColumn == null || _TitleColumn == null)
                return base.MeasureOverride(constraint);

            // Seems like there should be an easier way to get the desired resize behavior, but this is
            // all I can think of.
            if (_QatColumn.Width.IsAuto)
            {
                if (_TitleColumn.ActualWidth <= 55)
                {
                    _QatColumn.Width = new GridLength(1, GridUnitType.Star);
                    _TitleColumn.Width = new GridLength(55, GridUnitType.Pixel);
                }
            }
            else
            {
                if (_QATPresenter != null)
                {
                    _QATPresenter.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    if (_QatColumn.ActualWidth > _QATPresenter.DesiredSize.Width + 2)
                    {
                        _QatColumn.Width = new GridLength(1, GridUnitType.Auto);
                        _TitleColumn.Width = new GridLength(1, GridUnitType.Star);
                    }
                }
            }

            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// Overriding to prevent context menu from opening when ShowSystemMenu is false or if 
        /// mouse if over a button.
        /// </summary>
        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            var fe = e.OriginalSource as FrameworkElement;
            ContextMenu menu = fe.ContextMenu;            

            while (menu == null && fe != this)
            {
                fe = VisualTreeHelper.GetParent(fe) as FrameworkElement;
                if (fe != null)
                    menu = fe.ContextMenu;
            }

            if (menu != null && fe == this)
            {
                //MetroUI.SetTheme(menu, MetroUI.GetEffectiveTheme(this));

                var pos = ((UIElement)e.OriginalSource).TranslatePoint(new Point(e.CursorLeft, e.CursorTop), this);
                if (IsCaption(pos))
                {
                    e.Handled = !ShowSystemMenu;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            _ShouldDragMoveWindow = true;
        }

        /// <summary>
        /// Overriding to do window drag.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_ShouldDragMoveWindow && e.LeftButton == MouseButtonState.Pressed && IsCaption(e.GetPosition(this)))
            {
                var window = Window.GetWindow(this);
                if (window != null)
                {
                    window.DragMove();
                    e.Handled = true;
                }
            }

            _ShouldDragMoveWindow = false;
        }


        /// <summary>
        /// Overriding to toggle between window Maximized and Restored states.
        /// </summary>
        protected override void OnMouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsCaption(e.GetPosition(this)) || System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
                return;

            var window = Window.GetWindow(this);
            if (window != null && window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                if (window.WindowState != WindowState.Maximized)
                    window.WindowState = WindowState.Maximized;
                else
                    window.WindowState = WindowState.Normal;
            }
        }

        #endregion // Overrides

        #region Private

        private bool IsPositionWithin(FrameworkElement element, Point relativePosition)
        {
            return relativePosition.X >= 0 &&
                   relativePosition.Y >= 0 &&
                   relativePosition.X <= element.ActualWidth &&
                   relativePosition.Y <= element.ActualHeight;
        }

        private bool IsCaption(Point position)
        {
            var focusable = this.HitTestForOne<FrameworkElement>(position, p => p.Focusable);

            if (focusable != null)
                return false;
            return true;
        }

        private void HandleIconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!ShowSystemMenu)
                return;

            // The Popup for the context menu gets in the way and ClickCount is always 1 never 2.
            // So it is necessary to time the duration between ticks to determine a double click
            // on the Icon. (Double click on icon closes application.)

            if (IsDoubleClick(_IconMouseDownTicks) && WindowCommands.CloseCommand.CanExecute(null, this))
            {
                CloseParentWindow();
                return;
            }

            _IconMouseDownTicks = DateTime.Now.Ticks;

            // Mouse down over the icon will cause the context menu to close if it is open. Context menu's
            // IsOpen property is set to false before the popup actually closes and before this event handler is called.
            // However, context menu's Closed event fires after this method is called. So we can maintain a flag
            // which lets us know if the context menu was open when the click actually occurred. If it was open, then the click 
            // should not open it again.
            if (_IsContextMenuClosed)
            {
                OpenContextMenu();
                // If not handled here, then the mouse down event is not recieved at all if context menu is open,
                // making it impossible to detect a double click.
                e.Handled = true;
            }
        }

        private void CloseParentWindow()
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
        }

        private static bool IsDoubleClick(long ticksStart)
        {
            return TimeSpan.FromTicks(DateTime.Now.Ticks - ticksStart).TotalMilliseconds < GetDoubleClickTime();
        }

        [DllImport("User32", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern int GetDoubleClickTime();

        private void OpenContextMenu()
        {
            var contextMenu = ContextMenu;
            contextMenu.PlacementTarget = this;
            contextMenu.Placement = PlacementMode.Bottom;
            _IsContextMenuClosed = false;
            contextMenu.IsOpen = true;
        }

        #endregion // Private
    }
}
