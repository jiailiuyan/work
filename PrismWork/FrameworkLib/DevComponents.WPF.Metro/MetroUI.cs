using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// A static class which exports the attached dependency property Theme. Theme is a property of type nullable MetroTheme. 
    /// This attached DP is a behavior. When a non-null value for Theme is set on a FrameworkElement for the first time, a new
    /// instance of MetroColors resource dictionary is created and colors generated based on the MetroTheme value and 
    /// merged into the resources of the framework element. Also, referencing MetroUI in an application will cause
    /// the static constructor to be called in which the global resources required by Metro UI are merged into the 
    /// application's resources.
    /// </summary>
    public static class MetroUI
    {
        static MetroUI()
        {
            if (Application.Current != null)
            {
                // Add global resources required by Metro to the application's resources.
                Application.Current.Resources.MergedDictionaries.Insert(0, MetroStrings.Default);
                Application.Current.Resources.MergedDictionaries.Insert(1, CustomControlsResources.Default);
            }
        }

        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(MetroTheme?), typeof(MetroUI), new UIPropertyMetadata(null,
                (s, e) =>
                {
                    var fe = s as FrameworkElement;
                    if (fe == null)
                        return;
                        
                    SetupMetroThemeOnElement(fe, (MetroTheme?)e.NewValue);

                    // It seems to be necessary to ensure that the theme got applied when element is not loaded by re-applying after 
                    // the element is loaded. And it is still necessary to setup theme initially when element is not loaded. I've seen
                    // case where, when fe is not loaded, the call above to setup theme fails (MetroBill sample) and I've also seen
                    // a case where, still fe is not loaded, the call above is absolutely necessary (SampleBrowserApp) and the delegate below is useless.
                    if (!fe.IsLoaded)
                    {
                        RoutedEventHandler h = null;
                        h = delegate
                        {
                            fe.Loaded -= h;
                            SetupMetroThemeOnElement(fe, (MetroTheme?)e.NewValue);
                        };

                        fe.Loaded += h;
                    }
                }));

        private static void SetupMetroThemeOnElement(FrameworkElement element, MetroTheme? newTheme)
        {
            if (newTheme != null && !newTheme.Equals(MetroTheme.NullTheme))
            {
                var metroColors = element.Resources.MergedDictionaries.OfType<MetroColors>().SingleOrDefault();
                if (metroColors == null)
                {
                    metroColors = new MetroColors();
                    element.Resources.MergedDictionaries.Insert(0, metroColors);
                }
                metroColors.Theme = newTheme.Value;
            }
            else
            {
                var metroColors = element.Resources.MergedDictionaries.OfType<MetroColors>().SingleOrDefault();
                if (metroColors != null)
                    element.Resources.MergedDictionaries.Remove(metroColors);
            }
        }

        /// <summary>
        /// Gets the Theme that has been set on the object.
        /// </summary>
        public static MetroTheme? GetTheme(DependencyObject obj)
        {
            return (MetroTheme?)obj.GetValue(ThemeProperty);
        }
        /// <summary>
        /// Sets the Theme on the object.
        /// </summary>
        public static void SetTheme(DependencyObject obj, MetroTheme? value)
        {
            obj.SetValue(ThemeProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Theme. This is a attached property which implements
        /// a behavior which merges a resource dictionary containing Metro color resources into the resources of Application.Current.
        /// </summary>
        public static readonly DependencyProperty ApplicationThemeProperty =
            DependencyProperty.RegisterAttached("ApplicationTheme", typeof(MetroTheme?), typeof(MetroUI), new UIPropertyMetadata(null,
                (s, e) =>
                {
                    var colors = Application.Current.Resources.MergedDictionaries.OfType<MetroColors>().SingleOrDefault();

                    if (e.NewValue == null)
                    {
                        if (colors != null)
                            Application.Current.Resources.MergedDictionaries.Remove(colors);
                    }
                    else
                    {
                        if (colors == null)
                        {
                            colors = new MetroColors();
                            Application.Current.Resources.MergedDictionaries.Insert(0, colors);
                        }
                        colors.Theme = (MetroTheme)e.NewValue;
                    }
                }));
        /// <summary>
        /// Gets the MetroTheme which is applied application wide.
        /// </summary>
        public static MetroTheme? GetApplicationTheme(DependencyObject obj)
        {
            return (MetroTheme?)obj.GetValue(ApplicationThemeProperty);
        }
        /// <summary>
        /// Sets the MetroTheme which is applied application wide.
        /// </summary>
        public static void SetApplicationTheme(DependencyObject obj, MetroTheme? value)
        {
            obj.SetValue(ApplicationThemeProperty, value);
        }
        
        /// <summary>
        /// Gets the effective theme for the object by walking the visual and/or logical trees
        /// returning the first element which has non-null value for Theme.
        /// </summary>
        public static MetroTheme? GetEffectiveTheme(DependencyObject obj)
        {
            var target = obj;

            while (target != null)
            {
                MetroTheme? theme = GetTheme(target);
                if (theme != null)
                    return theme.Value;

                DependencyObject temp = null;
                if (target is Visual || target is Visual3D)
                    temp = VisualTreeHelper.GetParent(target);
                if (temp == null)
                    temp = LogicalTreeHelper.GetParent(target);
                target = temp;
            }

            var colors = Application.Current.Resources.MergedDictionaries.OfType<MetroColors>().FirstOrDefault();
            if (colors != null)
                return colors.Theme;

            return null;
        }

        internal static void EnsureTheme(Control metroControl)
        {
            var effectiveTheme = GetEffectiveTheme(metroControl);
            if (effectiveTheme == null)
            {
                var window = Window.GetWindow(metroControl);
                if (window != null)
                {
                    SetTheme(window, MetroTheme.Default);
                }
            }
        }

    }
}
