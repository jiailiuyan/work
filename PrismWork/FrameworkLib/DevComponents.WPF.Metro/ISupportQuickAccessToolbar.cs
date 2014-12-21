using System.Windows;

namespace DevComponents.WPF.Metro
{
    public interface ISupportQuickAccessToolBar
    {
        /// <summary>
        /// Create a "clone" of the item which can be added to the QAT.
        /// </summary>
        FrameworkElement CloneForQuickAccessToolbar();
    }
}
