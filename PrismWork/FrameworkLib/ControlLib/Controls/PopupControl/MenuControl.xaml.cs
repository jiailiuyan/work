using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlLib
{
    /// <summary>
    /// MenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class MenuControl : UserControl
    {

        public string MenuTitle
        {
            get { return (string)GetValue(MenuTitleProperty); }
            set { SetValue(MenuTitleProperty, value); }
        }
        public static readonly DependencyProperty MenuTitleProperty =
            DependencyProperty.Register("MenuTitle", typeof(string), typeof(MenuControl), new PropertyMetadata("菜单"));

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(MenuControl), new PropertyMetadata(null));

        public FrameworkElement PopupContent
        {
            get { return (FrameworkElement)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(FrameworkElement), typeof(MenuControl), new PropertyMetadata(null));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(MenuControl), new PropertyMetadata(false, IsOpenChangedCallback));

        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }
        public static readonly DependencyProperty PopupWidthProperty =
            DependencyProperty.Register("PopupWidth", typeof(double), typeof(MenuControl), new PropertyMetadata(200d));

        public double PopupHeight
        {
            get { return (double)GetValue(PopupHeightProperty); }
            set { SetValue(PopupHeightProperty, value); }
        }
        public static readonly DependencyProperty PopupHeightProperty =
            DependencyProperty.Register("PopupHeight", typeof(double), typeof(MenuControl), new PropertyMetadata(200d));

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof(double), typeof(MenuControl), new PropertyMetadata(0d));

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof(double), typeof(MenuControl), new PropertyMetadata(1d));

        public MenuControl()
        {
            InitializeComponent();

            this.popupcontrol.IsOpen = false;
            this.popupcontrol.AllowsTransparency = true;

            this.popupcontrol.PopupAnimation = PopupAnimation.Fade;

            this.popupcontrol.StaysOpen = false;

            this.Loaded += MenuControl_Loaded;

            this.popupcontrol.Opened += popupcontrol_Opened;
            this.popupcontrol.Closed += popupcontrol_Closed;
            //Grid g = new Grid();
            //g.Width = 200;
            //g.Height = 200;
            //g.Background = new SolidColorBrush(Color.FromArgb(50, 200, 150, 2));
            //this.PopupContent = g;

            this.DataContext = this;
        }

        static void IsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MenuControl;
            if ((bool)e.NewValue)
            {
                if (!control.popupcontrol.IsOpen)
                {
                    control.popupcontrol.IsOpen = true;
                }
            }
            else
            {
                if (control.popupcontrol.IsOpen)
                {
                    control.popupcontrol.IsOpen = false;
                }
            }
        }

        void popupcontrol_Opened(object sender, EventArgs e)
        {
            if (!IsOpen)
            {
                IsOpen = true;
            }
            ResetLocation();
        }

        void popupcontrol_Closed(object sender, EventArgs e)
        {
            if (IsOpen)
            {
                IsOpen = false;
            }
        }

        void MenuControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = !IsOpen;
        }

        public void ResetLocation()
        {
            this.popupcontrol.HorizontalOffset = OffsetX;
            this.popupcontrol.VerticalOffset = -this.PopupContent.ActualHeight - this.ActualHeight - OffsetY;

        }

    }
}
