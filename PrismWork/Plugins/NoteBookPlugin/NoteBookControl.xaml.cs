using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WorkCommon.Plugin;

namespace NoteBookPlugin
{
    /// <summary>
    /// NoteBookControl.xaml 的交互逻辑
    /// </summary>
    [ExportAttribute(typeof(IPluginObject))]
    public partial class NoteBookControl : UserControl, IPluginObject
    {

        ObservableCollection<string> content= new ObservableCollection<string>();
    
        private FrameworkElement pluginobject;

        public NoteBookControl()
        {
            InitializeComponent();
            content.Add("1 发票离线控制：   0笔 ");
            content.Add("2 单车收油监控：   0笔 ");
            content.Add("3 撤销交易监控：   0笔 ");
            content.Add("4 结算超时监控：   0笔 ");
            content.Add("5 脱机交易监控：   0笔 ");
            content.Add("6 自用油监控 ：   0笔  ");
           this.listbox.ItemsSource = content;
           // this.DataContext = this;
          
        }

        public string PluginName
        {
            get { return "记事本"; }
        }

        public ImageSource PluginIcon
        {
            get { return null; }
        }

        public FrameworkElement Plugin
        {
            get
            {
                if (pluginobject == null)
                {
                    pluginobject = new NoteBookControl();
                }
                return pluginobject;
            }
        }
    }
}
