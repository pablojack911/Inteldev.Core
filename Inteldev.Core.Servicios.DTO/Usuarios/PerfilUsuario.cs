using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Inteldev.Core.DTO.Usuarios
{
    public class PerfilUsuario : DTOMaestro
    {
        [DataMember]
        public Permiso Permiso { get; set; }
    }
}
