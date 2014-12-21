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
using WorkCommon.Events;
using WorkCommon.Manager;
using WorkCommon.Plugin;

namespace Modules.BottomToolBar
{
    /// <summary>
    /// BottomToolBarUC.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomToolBar)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class BottomToolBarUC : UserControl
    {
        public BottomToolBarUC()
        {
            InitializeComponent();

            this.Loaded += BottomToolBarUC_Loaded;
        }

        void BottomToolBarUC_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext;
        }

        [Import]
        public BottomToolBarViewModel ViewModel
        {
            get
            {
                return this.DataContext as BottomToolBarViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void pluginAction_Click(object sender, RoutedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control != null)
            {
                var po = control.DataContext as IPluginObject;
                if (po != null)
                {
                    po.IsShow = !po.IsShow;
                }
            }
        }


    }
}
