using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Inteldev.Core.Presentacion
{
    public class PermisoManager : FrameworkElement
    {

        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for Enabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(PermisoManager), new PropertyMetadata(true, new PropertyChangedCallback(SetEnabled)));

        private static void SetEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                SetHabilitado(d, (bool)e.NewValue);
            };

            d.Dispatcher.BeginInvoke(action);
            //SetHabilitado(d, (bool)e.NewValue);
        }
        private static void SetHabilitado(DependencyObject padre, bool enabled)
        {
            var childs = VisualTreeHelper.GetChildrenCount(padre);
            for (int i = 0; i < childs; i++)
            {
                var child = VisualTreeHelper.GetChild(padre, i);

                if ((child is TextBox ||
                    child is CheckBox ||
                    child is ComboBox ||
                    child is Button)
                    )

                    ((UIElement)child).IsEnabled = enabled;
                else
                    SetHabilitado(child, enabled);

            }
        }


    }
}
