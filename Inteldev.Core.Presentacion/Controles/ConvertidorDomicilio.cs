using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Inteldev.Core.Presentacion.Controles
{
    public class ConvertidorDomicilio:IMultiValueConverter
    {
        
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (string.Join("",values).ToString().Length == 0)
                return "Falta indicar Domicilio";
            else
            {
                return String.Format("{0} {1} {2} {3}",
                    values[0],
                    values[1].ToString().Length > 0 ? "Nº:" + values[1].ToString() : "",
                    values[2].ToString().Length > 0 ? "Piso:" + values[2].ToString() : "",
                    values[3].ToString().Length > 0 ? "Dpto:" + values[3].ToString() : "");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
