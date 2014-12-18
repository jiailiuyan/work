using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkCommon.Plugin;

namespace WorkCommon.Manager
{
    public class GlobalManager
    {

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

        public void InitManager()
        {
            PluginManager.Instance.LoadPlugin();
        }

    }
}
