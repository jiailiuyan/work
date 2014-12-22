using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkCommon.Plugin;

namespace WorkCommon.Events
{

    public enum PluginAction
    {
        Add,
        Delete,
        Show,
        Close
    }

    public class PluginsEventArgs
    {
        public PluginAction Action { get; set; }

        public IPluginObject PluginObject { get; set; }

    }

    public class PluginsEvent : Microsoft.Practices.Prism.Events.CompositePresentationEvent<PluginsEventArgs>
    {

    }

}
