using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Core.Presentacion
{
	/// <summary>
	/// Clase Estatica para mostrar Cuadros de Dialogo. 
	/// </summary>
    public static class Dialogos
    {
        /// <summary>
        /// Dialogo de pregunta. O mejor conocido como prompt.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el dialogo</param>
        /// <returns>True si usuario responde si. False si responde que no.</returns>
        public static bool PreguntaSiNo(string mensaje)
        {
            var result = MessageBox.Show(mensaje, "Atencion!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }

}
