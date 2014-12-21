using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using WorkCommon.ViewModel;
using WorkCommon.Events;
using System.Collections.ObjectModel;
using WorkCommon.Plugin;

namespace Modules.BottomToolBar
{
    [Export(typeof(BottomToolBarViewModel))]
    public class BottomToolBarViewModel : BaseObject
    {

        private ObservableCollection<IPluginObject> pluginObjects = new ObservableCollection<IPluginObject>();
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
        public BottomToolBarViewModel(IEventAggregator eventAggregator)
        {

            //项目工程的切换事件
            eventAggregator.GetEvent<PluginsEvent>().Subscribe(ProjectChangeChanged);
        }


        void ProjectChangeChanged(PluginsEventArgs args)
        {
            switch (args.Action)
            {
                case PluginAction.Add:
                case PluginAction.Show:
                    {
                        if (!PluginObjects.Contains(args.PluginObject))
                        {
                            PluginObjects.Add(args.PluginObject);
                        }
                        break;
                    }

                case PluginAction.Close:
                    {
                        break;
                    }

                case PluginAction.Delete:
                    {
                        PluginObjects.Remove(args.PluginObject);
                        break;
                    }

                default: break;
            }


        }
    }

}
