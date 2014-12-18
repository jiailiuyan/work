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

namespace ImageView
{
    /// <summary>
    /// ImageViewUC.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ImageView)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ImageViewUC : UserControl
    {

        [Import]
        public ImageViewUCViewModel ViewModel
        {
            get
            {
                return this.DataContext as ImageViewUCViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public ImageViewUC()
        {
            InitializeComponent();

            this.Loaded += ImageViewUC_Loaded;
        }

        void ImageViewUC_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
