using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Tesoreria
{
    public class Valor : DTOBase
    {
        [DataMember]
        public Valores TipoValor { get; set; }
        [DataMember]
        public decimal Importe { get; set; }
    }
}
