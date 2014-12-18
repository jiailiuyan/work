using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WorkCommon.Plugin
{
    public interface IPluginObject
    {

        string PluginName { get;  }

        ImageSource PluginIcon { get; }

        FrameworkElement Plugin { get; }

    }
}
