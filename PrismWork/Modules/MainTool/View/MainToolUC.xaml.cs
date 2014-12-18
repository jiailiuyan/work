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
        }

        void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            listboxview.Height = e.NewSize.Height;
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
                PluginWindow p = new PluginWindow(iplugin);
                this.pluginview.Children.Add(p);
            }
        }

    }

}
