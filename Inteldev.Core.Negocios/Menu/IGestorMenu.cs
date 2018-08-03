using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Inteldev.Core.Negocios.Menu
{
	/// <summary>
	/// Interfaz que debe implementar el gestor de menu
	/// </summary>
    public interface IGestorMenu
    {
		/// <summary>
		/// Crea el menu con sus nombres y submenues.
		/// </summary>
        void Crear();

		/// <summary>
		/// Obtiene la lista con los menues disponibles
		/// </summary>
		/// <returns>lista con los elementos del menu</returns>
		System.Collections.Generic.List<Inteldev.Core.DTO.Menu.OpcionMenu> Obtener();

    }

}
