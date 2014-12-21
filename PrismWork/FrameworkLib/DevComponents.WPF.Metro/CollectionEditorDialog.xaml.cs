using System.ComponentModel;
using System.Windows.Input;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Interaction logic for CollectionEditorDialog.xaml
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CollectionEditorDialog
    {
        public CollectionEditorDialog()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, new ExecutedRoutedEventHandler(HandleApplicationCloseCommand)));

            InitializeComponent();
        }
        private void HandleApplicationCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if ((string)e.Parameter == "Ok")
                Editor.PersistChanges();
            Close();
        }
    }
}
