using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Inteldev.Core.Extenciones;

namespace Inteldev.Core.Presentacion
{
    public class CamelCaseConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value.ToString().SplitCamelCase();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Replace(" ", "");
        }
    }
}
