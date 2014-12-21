using System.ComponentModel;
using System.Windows;
using DevComponents.WPF.Controls;
using System.Windows.Input;
using System;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// A DevComponents.WPF.Controls.DialogWindow styled for Metro.
    /// </summary>
    [DesignTimeVisible(false)]
    public class MetroDialogWindow : DialogWindow
    {
        static MetroDialogWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroDialogWindow), new FrameworkPropertyMetadata(typeof(MetroDialogWindow)));
        }

        public MetroDialogWindow()
        {
            ChromelessWindowBehavior.SetIsChromeless(this, true);
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Chrome.
        /// </summary>
        public static readonly DependencyProperty ChromeProperty = DependencyProperty.Register("Chrome", typeof(MetroChrome), typeof(MetroDialogWindow), new UIPropertyMetadata(null));
        /// <summary>
        /// Sets the Chrome for the window. Usually an instance of MetroChrome.
        /// </summary>
        [Description("Sets the Chrome for the window. Usually an instance of MetroChrome.")]
        [Browsable(true)]
        [Category("Metro")]
        public MetroChrome Chrome
        {
            get { return (MetroChrome)GetValue(ChromeProperty); }
            set { SetCurrentValue(ChromeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HasDropShadow.
        /// </summary>
        public static readonly DependencyProperty HasDropShadowProperty = ChromelessWindowBehavior.HasDropShadowProperty.AddOwner(typeof(MetroDialogWindow));
        /// <summary>
        /// Get or set whether the window is displayed with a drop shadow. Supported for OS Vista and above only.
        /// This is a dependency property. The default value is true.
        /// </summary>
        public bool HasDropShadow
        {
            get { return (bool)GetValue(HasDropShadowProperty); }
            set { SetCurrentValue(HasDropShadowProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ResizeBorderThickness.
        /// </summary>
        public static readonly DependencyProperty ResizeBorderThicknessProperty = ChromelessWindowBehavior.ResizeBorderThicknessProperty.AddOwner(typeof(MetroDialogWindow));
        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetCurrentValue(ResizeBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (MetroUI.ThemeProperty.IsUnsetValue(this) && Owner != null)
            {
                var focused = FocusManager.GetFocusedElement(Owner);
                if (focused != null)
                {
                    var theme = MetroUI.GetEffectiveTheme((UIElement)focused);
                    MetroUI.SetTheme(this, theme);
                }
                else
                {
                    MetroUI.SetTheme(this, MetroUI.GetTheme(Owner));
                }
            }

            if (ChromeProperty.IsUnsetValue(this) && Chrome == null)
            {
                Chrome = new MetroChrome { ShowSystemMenu = false, HideMinimizeMaximizeButtons = true, FontWeight = FontWeights.Bold };
            }
        }
    }

    /// <summary>
    /// A MetroDialogWindow where the default values for HasCancelButton and HasOkButton are set to false.
    /// This class is here for legacy reasons.
    /// </summary>
    [DesignTimeVisible(false)]
    [Obsolete("Use MetroDialogWindow")]
    public class MetroDialog : MetroDialogWindow
    {
        static MetroDialog()
        {
            MetroDialogWindow.HasCancelButtonProperty.OverrideMetadata(typeof(MetroDialog), new FrameworkPropertyMetadata(false));
            MetroDialogWindow.HasOkButtonProperty.OverrideMetadata(typeof(MetroDialog), new FrameworkPropertyMetadata(false));
        }
    }
}
