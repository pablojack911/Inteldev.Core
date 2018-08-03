using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public class SqlSelect:ISqlSelect
    {
        public SqlSelect(string[] tablas)
        {
            this.tablas = tablas;
        }
        string[] tablas;

        public ISqlSelect Campo<ValueType>(string campo, ValueType value)
        {            
            return this;
        }

        public ISqlSelect Where<ValueType>(string campo, ValueType value)
        {
            return this;
        }

        public ISqlSelect GroupBy(string campo)
        {
            return this;
        }

        public ISqlSelect OrderBy(string campo)
        {
            return this;
        }
    }
}
