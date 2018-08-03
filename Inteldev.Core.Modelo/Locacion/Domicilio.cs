using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Locacion
{
    public class Domicilio : EntidadBase
    {
        public Calle Calle { get; set; }
        [ForeignKey("Calle")]
        public int? CalleId { get; set; }
        public int Numero { get; set; }
        public int Piso { get; set; }
        public string Departamento { get; set; }
        public Coordenada Coordenada { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Nº {1} {2} {3}", Calle == null ? string.Empty : Calle.ToString(),
                                                       Numero,
                                                       Piso == 0 ? string.Empty : "Piso:" + Piso.ToString(),
                                                       string.IsNullOrEmpty(Departamento) ? string.Empty : "Dpto:" + Departamento);
        }
    }
}
