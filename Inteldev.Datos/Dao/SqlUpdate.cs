using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public class SqlUpdate:SqlQuery,ISqlUpdate
    {
        string Tabla;     
        
        ArrayList Values;
        ArrayList ValuesFields;
        ArrayList Conditions;
        ArrayList ConditionFields;
        public SqlUpdate(string tabla)
        {
            this.Tabla = tabla;
            this.Conditions = new ArrayList();
            this.ConditionFields = new ArrayList();
            this.Values = new ArrayList();
            this.ValuesFields = new ArrayList();
        }

        public ISqlUpdate Campo<ValueType>(string campo, ValueType value)
        {
            this.ValuesFields.Add(campo);
            this.Values.Add(value);
            
            return this;
        }

        public ISqlUpdate Where<ValueType>(string campo, ValueType value)
        {
            this.Conditions.Add(value);
            this.ConditionFields.Add(campo);
            return this;
        }
        
        public override string ToString()
        {
            Func<dynamic, string> convertion = s => s.ToString();
            //set
            var list = this.UnirListas(this.ValuesFields, this.Values, "=");            
            var sets = this.ListaToString(list, ",", new Funcion(convertion));
            //

            //where
            var list2 = this.UnirListas(this.ConditionFields, this.Conditions, "=");            
            var wheres = this.ListaToString(list2, " AND ", new Funcion(convertion));
            //
            string sql= "UPDATE "+this.Tabla + " SET "+sets ;

            if (wheres.Length > 0)
                sql = sql + " WHERE "+ wheres ;

            return sql;
        }
    }
}
