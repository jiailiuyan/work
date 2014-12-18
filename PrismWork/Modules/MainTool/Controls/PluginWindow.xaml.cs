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
using WorkCommon.Plugin;

namespace Modules.MainTool
{
    /// <summary>
    /// PluginItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class PluginWindow : UserControl
    {

        public Panel Parent { get; private set; }

        public IPluginObject Context { get; private set; }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PluginWindow), new PropertyMetadata(""));

        public PluginWindow(Panel parent, IPluginObject pluginobject)
        {
            InitializeComponent();
            this.Context = pluginobject;
            this.contentpresenter.Content = pluginobject.Plugin;
            this.Title = pluginobject.PluginName;

            this.DataContext = this;

            this.Parent = parent;

            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void min_Click(object sender, RoutedEventArgs e)
        {

        }



        private void close_Clic1k(object sender, RoutedEventArgs e)
        {

        }

        private void max_Click1(object sender, MouseButtonEventArgs e)
        {

        }

        private void max_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void close_Click(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        public void Show()
        {
            this.Visibility = System.Windows.Visibility.Visible;
        }

        public void Close()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

    }
}
