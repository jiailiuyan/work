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

    }
}
