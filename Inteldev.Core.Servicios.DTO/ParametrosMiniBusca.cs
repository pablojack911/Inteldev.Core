using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    [DataContract]
    public class ParametrosMiniBusca
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string TipoObjeto { get; set; }
        [DataMember]
        public object Valor { get; set; }
    }
}
