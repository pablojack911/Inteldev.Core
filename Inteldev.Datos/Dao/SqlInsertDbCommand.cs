using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public class SqlInsertDbCommand : ISqlInsertDbCommand
    {
        string Tabla;
        List<string> Campos;
        IDbCommand Comando;
        public SqlInsertDbCommand(string tabla, IDbCommand comando)
        {
            Tabla = tabla;
            this.Comando = comando;
            this.Campos = new List<string>();
        }

        public ISqlInsertDbCommand Campo<ValueType>(string campo, ValueType value)
        {
            this.Campos.Add(campo);

            IDbDataParameter dp = this.Comando.CreateParameter();
            dp.ParameterName = "@" + "p" + this.Comando.Parameters.Count.ToString();
            dp.Value = value;
            dp.DbType = SqlBuildQuery.TypeToDbType(value.GetType());
            Comando.Parameters.Add(dp);

            return this;
        }

        public IDbCommand ToDbCommand()
        {
            //this.Comando.CommandText 
            string campos = "";
            string parames = "";
            int index = 0;
            foreach (string item in this.Campos)
            {
                if (campos.Length > 0)
                {
                    campos = campos + ",";
                    parames = parames + ",";
                }
                campos = campos + item;
                parames = parames + "?";
                index++;
            }

            this.Comando.CommandText = "insert into " + this.Tabla + " ( " + campos + " ) values (" + parames + ")";

            return this.Comando;

        }

    }
}
