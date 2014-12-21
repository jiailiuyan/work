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
using ControlLib;

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
            actionview.Height = itemscontrol.ActualHeight - 10;
            actionview.Width = itemscontrol.ActualWidth - 10;

            foreach (var iplugin in this.ViewModel.PluginObjects)
            {
                actionview.AddDragControl(iplugin);
            }

            this.actionview.AddControlEvent += actionview_AddControlEvent;
        }

        void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.pluginview.Height = this.actionview.Height = e.NewSize.Height - 10;
            this.pluginview.Width = this.actionview.Width = e.NewSize.Width - 10;
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

        void actionview_AddControlEvent(object sender, AddControlArgs e)
        {
            var iplugin = e.PluginObject;
            if (iplugin != null)
            {
                var p = this.PluginWindows.FirstOrDefault(i => i.Context.Equals(iplugin));
                if (p != null)
                {
                    p.ShowPlugin();
                }
                else
                {
                    PluginWindow pw = new PluginWindow(iplugin);
                    this.PluginWindows.Add(pw);
                    this.pluginview.Children.Add(pw);
                    pw.ShowPlugin();
                }
            }
        }

    }

}
