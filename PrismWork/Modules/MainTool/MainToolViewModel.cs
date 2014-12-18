using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using WorkCommon.Plugin;
using WorkCommon.ViewModel;

namespace Modules.MainTool
{
    [Export(typeof(MainToolViewModel))]
    public class MainToolViewModel : BaseObject
    {

        private ObservableCollection<IPluginObject> pluginObjects;
        public ObservableCollection<IPluginObject> PluginObjects
        {
            get
            {
                return pluginObjects;
            }
            set
            {
                pluginObjects = value;
            }
        }

        [ImportingConstructor]
        public MainToolViewModel(IEventAggregator eventAggregator)
        {
            InitPlugins();
        }

        private void InitPlugins()
        {
            PluginObjects = PluginManager.Instance.PluginObjects;
        }

    }

}
