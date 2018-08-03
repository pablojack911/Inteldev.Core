using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo.Organizacion
{
    public class DivisionComercial : EntidadMaestro
    {
        public DivisionComercial()
        {
            this.Empresas = new List<EmpresaCodigo>();
        }
        public ICollection<EmpresaCodigo> Empresas { get; set; }
    }
}
