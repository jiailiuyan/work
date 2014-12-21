using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modules.BottomToolBar
{
    /// <summary>
    /// MenueUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MenueUserControl : UserControl
    {
        public MenueUserControl()
        {
            InitializeComponent();

            List<TestC> list = new List<TestC>();
            TestC testc = new TestC();
            testc.id = "我的工作";
            testc.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m1.png", UriKind.Relative));
            TestC testc1 = new TestC();
            testc1.id = "油品业务";
            testc1.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m2.png", UriKind.Relative));
            TestC testc2 = new TestC();
            testc2.id = "非油业务";
            testc2.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m3.png", UriKind.Relative));
            TestC testc3 = new TestC();
            testc3.id = "卡业务";
            testc3.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m3.png", UriKind.Relative));
            TestC testc4 = new TestC();
            testc4.id = "直销业务";
            testc4.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m1.png", UriKind.Relative));
            TestC testc5 = new TestC();
            testc5.id = "客存管理";
            testc5.imgPath = new BitmapImage(new Uri("/Modules.BottomToolBar;component/Images/m2.png", UriKind.Relative));
            list.Add(testc);
            list.Add(testc1);
            list.Add(testc2);
            list.Add(testc3);
            list.Add(testc4);
            list.Add(testc5);

            sp1.ItemsSource = list;
        }


        private void sp1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            TestC t = sp1.SelectedItem as TestC;
            MessageBox.Show(t.id);
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Label lb = sender as Label;
            MessageBox.Show(lb.Content.ToString());
        }
    }
    public class TestC
    {
        public string id { get; set; }
        public ImageSource imgPath { get; set; }
    }
}
