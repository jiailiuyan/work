using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using WorkCommon.Plugin;

namespace WorkCommon.Manager
{
    public class GlobalManager
    {

        public AggregateCatalog AggregateCatalog { get; private set; }

        private static GlobalManager instance;
        public static GlobalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalManager();
                }
                return instance;
            }
        }

        public GlobalManager()
        {

        }

        public void InitManager(AggregateCatalog aggregateCatalog)
        {
            this.AggregateCatalog = aggregateCatalog;
            PluginManager.Instance.LoadPlugin();
        }

    }
}
