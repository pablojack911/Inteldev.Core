using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Auditoria
{
    public class Observacion : DTOBase
    {
        [DataMember]
        public DateTime? FechaHora { get; set; }
        [DataMember]
        public Usuario Usuario { get; set; }
        [DataMember]
        public int? UsuarioId { get; set; }

        public Observacion()
        {
            this.FechaHora = DateTime.Now;
        }
    }
}
