using System;

namespace Inteldev.Core.Negocios.Menu
{
	public interface IMenuHelper
	{
		void CargaPermuisos(Inteldev.Core.DTO.Usuarios.Permiso permiso);
		void EliminaRecursivo(System.Collections.Generic.ICollection<Inteldev.Core.DTO.Menu.OpcionMenu> opciones, System.Collections.Generic.List<Inteldev.Core.DTO.Menu.OpcionMenu> aux);
		IGestorMenu ResuelveMenu(Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio? UnidadActual);
	}
}
