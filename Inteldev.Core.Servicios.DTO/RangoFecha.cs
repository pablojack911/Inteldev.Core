using Inteldev.Core.DTO.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    [Validator(typeof(ValidadorFecha))]
    //tuve que hacer que derive de dtomaestro para poder aplicarle las validaciones.
    public class RangoFecha : DTOMaestro
    {
        [DataMember]
        public DateTime FechaDesde { get; set; }
        [DataMember]
        public DateTime FechaHasta { get; set; }
    }
}
