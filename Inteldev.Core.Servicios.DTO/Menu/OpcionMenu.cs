using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Inteldev.Core.DTO.Menu
{
    public class OpcionMenu:DTOBase
    {
        [DataMember]
        public string Modulo { get; set; }
        [DataMember]
        public string Icono { get; set; }
        [DataMember]
        public string Atajo { get; set; }
        [DataMember]
        public object Comando { get; set; }
        [DataMember]
        public List<OpcionMenu> Opciones { get; set; }
        
    }
}
