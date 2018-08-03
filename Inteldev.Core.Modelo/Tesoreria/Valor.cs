using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo.Tesoreria
{
    public class Valor : EntidadBase
    {
        public Valores TipoValor { get; set; }
        public decimal Importe { get; set; }
    }
}
