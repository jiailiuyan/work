using System.Windows;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// ResourceDictionary which defines implicit Styles for common controls. This resource dictionary
    /// is merged into Application.Current.Resources.MergedDictionaries.
    /// </summary>
    public partial class CommonControlsResources
    {
        public static readonly ComponentResourceKey ButtonStyleKey = ButtonResources.ButtonStyleKey;
        public static readonly ComponentResourceKey ToggleButtonStyleKey = ButtonResources.ToggleButtonStyleKey;
        public static readonly ComponentResourceKey RadioButtonStyleKey = ButtonResources.RadioButtonStyleKey;
        public static readonly ComponentResourceKey CheckBoxStyleKey = ButtonResources.CheckBoxStyleKey;
        public static readonly ComponentResourceKey TextBoxStyleKey = CommonResources.TextBoxStyleKey;

        public static readonly ComponentResourceKey MenuItemStyleKey = MenuResources.MenuItemStyleKey;
        public static readonly ComponentResourceKey MenuItemTemplateKey = MenuResources.MenuItemTemplateKey;
        public static readonly ComponentResourceKey ContextMenuStyleKey = MenuResources.ContextMenuStyleKey;
        public static readonly ComponentResourceKey ComboBoxStyleKey = SelectorResources.ComboBoxStyleKey;
        public static readonly ComponentResourceKey ComboBoxIsEditableTemplateKey = SelectorResources.ComboBoxIsEditableTemplateKey;
        public static readonly ComponentResourceKey ComboBoxUneditableTemplateKey = SelectorResources.ComboBoxNonIsEditableTemplateKey;
        public static readonly ComponentResourceKey ComboBoxItemStyleKey = SelectorResources.ComboBoxItemStyleKey;
        public static readonly ComponentResourceKey ListBoxStyleKey = SelectorResources.ListBoxStyleKey;
        public static readonly ComponentResourceKey ListBoxItemStyleKey = SelectorResources.ListBoxItemStyleKey;

        public static readonly ComponentResourceKey PasswordBoxStyleKey = new ComponentResourceKey(typeof(CommonControlsResources), "PasswordBoxStyleKey");
        public static readonly ComponentResourceKey ScrollBarStyleKey = new ComponentResourceKey(typeof(CommonControlsResources), "ScrollBarStyleKey");
        public static readonly ComponentResourceKey ToolTipStyleKey = new ComponentResourceKey(typeof(CommonControlsResources), "ToolTipStyleKey");
        public static readonly ComponentResourceKey ScrollViewerStyleKey = new ComponentResourceKey(typeof(CommonControlsResources), "ScrollViewerStyleKey");

        public static readonly ComponentResourceKey ScrollBarSizeKey = new ComponentResourceKey(typeof(CommonControlsResources), "ScrollBarSizeKey");
        public static readonly ComponentResourceKey VerticalScrollBarMarginKey = new ComponentResourceKey(typeof(CommonControlsResources), "VerticalScrollBarMarginKey");
        public static readonly ComponentResourceKey HorizontalScrollBarMarginKey = new ComponentResourceKey(typeof(CommonControlsResources), "HorizontalScrollBarMarginKey");
        public static readonly ComponentResourceKey VerticalScrollBarRepeatButtonPaddingKey = new ComponentResourceKey(typeof(CommonControlsResources), "VerticalScrollBarRepeatButtonPaddingKey");
        public static readonly ComponentResourceKey HorizontalScrollBarRepeatButtonPaddingKey = new ComponentResourceKey(typeof(CommonControlsResources), "HorizontalScrollBarRepeatButtonPaddingKey");

        //public static readonly CommonControlsResources Default = new CommonControlsResources();
        public CommonControlsResources()
        {
            InitializeComponent();
        }
    }
}
