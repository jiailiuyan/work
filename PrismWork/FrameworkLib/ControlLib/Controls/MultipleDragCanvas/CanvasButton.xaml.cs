using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jisons;
using WorkCommon.Plugin;

namespace ControlLib
{
    /// <summary>
    /// CanvasButton.xaml 的交互逻辑
    /// </summary>
    public partial class CanvasButton : UserControl, IActionControl
    {

        public static readonly Brush UnSelectBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4c4c4c"));

        public static readonly Brush SelectBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0000"));

        public IPluginObject PluginObject { get; private set; }

        //public bool IsSelected
        //{
        //    get { return (bool)GetValue(IsSelectedProperty); }
        //    set { SetValue(IsSelectedProperty, value); }
        //}
        //public static readonly DependencyProperty IsSelectedProperty =
        //    DependencyProperty.Register("IsSelected", typeof(bool), typeof(CanvasButton), new PropertyMetadata(false, IsSelectedChangedCallback));
        //static void IsSelectedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var control = d as CanvasButton;
        //    control.SetSelectedView((bool)e.NewValue);
        //}

        public event EventHandler AddEvent;
        protected void AddingEvent()
        {
            if (AddEvent != null)
            {
                AddEvent(this, EventArgs.Empty);
            }
        }

        public CanvasButton(IPluginObject po)
        {
            InitializeComponent();
            IsSelected = true;
            this.Loaded += CanvasButton_Loaded;
            PluginObject = po;
            this.pluginimage.Source = po.PluginIcon;
            this.pluginname.Text = po.PluginName;
        }

        void CanvasButton_Loaded(object sender, RoutedEventArgs e)
        {
            defaultView = this.btn.GetImageSource();
            IsSelected = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void SetSelectedView(bool isselected)
        {
            this.selectedview.Fill = isselected ? SelectBrush : UnSelectBrush;
        }

        #region IActionControl 成员

        bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                SetSelectedView(isSelected);
            }
        }

        ImageSource defaultView = null;
        public ImageSource DefaultView
        {
            get
            {
                return defaultView;
            }
        }

        public FrameworkElement Element
        {
            get { return this; }
        }

        #endregion

        private void btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddingEvent();
        }
    }
}
