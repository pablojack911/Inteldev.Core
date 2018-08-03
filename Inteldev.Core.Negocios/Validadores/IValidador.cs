using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Validadores
{
    public interface IValidador<TEntidad> where TEntidad : Inteldev.Core.Modelo.EntidadMaestro
    {
        bool ExisteError(TEntidad entidad, string empresa, out string mensajeError);
    }
}
