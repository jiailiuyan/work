using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ControlLib
{
    public interface IActionControl
    {

        bool IsSelected { get; set; }

        ImageSource DefaultView { get; }

        FrameworkElement Element { get; }
    }
}
