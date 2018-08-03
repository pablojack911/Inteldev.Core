using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Inteldev.Core.DTO.Usuarios;

namespace Inteldev.Core.Contratos
{
	/// <summary>
	/// Interface de servicio basico de operaciones sobre usuarios. Implementa IServicioABM
	/// </summary>
    [ServiceContract]
    public interface IServicioUsuario : IServicioABM<Usuario>
    {

        [OperationContract]
        Usuario Autenticar(string nombre, string clave);

    }

}
