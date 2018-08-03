using System;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
	public interface IPresentadorBase<TEntidad>
	 where TEntidad : Inteldev.Core.DTO.DTOBase, new()
	{
		void Ejecutar( );
		TEntidad EntidadActual { get; set; }
		System.Collections.ObjectModel.ObservableCollection<TEntidad> Entidades { get; set; }
		System.Windows.Window Ventana { get; set; }
		System.Windows.FrameworkElement Vista { get; set; }
	}
}
