using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Contratos
{
    public interface IServicioConsulta<TEntidad> where TEntidad : DTOMaestro
    {
        Respuesta<TEntidad> Consultar(Parametros<TEntidad> parametros);
    }
}
