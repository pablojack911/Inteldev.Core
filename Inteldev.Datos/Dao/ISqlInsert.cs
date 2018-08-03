using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlInsert
    {
        ISqlInsert Campo<ValueType>(string campo, ValueType value);
    }
}
