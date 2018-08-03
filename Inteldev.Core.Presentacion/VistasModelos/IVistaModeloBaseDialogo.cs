using System;
using System.Collections.Generic;
using System.Windows;
namespace Inteldev.Core.Presentacion.VistasModelos
{
    public interface IVistaModeloBaseDialogo
	{
        bool SeleccionOk { get;}
        int ObtenerID();
		int[] ObtenerIds( );
        Window Ventana { get; set; }
    }
}
