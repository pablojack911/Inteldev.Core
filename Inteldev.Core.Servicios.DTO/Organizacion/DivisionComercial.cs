using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Organizacion
{
    [Validaciones.ValidadorAtributo(typeof(Validaciones.ValidadorDivisionComercial))]
    public class DivisionComercial : DTOMaestro
    {
        [DataMember]
        public List<Empresa> Empresas { get; set; }

        public override string ToString()
        {
            return this.Codigo;
        }
    }
}
