using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Servicios
{
	public class ServicioPerfilUsuario : ServicioABM<Inteldev.Core.DTO.Usuarios.PerfilUsuario,Inteldev.Core.Modelo.Usuarios.PerfilUsuario>, IServicioPerfilUsuario
	{
		public PerfilUsuario CargarPermisos(List<OpcionMenu> Menues)
		{
			var menu = Menues.FirstOrDefault();
			var permiso = new Permiso();
			permiso.Id = menu.Id;
			permiso.Nombre = menu.Nombre;
			var perfil = new PerfilUsuario();
			LlenaRecursivo(menu,permiso);
			perfil.Permiso = permiso;
			return perfil;
		}

		private void LlenaRecursivo(OpcionMenu menu, Permiso permiso)
		{
			if (menu != null)
			{
				foreach (var item in menu.Opciones)
				{
					var permi = new Permiso();
					permi.Id = item.Id;
					permi.Nombre = item.Nombre;
					permiso.SubModulos.Add(permi);
					LlenaRecursivo(item,permi);
				}
			}
		}

	}
}
