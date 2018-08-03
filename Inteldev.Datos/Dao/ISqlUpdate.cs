using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlUpdate
    {
        ISqlUpdate Campo<ValueType>(string campo, ValueType value);
        ISqlUpdate Where<ValueType>(string campo, ValueType value);
    }
}
