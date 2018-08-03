using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlUpdateDbCommand
    {
        ISqlUpdateDbCommand Where<ValueType>(string campo, ValueType value);
        ISqlUpdateDbCommand Campo<ValueType>(string campo, ValueType value);
        IDbCommand ToDbCommand();
    }
}
