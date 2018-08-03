using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Controles;
using System;
namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
	public interface IPresentadorContacto<tMaestro> : IPresentadorGrilla<tMaestro,Contacto,ItemContacto>
	{
		bool Aceptar( );
		void CrearVentana( );
		bool Editar( );
		void Inicializar( );
	}
}
