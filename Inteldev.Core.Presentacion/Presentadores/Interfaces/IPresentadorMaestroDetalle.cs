
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
	public interface IPresentadorMaestroDetalle<TMaestro, TDetalle>
	 where TDetalle : new()
	{
		bool BorrarItem( );
		System.Windows.Input.ICommand CmdBorrar { get; set; }
		System.Collections.ObjectModel.ObservableCollection<TDetalle> Detalle { get; set; }
		IList<TDetalle> DetalleDTO { get; set; }
		System.Collections.IEnumerable DTO { get; set; }
		void Inicializar( );
		TDetalle ItemSeleccionado { get; set; }
        TMaestro Maestro { get; set; }
	}
}
