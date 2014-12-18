using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using WorkCommon.Behaviors;
using WorkCommon.Manager;
using Jisons;
using WorkCommon.Plugin;

namespace Modules.MainTool
{
    /// <summary>
    /// MainToolUC.xaml 的交互逻辑
    /// </summary>
    /// 
    /// <summary>
    /// BottomToolBarUC.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainTool)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainToolUC : UserControl
    {
        public List<PluginWindow> PluginWindows = new List<PluginWindow>();

        public MainToolUC()
        {
            InitializeComponent();

            this.Loaded += MainToolUC_Loaded;

        }

        void MainToolUC_Loaded(object sender, RoutedEventArgs e)
        {
            var itemscontrol = this.FindVisualParent<ItemsControl>();
            itemscontrol.SizeChanged += ItemsControl_SizeChanged;
            listboxview.Height = itemscontrol.ActualHeight;
            listboxview.Width = itemscontrol.ActualWidth;

            foreach (var iplugin in this.ViewModel.PluginObjects)
            {
                PluginWindow p = new PluginWindow(this.pluginview, iplugin);
                this.PluginWindows.Add(p);
                this.pluginview.Children.Add(p);
            }

        }

        void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.pluginview.Height = this.listboxview.Height = e.NewSize.Height;
            this.pluginview.Width = this.listboxview.Width = e.NewSize.Width;
        }

        [Import]
        public MainToolViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainToolViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void pluginAdd_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var iplugin = button.DataContext as IPluginObject;
            if (iplugin != null)
            {
                var p = this.PluginWindows.FirstOrDefault(i => i.Context.Equals(iplugin));
                if (p != null)
                {
                    p.Show();
                }
            }
        }

    }

}
