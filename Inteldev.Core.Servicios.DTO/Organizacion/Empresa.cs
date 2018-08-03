using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.DTO.Fiscal;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Inteldev.Core.DTO.Organizacion
{
    [Validaciones.ValidadorAtributo(typeof(Validaciones.ValidadorEmpresa))]
    public class Empresa : DTOMaestro
    {
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public CondicionAnteIva CondicionAnteIVA { get; set; }
        [DataMember]
        public string CUIT { get; set; }
        [DataMember]
        public string IIBB { get; set; }
        [DataMember]
        public DateTime FechaDesde { get; set; }
        [DataMember]
        public DateTime FechaHasta { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
