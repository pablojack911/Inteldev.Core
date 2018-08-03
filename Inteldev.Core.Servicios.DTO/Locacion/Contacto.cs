using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Inteldev.Core.DTO.Locacion
{
    public class Contacto : DTOBase
    {

        [DataMember]
        public System.Collections.Generic.List<Telefono> Telefonos { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Web { get; set; }

        [DataMember]
        public Domicilio Domicilio { get; set; }
        
        public Contacto():base()
        {
			this.Telefonos = new List<Telefono>();
            Domicilio = new Domicilio();
        }
    }
}
