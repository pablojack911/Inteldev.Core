using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Core.Presentacion.Controles
{
    public static class DataGridColumnsHelper
    {

        public static Object GetColumnas(DependencyObject obj)
        {
            return (Object)obj.GetValue(ColumnasProperty);
        }

        public static void SetColumnas(DependencyObject obj, Object value)
        {
            obj.SetValue(ColumnasProperty, value);
        }

        // Using a DependencyProperty as the backing store for Columnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnasProperty =
            DependencyProperty.RegisterAttached("Columnas", typeof(Object), typeof(DataGridColumnsHelper));

        
    }
}
