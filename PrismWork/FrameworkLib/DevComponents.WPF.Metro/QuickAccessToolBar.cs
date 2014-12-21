using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of QuickAccessToolbar for Metro.
    /// </summary>
    public class QuickAccessToolBar : ToolBar
    {
        /// <summary>
        /// Serves as wrapper for items which are included in the Customize drop down.
        /// </summary>
        public class CustomizableItem : INotifyPropertyChanged
        {
            public CustomizableItem(object item)
            {
                Item = item;
                var dp = item as DependencyObject;
                if (dp != null)
                    Name = QuickAccessToolBar.GetDisplayName(dp);

                if (String.IsNullOrEmpty(Name))
                    Name = item.ToString();
            }

            public object Item { get; private set; }
            public string Name { get; private set; }
            public string Id { get; private set; }

            private Visibility _Visibility;
            public Visibility Visibility
            {
                get { return _Visibility; }
                set
                {
                    if (value != _Visibility)
                    {
                        _Visibility = value;
                        OnPropertyChanged("Visibility");
                    }
                }
            }

            private void OnPropertyChanged(string propertyName)
            {
                var h = PropertyChanged;
                if (h != null)
                    h(this, new PropertyChangedEventArgs(propertyName));
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        #region Construction and Fields

        /// <summary>
        /// Routed command for adding an item to the QuickAccessToolbar. When executed, command parameter should be set to the item being added.
        /// </summary>
        public static readonly RoutedUICommand AddToQATCommand = new RoutedUICommand(MetroStrings.Default.AddToQATCommandString, "AddToQATCommand", typeof(QuickAccessToolBar));

        /// <summary>
        /// Routed command for removing an item from the QuickAccessToolbar. When executed, command parameter should be set to the item being removed.
        /// </summary>
        public static readonly RoutedUICommand RemoveFromQATCommand = new RoutedUICommand(MetroStrings.Default.RemoveFromQATCommandString, "RemoveFromQATCommand", typeof(QuickAccessToolBar));

        /// <summary>
        /// Routed command for customizing the QAT.
        /// </summary>
        public static readonly RoutedUICommand CustomizeQATCommand = new RoutedUICommand(MetroStrings.Default.RemoveFromQATCommandString, "CustomizeQATCommand", typeof(QuickAccessToolBar));

        /// <summary>
        /// Identifies the default context menu for items which are in the QAT.
        /// </summary>
        public static readonly ComponentResourceKey RemoveItemContextMenuKey = new ComponentResourceKey(typeof(QuickAccessToolBar), "RemoveItemContextMenuKey");
        /// <summary>
        /// Identifies the default context menu for items which can be added the QAT.
        /// </summary>
        public static readonly ComponentResourceKey AddItemContextMenuKey = new ComponentResourceKey(typeof(QuickAccessToolBar), "AddItemContextMenuKey");

        static QuickAccessToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QuickAccessToolBar), new FrameworkPropertyMetadata(typeof(QuickAccessToolBar)));

            // OverrideMetadata to set up Coersion which prevents overflow panel from closing when customize menu is open
            // since customize button is in overflow panel if there are overflow items.
            ToolBar.IsOverflowOpenProperty.OverrideMetadata(typeof(QuickAccessToolBar), new FrameworkPropertyMetadata(false, null,
                (s, v) =>
                {
                    if ((bool)v == false)
                    {
                        var toolBar = (QuickAccessToolBar)s;
                        // Unlike property changed handlers, coersion handlers seem to be called even when there is
                        // no change in property value.
                        if (toolBar.IsOverflowOpen)
                        {
                            if (toolBar.IsCustomizeMenuOpen)
                                return true;
                        }
                    }
                    return v;
                }));
        }


        #endregion // Construction and Fields

        #region CLR Properties

        private ObservableCollection<CustomizableItem> _CustomizableItems = new ObservableCollection<CustomizableItem>();
        /// <summary>
        /// Gets the collection of wrappers for the items which are in the customizable collection.
        /// </summary>
        public ObservableCollection<CustomizableItem> CustomizableItems
        {
            get { return _CustomizableItems; }
        }

        #endregion // Clr Properties

        #region Attached Dependency Properties

        /// <summary>
        /// Using a DependencyProperty as the backing store for CanAddToQuickAccessToolbar.
        /// </summary>
        public static readonly DependencyProperty CanAddToQuickAccessToolbarProperty =
            DependencyProperty.RegisterAttached("CanAddToQuickAccessToolbar", typeof(bool), typeof(QuickAccessToolBar), new PropertyMetadata(true));
        /// <summary>
        /// Get whether an instance of a class which implements ISupportQuickAccessToolbar can be placed in the QAT. Default value is true.
        /// </summary>
        public static bool GetCanAddToQuickAccessToolbar(DependencyObject obj)
        {
            if (obj == null) return false;
            return (bool)obj.GetValue(CanAddToQuickAccessToolbarProperty);
        }
        /// <summary>
        /// Set whether an item can be added to the QAT. The default value is true. Note that only items which implement interface ISupportQuickAccessToolbar 
        /// can be added to the QAT. This property is a means of disabling QAT for a specific instance of a class which implements the interface.
        /// </summary>
        public static void SetCanAddToQuickAccessToolbar(DependencyObject obj, bool value)
        {
            obj.SetValue(CanAddToQuickAccessToolbarProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for DisplayName.
        /// </summary>
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.RegisterAttached("DisplayName", typeof(string), typeof(QuickAccessToolBar), new PropertyMetadata(null));
        /// <summary>
        /// Get the value displayed for an item when the Quick Access Toolbar needs to reference it by a name rather than the actual value.
        /// </summary>
        public static string GetDisplayName(DependencyObject obj)
        {
            return (string)obj.GetValue(DisplayNameProperty);
        }
        /// <summary>
        /// Set the value displayed for an item when the Quick Access Toolbar needs to reference it by a name rather than the actual value.
        /// </summary>
        public static void SetDisplayName(DependencyObject obj, string value)
        {
            obj.SetValue(DisplayNameProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Id.
        /// </summary>
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.RegisterAttached("Id", typeof(string), typeof(QuickAccessToolBar), new PropertyMetadata(null));
        /// <summary>
        /// Get the value of attached property Id for an element.
        /// </summary>
        public static string GetId(DependencyObject obj)
        {
            return (string)obj.GetValue(IdProperty);
        }
        /// <summary>
        /// Set the value of attached property Id for an element.
        /// </summary>
        public static void SetId(DependencyObject obj, string value)
        {
            obj.SetValue(IdProperty, value);
        }

        private static readonly DependencyProperty IsCloneProperty =
            DependencyProperty.RegisterAttached("IsClone", typeof(bool), typeof(QuickAccessToolBar), new UIPropertyMetadata(false));

        #endregion  // Attached dependency properties

        #region Dependency Properties

        /// <summary>
        /// Using a DependencyProperty as the backing store for ActiveItems.
        /// </summary>
        public static readonly DependencyProperty SerializationStringProperty =
            DependencyProperty.Register("SerializationString", typeof(string), typeof(QuickAccessToolBar), new PropertyMetadata(null,
                (s, e) =>
                {
                    ((QuickAccessToolBar)s).HandleSerializationStringChanged((string)e.NewValue);
                }));
        /// <summary>
        /// Get or set a string can be saved and used later to load current items into the QAT.
        /// </summary>
        public string SerializationString
        {
            get { return (string)GetValue(SerializationStringProperty); }
            set { SetCurrentValue(SerializationStringProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for IsCustomizeMenuOpen.
        /// </summary>
        public static readonly DependencyProperty IsCustomizeMenuOpenProperty =
            DependencyProperty.Register("IsCustomizeMenuOpen", typeof(bool), typeof(QuickAccessToolBar), new UIPropertyMetadata(false));
        /// <summary>
        /// Get or set whether the customize menu is open.
        /// </summary>
        public bool IsCustomizeMenuOpen
        {
            get { return (bool)GetValue(IsCustomizeMenuOpenProperty); }
            set { SetCurrentValue(IsCustomizeMenuOpenProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsLocked.
        /// </summary>
        public static readonly DependencyProperty IsLockedProperty =
            DependencyProperty.Register("IsLocked", typeof(bool), typeof(QuickAccessToolBar), new PropertyMetadata(false));
        /// <summary>
        /// Get or Set whether the Quick Access Toolbar is locked. When locked, items cannot be added or removed from the toolbar. 
        /// This is a dependency property. The default value is false.
        /// </summary>
        public bool IsLocked
        {
            get { return (bool)GetValue(IsLockedProperty); }
            set { SetCurrentValue(IsLockedProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for ShowCustomizeButton.
        /// </summary>
        public static readonly DependencyProperty ShowCustomizeButtonProperty =
            DependencyProperty.Register("ShowCustomizeButton", typeof(bool), typeof(QuickAccessToolBar), new UIPropertyMetadata(true));
        /// <summary>
        /// Gets or sets whether the customize button is shown. Default is true.
        /// </summary>
        public bool ShowCustomizeButton
        {
            get { return (bool)GetValue(ShowCustomizeButtonProperty); }
            set { SetCurrentValue(ShowCustomizeButtonProperty, value); }
        }

        #endregion  // Dependency Properties

        #region Overrides

        private PopupButton _CustomizeButton;

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.CommandBindings.Add(new CommandBinding(QuickAccessToolBar.AddToQATCommand, AddToQATCommandExecuted, AddToQATCommandCanExecute));
                window.CommandBindings.Add(new CommandBinding(QuickAccessToolBar.RemoveFromQATCommand, RemoveFromQATCommandExecuted, RemoveFromQATCommandCanExecute));
                window.CommandBindings.Add(new CommandBinding(QuickAccessToolBar.CustomizeQATCommand, CustomizeQATCommandExecuted, CustomizeQATCommandCanExecute));
            }

            _CustomizeButton = GetTemplateChild("CustomizeMenuButton") as PopupButton;
            if (_CustomizeButton != null)
            {
                var panel = _CustomizeButton.Parent as Panel;
                if (panel != null)
                {
                    panel.Children.Remove(_CustomizeButton);
                    Items.Add(new Separator());
                    Items.Add(_CustomizeButton);
                }
                _CustomizeButton.PopupClosed += delegate { IsOverflowOpen = false; };
            }

            Dispatcher.BeginInvoke(new Action(LoadFromSerializationString), DispatcherPriority.ApplicationIdle);

        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (IsLoaded)
                UpdateSerializationString();
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var fe = element as FrameworkElement;
            if (fe != null && fe != _CustomizeButton)
            {
                if (FrameworkElement.ContextMenuProperty.IsUnsetValue(fe))
                    fe.SetResourceReference(FrameworkElement.ContextMenuProperty, QuickAccessToolBar.RemoveItemContextMenuKey);

                // Items can be added directly by client, in which case they go into the customizable list, or
                // they can be added internally when user elects to add item to QAT, in which case they do 
                // not go into the customize list.
                if ((bool)fe.GetValue(IsCloneProperty) == false && !(item is Separator))
                {
                    var customizeListWrapper = new CustomizableItem(item);
                    customizeListWrapper.Visibility = fe.Visibility;
                    customizeListWrapper.PropertyChanged += HandleCustomizableItemVisibilityChanged;
                    fe.Bind(VisibilityProperty, customizeListWrapper, "Visibility", BindingMode.TwoWay);
                    CustomizableItems.Add(customizeListWrapper);
                }
            }
        }

        /// <summary>
        /// Overriding to ensure proper state.
        /// </summary>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            var customizable = CustomizableItems.Where(c => c.Item == item).FirstOrDefault();
            if (customizable != null)
            {
                customizable.PropertyChanged -= HandleCustomizableItemVisibilityChanged;
                CustomizableItems.Remove(customizable);
            }
        }

        #endregion // Overrides

        #region Public

        /// <summary>
        /// Adds a clone of the item to the Items collection.
        /// </summary>
        public void Add(ISupportQuickAccessToolBar item)
        {
            if (IsLocked)
                return;

            string id = GetEffectiveId((DependencyObject)item);
            if (Contains(id))
                return;

            var clone = item.CloneForQuickAccessToolbar();
            SetId(clone, id);
            clone.SetValue(IsCloneProperty, true);
            InsertItem(clone);
        }

        /// <summary>
        /// Returns true if an element based on item has been added to the QAT.
        /// </summary>
        public bool Contains(ISupportQuickAccessToolBar item)
        {
            return Contains(GetEffectiveId((DependencyObject)item));
        }

        /// <summary>
        /// Return true if an item with the givin id has been added to the Items collection.
        /// </summary>
        public bool Contains(string id)
        {
            foreach (var item in Items)
            {
                var container = ItemContainerGenerator.ContainerFromItem(item);
                if (GetEffectiveId(container) == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the item from the view. If the item is one of the customizable items, then it is 
        /// collapsed, if it is an item added to the QAT from the UI, it is removed from the Items collection.
        /// </summary>
        public void Remove(object item)
        {
            if (!Items.Contains(item))
            {
                var dp = item as DependencyObject;
                if (dp != null)
                {
                    item = ItemContainerGenerator.ItemFromContainer(dp);
                    if (item == null || !Items.Contains(item))
                        return;
                }
                else
                {
                    return;
                }
            }

            var customizable = CustomizableItems.Where(c => c.Item == item).FirstOrDefault();
            if (customizable != null)
            {
                customizable.Visibility = Visibility.Collapsed;
            }
            else
            {
                Items.Remove(item);
            }
        }

        #endregion  // Public

        #region Command Handlers

        private void AddToQATCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as ISupportQuickAccessToolBar;
            if (item != null)
                Add(item);
        }

        private void AddToQATCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var qatItem = e.Parameter as ISupportQuickAccessToolBar;
            e.CanExecute = qatItem != null && !Contains(qatItem);
        }

        private void RemoveFromQATCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Remove(e.Parameter);
        }

        private void RemoveFromQATCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = e.Parameter;
            e.CanExecute = Contains(item);
            if (!e.CanExecute)
                e.CanExecute = Items.Contains(e.Parameter);
        }

        private void CustomizeQATCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)delegate { IsCustomizeMenuOpen = true; }, DispatcherPriority.ApplicationIdle);
        }

        private void CustomizeQATCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion // Command Handlers

        #region Private

        private void HandleCustomizableItemVisibilityChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(UpdateSerializationString), DispatcherPriority.ContextIdle);
        }

        private bool Contains(object item)
        {
            foreach (var i in Items)
            {
                if (i == item)
                    return true;
                else
                {
                    var container = ItemContainerGenerator.ContainerFromItem(i);
                    if (item == container)
                        return true;
                }
            }
            return false;
        }

        // Insert the item into the Items collection just in front of the customize button.
        private void InsertItem(object item)
        {
            var index = Items.Count - 2;
            Items.Insert(index, item);
        }

        private string GetEffectiveId(DependencyObject obj)
        {
            string id = GetId(obj);
            if (String.IsNullOrEmpty(id))
            {
                var fe = obj as FrameworkElement;
                if (fe != null)
                    id = fe.Name;
            }
            if (String.IsNullOrEmpty(id))
                id = GetDisplayName(obj);
            if (String.IsNullOrEmpty(id))
                id = obj.GetHashCode().ToString();
            return id;
        }

        private bool _UpdatingSerializationString;
        private void HandleSerializationStringChanged(string ids)
        {
            if (!_UpdatingSerializationString)
                LoadFromSerializationString();
        }

        private void UpdateSerializationString()
        {
            var sb = new StringBuilder();
            foreach (var item in GetContainers())
            {
                var container = ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                if (container.Visibility == Visibility.Visible)
                {
                    var id = GetEffectiveId(container);
                    sb.Append(id);
                    sb.Append(",");
                }
            }
            var str = sb.ToString();
            if (SerializationString != str)
            {
                _UpdatingSerializationString = true;
                SerializationString = str;
                _UpdatingSerializationString = false;
            }
        }

        private IEnumerable<UIElement> GetContainers()
        {
            foreach (var item in Items)
            {
                var container = ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                if (container != null && !(container is Separator) && container != _CustomizeButton)
                    yield return container;
            }
        }

        private void LoadFromSerializationString()
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow == null || !IsLoaded || !parentWindow.IsLoaded)
                return;

            var serialization = SerializationString;
            if (String.IsNullOrEmpty(serialization))
                return;

            List<ISupportQuickAccessToolBar> qatSupporters = null;
            var localContainers = GetContainers().ToList();

            // Hide all current items, set each visible if it is included in serialization string.
            foreach (var container in localContainers)
                container.Visibility = Visibility.Collapsed;

            string[] ids = serialization.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in ids)
            {
                bool found = false;

                // Look for item with id in existing containers.
                foreach (var container in localContainers)
                {
                    var containerID = GetEffectiveId(container);
                    if (containerID == id)
                    {
                        container.Visibility = Visibility.Visible;
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;

                // The item with givin ID is not one of the local items. Look in UI for items which implement ISupportQuickAccessToolBar
                var qatSupporter = FindQATSupporter(id, ref qatSupporters);
                if (qatSupporter != null)
                {
                    Add(qatSupporter);
                }
            }
        }

        private ISupportQuickAccessToolBar FindQATSupporter(string id, ref List<ISupportQuickAccessToolBar> qatSupporters)
        {
            var window = Window.GetWindow(this);
            if (window == null) return null;

            if (qatSupporters == null)
            {
                qatSupporters = window.GetVisuals<ISupportQuickAccessToolBar>().ToList();
            }

            foreach (var qatSupporter in qatSupporters)
            {
                if (GetEffectiveId((DependencyObject)qatSupporter) == id)
                    return qatSupporter;
            }

            var metroShell = window.GetFirstDescendent<MetroShell>();
            if (metroShell == null)
                return null;

            var selectedTab = metroShell.SelectedItem;
            ISupportQuickAccessToolBar target = null;

            try
            {
                foreach (var item in metroShell.Items)
                {
                    if (item != selectedTab)
                    {
                        metroShell.SelectedItem = item;
                        DispatcherHelper.WaitForPriority(DispatcherPriority.ContextIdle);
                        var supporters = metroShell.GetVisuals<ISupportQuickAccessToolBar>().ToList();
                        qatSupporters.AddRange(supporters);
                        foreach (var qatSupporter in supporters)
                        {
                            if (GetEffectiveId((DependencyObject)qatSupporter) == id)
                            {
                                target = qatSupporter;
                                break;
                            }
                        }
                    }

                    if (target != null)
                        break;
                }
            }
            catch { } // Don't want unexpected errors to cause a crash. Doing something a bit unusual after all...
            finally
            {
                metroShell.SelectedItem = selectedTab;
            }
            return target;
        }

        #endregion // Private

    }
}
