using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Inteldev.Core.Contratos
{
    [ServiceContract]
    public interface IServicioObtenerCodigoDisponible
    {

        [OperationContract]
        string CodigoDisponible(string desde);
    }
}
