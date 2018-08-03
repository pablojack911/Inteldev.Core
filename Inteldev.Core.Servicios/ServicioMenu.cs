using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Negocios.Menu;
using Inteldev.Core.Negocios;
using System.Collections.ObjectModel;
using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.DTO.Organizacion;


namespace Inteldev.Core.Servicios
{
    public class ServicioMenu : Inteldev.Core.Contratos.IServicioMenu
    {
		public List<Inteldev.Core.DTO.Menu.OpcionMenu> ObtenerMenu(Inteldev.Core.DTO.Usuarios.Usuario usuario, Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio? UnidadActual)
        {
            List<OpcionMenu> list;
            var menuHelper = (IMenuHelper)FabricaNegocios.Instancia.Resolver(typeof(IMenuHelper));
            IGestorMenu menu = menuHelper.ResuelveMenu(UnidadActual);
            list = menu.Obtener();
            if (usuario.Nombre != "ADMIN")
            {
                 var opciones = list.FirstOrDefault().Opciones;
                 var aux = new List<OpcionMenu>(opciones);
                 menuHelper.CargaPermuisos(usuario.PerfilUsuario.Permiso);
                 menuHelper.EliminaRecursivo(opciones, aux);
                 list.FirstOrDefault().Opciones = aux;
            }
            return list;
        }

		public List<Inteldev.Core.DTO.Menu.OpcionMenu> ObtenerMenuTodo(UnidadeDeNegocio? UnidadActual )
		{
			var menuHelper = (IMenuHelper)FabricaNegocios.Instancia.Resolver(typeof(IMenuHelper));
			return menuHelper.ResuelveMenu(UnidadActual).Obtener();
		}

    }

}
