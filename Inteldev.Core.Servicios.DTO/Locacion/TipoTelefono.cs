using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Locacion
{
    public enum TipoTelefono : int
    {
        [EnumMember]
        Fijo = 0,
        [EnumMember]
        Celular = 1
    }
}
