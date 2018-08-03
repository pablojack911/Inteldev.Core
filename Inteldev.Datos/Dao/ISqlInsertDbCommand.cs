using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlInsertDbCommand
    {
        ISqlInsertDbCommand Campo<ValueType>(string campo, ValueType value);
        IDbCommand ToDbCommand();
    }
}
