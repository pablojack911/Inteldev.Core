using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inteldev.Core.Modelo;

namespace Inteldev.Core.Negocios
{
    public interface IGrabadorFox<TEntidad> where TEntidad : EntidadBase
    {
        string Usuario { get; set; }
        bool Grabar(TEntidad entidad);
        bool Borrar(TEntidad entidad);
    }
}
