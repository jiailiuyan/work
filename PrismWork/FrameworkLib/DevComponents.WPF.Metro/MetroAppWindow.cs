using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{

    /// <summary>
    /// Implementation of main application window for Metro. Inherits from DevComponents.WPF.Controls.ChromelessWindow. 
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroAppWindow : ChromelessWindow
    {
        #region Construction, Fields

        static MetroAppWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroAppWindow), new FrameworkPropertyMetadata(typeof(MetroAppWindow)));
        }

        #endregion // Construction and Fields

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MetroUI.EnsureTheme(this);
            Dispatcher.BeginInvoke(new Action(SetStatusBarInnerMargin), DispatcherPriority.ContextIdle);
        }

        /// <summary>
        /// Overriding to adjust the inner margin of the metro status bar, if there is one, when the window is maximized or restored.
        /// </summary>
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            SetStatusBarInnerMargin();
        }

        #endregion // Overrides

        #region Private

        private void SetStatusBarInnerMargin()
        {
            var statusBar = this.GetFirstDescendent<MetroStatusBar>();
            if (statusBar == null)
                return;

            if (ResizeMode != ResizeMode.CanResizeWithGrip || WindowState == WindowState.Maximized)
            {
                var thickness = new Thickness(0);
                if (!statusBar.InnerMargin.Equals(thickness))
                    statusBar.InnerMargin = thickness;
            }
            else
            {
                var resizeGrip = GetTemplateChild("ResizeGrip");
                if (resizeGrip != null)
                    statusBar.InnerMargin = new Thickness(0, 0, 10, 0);
            }
        }

        #endregion // Private
    }
}
