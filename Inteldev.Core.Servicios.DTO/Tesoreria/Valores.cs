using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Tesoreria
{
    public enum Valores : int
    {
        [EnumMember]
        Efectivo = 0,
        [EnumMember]
        Cheque = 1
    }
}
