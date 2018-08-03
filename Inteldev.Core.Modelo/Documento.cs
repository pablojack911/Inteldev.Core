using Inteldev.Core.Modelo.Organizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo
{
    public class Documento : EntidadMaestro
    {
        public Documento()
        {
            this.Fecha = DateTime.Now;
        }
        public DateTime Fecha { get; set; }
        //public Sucursal Sucursal { get; set; }
        //[ForeignKey("Sucursal")]
        //public int? SucursalId { get; set; }
        //public Empresa Empresa { get; set; }
        //[ForeignKey("Empresa")]
        //public int? EmpresaId { get; set; }
        public string Empresa { get; set; }
        public string Sucursal { get; set; }
        public string Letra { get; set; }
        public string Numero { get; set; }
        public string Prenumero { get; set; }
    }
}
