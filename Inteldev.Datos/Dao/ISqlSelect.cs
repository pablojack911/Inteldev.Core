using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlSelect
    {
        ISqlSelect Campo<ValueType>(string campo, ValueType value);
        ISqlSelect Where<ValueType>(string campo, ValueType value);
        ISqlSelect GroupBy(string campo);
        ISqlSelect OrderBy(string campo);
    }
}
