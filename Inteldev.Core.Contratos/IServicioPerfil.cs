using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Contratos
{
	[ServiceContract]
	public interface IServicioPerfilUsuario : IServicioABM<PerfilUsuario>
	{
		[OperationContract]
		PerfilUsuario CargarPermisos(List<OpcionMenu> Menues);
	}
}
