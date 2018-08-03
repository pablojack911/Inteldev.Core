using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Contabilidad
{
    public enum Imputaciones : int
    {
        [EnumMember]
        ProveedoresVarios = 0,
        [EnumMember]
        DeudoresPorVentas = 1,
        [EnumMember]
        IVACF = 2,
        [EnumMember]
        IVADF = 3,
        [EnumMember]
        ImpInternos = 4,
        [EnumMember]
        PercepcionIIBB = 5,
        [EnumMember]
        PercepcionIva = 6
    }
}
