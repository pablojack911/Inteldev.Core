using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Core.Presentacion
{
    /// <summary>
    /// Clase que contiene distintos mensajes para el usuario.
    /// </summary>
    public static class Mensajes
    {
        /// <summary>
        /// Clasico cartelito de error de Windows.
        /// </summary>
        /// <param name="mensaje">Mensaje que tiene que mostrar el cartel</param>
        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Clasico cartelito de error de windows.
        /// </summary>
        /// <param name="excepcion">Exepcion que contiene el mensaje de error.</param>
        public static void Error(Exception excepcion)
        {
            MessageBox.Show(excepcion.Message, "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Cartelito de aviso nada mas. 
        /// </summary>
        /// <param name="mensaje">mesaje a mostar</param>
        public static void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Mensaje para validar accion
        /// </summary>
        /// <param name="mensaje">Mensaje</param>
        /// <returns>SI O NO</returns>
        public static MessageBoxResult Confirmacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Atención", MessageBoxButton.YesNo, MessageBoxImage.Hand);
        }
    }

}
