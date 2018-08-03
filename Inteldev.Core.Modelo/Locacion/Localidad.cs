using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Locacion
{
    [Table("Localidades")]
    public class Localidad : EntidadMaestro
    {
        public Provincia Provincia { get; set; }
        [ForeignKey("Provincia")]
        public int? ProvinciaId { get; set; }

    }
}
