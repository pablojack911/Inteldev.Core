using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Menu
{
	/// <summary>
	/// Clase para ayudar con el asunto de los Permisos y el Menu. Mapea mas que nada.
	/// </summary>
	public class MenuHelper : Inteldev.Core.Negocios.Menu.IMenuHelper
	{
		private List<Permiso> permisos;

		public MenuHelper( )
		{
			this.permisos = new List<Permiso>();
		}

		public void CargaPermuisos(Permiso permiso)
		{
			if (permiso != null)
			{
				foreach (var item in permiso.SubModulos)
				{
					permisos.Add(item);
					this.CargaPermuisos(item);
				}
			}
		}

		/// <summary>
		/// Quita de la lista de menues aquellos menues que tengan el permiso en denegado
		/// </summary>
		/// <param name="opciones">lista original de menues</param>
		/// <param name="aux">lista con los menues quitados</param>
		public void EliminaRecursivo(ICollection<OpcionMenu> opciones, List<OpcionMenu> aux)
		{
			if (opciones != null && opciones.Count != 0)
			{
				foreach (var item in opciones)
				{
                    var ahh = permisos.FirstOrDefault(p => p.Nombre == item.Nombre);
                    
                        if (permisos.FirstOrDefault(p => p.Nombre == item.Nombre).NivelPermiso == DTO.Usuarios.NivelPermiso.Denegado)
                        {
                            aux.Remove(item);
                        }
                        else
                        {
                            var elem = aux.Find(p => p.Nombre == item.Nombre);
                            var aux2 = new List<OpcionMenu>(elem.Opciones);
                            EliminaRecursivo(item.Opciones, aux2);
                            elem.Opciones = aux2;
                        }
                    
				}
			}
		}

		public IGestorMenu ResuelveMenu(UnidadeDeNegocio? UnidadActual)
		{
			IGestorMenu menu;
			switch (UnidadActual)
			{
				case Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio.Preventa:
					menu = (IMenuPreventa)FabricaNegocios._Resolver(typeof(IMenuPreventa));
					break;
				case Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio.Mayorista:
					menu = (IMenuMayorista)FabricaNegocios._Resolver(typeof(IMenuMayorista));
					break;
				case Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio.Gestion:
					menu = (IMenuGestion)FabricaNegocios._Resolver(typeof(IMenuGestion));
					break;
				case Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio.Representaciones:
					menu = (IMenuRepresentaciones)FabricaNegocios._Resolver(typeof(IMenuRepresentaciones));
					break;
				case Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio.Logistica:
					menu = (IMenuLogistica)FabricaNegocios._Resolver(typeof(IMenuLogistica));
					break;
				default:
					menu = FabricaNegocios._Resolver<IGestorMenu>();
					break;
			}
			return menu;
		}
	}
}
