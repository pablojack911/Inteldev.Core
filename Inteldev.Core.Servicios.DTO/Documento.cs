using Inteldev.Core.DTO.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    public class Documento : DTOMaestro
    {
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public Sucursal Sucursal { get; set; }
        //[DataMember]
        //public int? SucursalId { get; set; }
        [DataMember]
        public Empresa Empresa { get; set; }
        //[DataMember]
        //public int? EmpresaId { get; set; }
        [DataMember]
        public string Letra { get; set; }
        [DataMember]
        public string Numero { get; set; }
        [DataMember]
        public string Prenumero { get; set; }
    }
}
