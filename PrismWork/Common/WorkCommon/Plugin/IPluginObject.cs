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

        string PluginName { get; }

        ImageSource PluginIcon { get; }

        FrameworkElement Plugin { get; }

        PluginType Type { get; }

        event EventHandler Opening;

        event EventHandler Closing;

        event EventHandler Hiding;

        bool IsShow { get; set; }
    }

    public enum PluginType
    {
        Window,
        Plugin

    }
}
