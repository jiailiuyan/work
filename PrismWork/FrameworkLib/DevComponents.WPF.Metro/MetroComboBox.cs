using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of a ComboBox which supports being added to the QAT by virtue of implementing ISupportQuickAccessToolBar.
    /// </summary>
    [DesignTimeVisible(true)]
    public class MetroComboBox : ComboBox, ISupportQuickAccessToolBar
    {
        static MetroComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroComboBox), new FrameworkPropertyMetadata(typeof(MetroComboBox)));
        }

        private MetroComboBox _CloneSource;
        private WeakReference _CloneRef;
        private ContentPresenter _ContentPresenter;

        /// <summary>
        // Using a DependencyProperty as the backing store for FrozenImageSource.
        /// </summary>
        internal static readonly DependencyProperty FrozenImageProperty =
            DependencyProperty.Register("FrozenImage", typeof(Image), typeof(MetroComboBox), new UIPropertyMetadata(null));
        internal Image FrozenImage
        {
            get { return (Image)GetValue(FrozenImageProperty); }
            set { SetValue(FrozenImageProperty, value); }
        }

        /// <summary>
        /// Performs cloning for the QAT.
        /// </summary>
        public FrameworkElement CloneForQuickAccessToolbar()
        {
            var clone = new MetroComboBox();
            clone._CloneSource = this;

            clone.Bind(IsReadOnlyProperty, this, IsReadOnlyProperty);
            clone.Bind(IsEditableProperty, this, IsEditableProperty);
            clone.Bind(TextProperty, this, TextProperty, BindingMode.TwoWay);
            clone.Bind(DisplayMemberPathProperty, this, DisplayMemberPathProperty);
            clone.Bind(ItemContainerStyleProperty, this, ItemContainerStyleProperty);
            clone.Bind(ItemsPanelProperty, this, ItemsPanelProperty);
            clone.Bind(ItemStringFormatProperty, this, ItemStringFormatProperty);
            clone.Bind(ItemTemplateProperty, this, ItemTemplateProperty);
            clone.Bind(SelectedValuePathProperty, this, SelectedValuePathProperty);
            clone.Bind(MaxDropDownHeightProperty, this, MaxDropDownHeightProperty);
            clone.Bind(MinWidthProperty, this, MinWidthProperty);

            bool bindItemsSource = false;
            var itemsSource = ItemsSource;
            if (itemsSource != null)
            {
                bindItemsSource = true;
                foreach (var item in itemsSource)
                {
                    if (item is Visual)
                    {
                        bindItemsSource = false;
                        break;
                    }
                }
                if (bindItemsSource)
                {
                    clone.Bind(ItemsSourceProperty, this, ItemsSourceProperty);
                    clone.Bind(SelectedItemProperty, this, SelectedItemProperty, BindingMode.TwoWay);
                }
            }

            if (!bindItemsSource && !IsEditable)
                ApplyFrozenImageToClone(clone);

            _CloneRef = new WeakReference(clone);

            return clone;
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ContentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;

            if (ContextMenuProperty.IsUnsetValue(this))
            {
                MetroChrome chrome = null;
                var window = Window.GetWindow(this);
                if (window != null && ((chrome = window.GetFirstDescendent<MetroChrome>()) != null) && chrome.QuickAccessToolBar != null)
                {
                    SetResourceReference(ContextMenuProperty, QuickAccessToolBar.AddItemContextMenuKey);
                }
            }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            if (_CloneSource == null || !IsEditable)
                return;

            if ((e.OriginalSource is TextBox) && !IsDropDownOpen)
            {
                TransferItems(_CloneSource, this);
            }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            if (_CloneSource == null || !IsEditable || IsKeyboardFocusWithin)
                return;

            TransferItems(this, _CloneSource);
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);

            // Is this instance cloned?
            if (_CloneSource != null)
            {
                if (!IsEditable)
                {
                    ApplyFrozenImage(_CloneSource, _CloneSource);
                    FrozenImage = null;
                }
                TransferItems(_CloneSource, this);
            }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);

            if (_CloneSource != null)
            { // This instance is cloned.                
                Dispatcher.BeginInvoke((Action)delegate
                {
                    ApplyFrozenImage(this, this);
                    TransferItems(this, _CloneSource);
                }, DispatcherPriority.ApplicationIdle);
            }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_CloneRef == null)
            {
                return;
            }
            if (!_CloneRef.IsAlive)
            {
                _CloneRef = null;
                return;
            }
            if (IsEditable)
                return;

            var clone = (MetroComboBox)_CloneRef.Target;
            if (!clone.IsDropDownOpen)
                ApplyFrozenImageToClone(clone);
        }

        private void ApplyFrozenImageToClone(MetroComboBox clone)
        {
            // Without the dispatch, the image doesn't show up.
            Dispatcher.BeginInvoke(new Action<MetroComboBox, MetroComboBox>(ApplyFrozenImage), DispatcherPriority.ContextIdle, this, clone);
        }

        private static void ApplyFrozenImage(MetroComboBox source, MetroComboBox target)
        {
            if (source._ContentPresenter == null)
                return;

            var visual = source._ContentPresenter.GetFirstDescendent<FrameworkElement>();
            if (visual == null)
                return;

            int sourceWidth = (int)Math.Round(visual.ActualWidth);
            int sourceHeight = (int)Math.Round(visual.ActualHeight);
            var bitmap = new RenderTargetBitmap(sourceWidth, sourceHeight, 96, 96, PixelFormats.Pbgra32);

            bitmap.Render(visual);

            var image = new Image
            {
                Source = bitmap,
                Stretch = Stretch.None
            };
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);

            target.FrozenImage = image;
            if (source != target)
                source.FrozenImage = null;
        }

        private static void TransferItems(MetroComboBox source, MetroComboBox target)
        {
            if (source.Items.Count == 0)
                return;

            var selectedItem = source.SelectedItem;
            source.SelectedItem = null;
            for (int i = source.Items.Count - 1; i >= 0; i--)
            {
                var item = source.Items[i];
                source.Items.Remove(item);
                target.Items.Add(item);
            }
            target.SelectedItem = selectedItem;
        }

    }
}
