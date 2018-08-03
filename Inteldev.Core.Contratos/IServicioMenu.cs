using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Collections.ObjectModel;

namespace Inteldev.Core.Contratos
{
	/// <summary>
	/// Interface para el servicio de Menus de la capa presentaciones
	/// </summary>
    [ServiceContract]
    public interface IServicioMenu
    {
        [OperationContract]
        List<Inteldev.Core.DTO.Menu.OpcionMenu> ObtenerMenu(Inteldev.Core.DTO.Usuarios.Usuario usuario, Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio? UnidadActual);
		[OperationContract]
		List<Inteldev.Core.DTO.Menu.OpcionMenu> ObtenerMenuTodo(Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio? UnidadActual);

    }

}
