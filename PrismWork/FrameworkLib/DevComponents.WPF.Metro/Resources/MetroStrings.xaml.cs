using System.Windows;
using System.ComponentModel;

namespace DevComponents.WPF.Metro
{
    public partial class MetroStrings
    {
        public static readonly ComponentResourceKey CustomizeQATMenuHeaderStringKey = new ComponentResourceKey(typeof(MetroStrings), "CustomizeQATMenuHeaderStringKey");
        public static readonly ComponentResourceKey AddToQATCommandStringKey = new ComponentResourceKey(typeof(MetroStrings), "AddToQATCommandStringKey");
        public static readonly ComponentResourceKey RemoveFromQATCommandStringKey = new ComponentResourceKey(typeof(MetroStrings), "RemoveFromQATCommandStringKey");
        public static readonly ComponentResourceKey CustomizeQATCommandStringKey = new ComponentResourceKey(typeof(MetroStrings), "CustomizeQATCommandStringKey");

        public static readonly MetroStrings Default = new MetroStrings();

        public MetroStrings()
        {
            InitializeComponent();
            //if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            //    return;
            CustomizeQATMenuHeaderString = "Customize Quick Access ToolBar";
            CustomizeQATCommandString = "Customize Quick Access ToolBar";
            AddToQATCommandString = "Add to Quick Access ToolBar";
            RemoveFromQATCommandString = "Remove from Quick Access ToolBar";
        }

        public string CustomizeQATMenuHeaderString
        {
            get { return (string)this[CustomizeQATMenuHeaderStringKey]; }
            set { this[CustomizeQATMenuHeaderStringKey] = value; }
        }

        public string AddToQATCommandString
        {
            get { return (string)this[AddToQATCommandStringKey]; }
            set { this[AddToQATCommandStringKey] = value; }
        }

        public string RemoveFromQATCommandString
        {
            get { return (string)this[RemoveFromQATCommandStringKey]; }
            set { this[RemoveFromQATCommandStringKey] = value; }
        }

        public string CustomizeQATCommandString
        {
            get { return (string)this[CustomizeQATCommandStringKey]; }
            set { this[CustomizeQATCommandStringKey] = value; }
        }
    }
}
