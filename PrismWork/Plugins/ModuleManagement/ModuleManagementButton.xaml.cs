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
using WorkCommon.Plugin;
using Jisons;

namespace ModuleManagement
{
    /// <summary>
    /// ModuleManagementButton.xaml 的交互逻辑
    /// </summary>
    [ExportAttribute(typeof(IPluginObject))]
    public partial class ModuleManagementButton : UserControl, IPluginObject
    {

        private ModuleManagementButton pluginobject;

        #region IPluginObject 成员

        public string PluginName
        {
            get
            {
                return "模块管理";
            }
        }

        public ImageSource PluginIcon
        {
            get
            { 
                return new BitmapImage(new Uri("/ModuleManagement;component/Images/icon1.png", UriKind.Relative));
            }
        }

        public FrameworkElement Plugin
        {
            get
            {
                if (pluginobject == null)
                {
                    pluginobject = new ModuleManagementButton();
                }
                return pluginobject;
            }
        }

        #endregion

        public ModuleManagementButton()
        {
            InitializeComponent();
        }

    }

}