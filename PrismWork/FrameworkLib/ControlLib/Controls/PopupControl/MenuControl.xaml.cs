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

namespace WpfApplication9
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

        public UIElement PopupContent
        {
            get { return (UIElement)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(UIElement), typeof(MenuControl), new PropertyMetadata(null));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(MenuControl), new PropertyMetadata(false));

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
            DependencyProperty.Register("OffsetY", typeof(double), typeof(MenuControl), new PropertyMetadata(0d));

        public MenuControl()
        {
            InitializeComponent();

            this.popupcontrol.HorizontalOffset = 100;
            this.popupcontrol.VerticalOffset = -100;
            this.popupcontrol.IsOpen = true;
            this.popupcontrol.AllowsTransparency = true;

            this.popupcontrol.PopupAnimation = PopupAnimation.Fade;

            this.popupcontrol.StaysOpen = false;

            this.Loaded += MenuControl_Loaded;

            this.popupcontrol.Opened += popupcontrol_Opened;

            Grid g = new Grid();
            g.Width = 200;
            g.Height = 200;
            g.Background = new SolidColorBrush(Color.FromArgb(50, 200, 150, 2));
            this.PopupContent = g;

            this.DataContext = this;
        }

        void popupcontrol_Opened(object sender, EventArgs e)
        {

            this.popupcontrol.HorizontalOffset = -150;
            this.popupcontrol.VerticalOffset = -200;
        }

        void MenuControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            if (IsOpen)
            {
                this.popupcontrol.IsOpen = false;
                IsOpen = false;
            }
            else
            {
                this.popupcontrol.IsOpen = true;
                IsOpen = true;
            }
        }
    }
}
