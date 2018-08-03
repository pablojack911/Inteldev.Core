using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inteldev.Core.Modelo.Auditoria
{

    public class Observacion : EntidadBase
    {
        public Observacion() //puede ser que por no tener la invocación al super, no cree la instancia de fecha?
            //: base()
        {
            this.FechaHora = DateTime.Now;
        }

        public DateTime? FechaHora { get; set; }

        public Usuario Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
    }
}
