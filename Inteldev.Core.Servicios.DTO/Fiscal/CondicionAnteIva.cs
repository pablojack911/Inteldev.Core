using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.DTO.Fiscal
{
    public enum CondicionAnteIva : int
    {
        ResponsableInscripto = 0,
        Monotributo = 1,
        Exento = 2,
        ConsumidorFinal = 3
    }
}
