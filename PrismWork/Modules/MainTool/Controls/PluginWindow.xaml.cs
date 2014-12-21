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
using WorkCommon.Events;
using WorkCommon.Manager;
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

        public PluginWindow(IPluginObject pluginobject)
        {
            InitializeComponent();
            this.Context = pluginobject;
            this.contentpresenter.Content = pluginobject.Plugin;
            this.Title = pluginobject.PluginName;

            this.DataContext = this;

            this.Visibility = System.Windows.Visibility.Collapsed;

            if (pluginobject.Type == PluginType.Window)
            {
                this.titlgrid.Visibility = System.Windows.Visibility.Visible;
            }

            pluginobject.Opening += PluginWindow_Opening;
            pluginobject.Closing += pluginobject_Closing;
            pluginobject.Hiding += pluginobject_Hiding;

            GlobalEvent.Instance.RaisePluginChange(new PluginsEventArgs() { Action = PluginAction.Add, PluginObject = this.Context });

            this.IsVisibleChanged += PluginWindow_IsVisibleChanged;
        }

        void PluginWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Context.Plugin.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        void PluginWindow_Opening(object sender, EventArgs e)
        {
            ShowPlugin(false);
        }

        void pluginobject_Closing(object sender, EventArgs e)
        {
            DeletePlugin();
        }

        void pluginobject_Hiding(object sender, EventArgs e)
        {
            ClosePlugin();
        }

        private void ask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            ClosePlugin();
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
            DeletePlugin();
        }

        public void ShowPlugin(bool issent = true)
        {
            this.Visibility = System.Windows.Visibility.Visible;
            if (issent)
            {
                GlobalEvent.Instance.RaisePluginChange(new PluginsEventArgs() { Action = PluginAction.Show, PluginObject = this.Context });
            }
        }

        public void DeletePlugin()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
            GlobalEvent.Instance.RaisePluginChange(new PluginsEventArgs() { Action = PluginAction.Delete, PluginObject = this.Context });
        }

        public void ClosePlugin()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
            GlobalEvent.Instance.RaisePluginChange(new PluginsEventArgs() { Action = PluginAction.Close, PluginObject = this.Context });
        }

    }
}
