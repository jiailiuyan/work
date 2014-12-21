using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkCommon.Plugin;

namespace ControlLib
{

    public enum PluginAction
    {

    }

    public class AddControlArgs : EventArgs
    {
        public IPluginObject PluginObject { get; set; }

    }

}
