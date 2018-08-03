using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Usuarios
{
    public class Permiso : DTOMaestro
    {
		[DataMember]
        public NivelPermiso NivelPermiso { get; set; }
		[DataMember]
		public List<Permiso> SubModulos { get; set; }
    }
}
