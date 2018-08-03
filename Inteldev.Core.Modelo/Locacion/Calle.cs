using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Locacion
{
    public class Calle : EntidadMaestro
    {
        public Localidad Localidad { get; set; }
        [ForeignKey("Localidad")]
        public int? LocalidadId { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }

    }
}
