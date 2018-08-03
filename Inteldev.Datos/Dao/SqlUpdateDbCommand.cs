using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public class SqlUpdateDbCommand : ISqlUpdateDbCommand
    {
        string Tabla;
        List<string> Campos;
        List<string> WhereList;
        IDbCommand Comando;
        public SqlUpdateDbCommand(string tabla, IDbCommand comando)
        {
            Tabla = tabla;
            this.Comando = comando;
            this.Campos = new List<string>();
            this.WhereList = new List<string>();
        }

        IDbDataParameter CrearParametro<ValueType>(string campo, ValueType value)
        {
            IDbDataParameter dp = this.Comando.CreateParameter();
            dp.ParameterName = "@" + "p" + this.Comando.Parameters.Count.ToString();
            dp.Value = value;
            dp.DbType = SqlBuildQuery.TypeToDbType(value.GetType());
            Comando.Parameters.Add(dp);
            return dp;
        }

        public ISqlUpdateDbCommand Campo<ValueType>(string campo, ValueType value)
        {
            var dp = this.CrearParametro<ValueType>(campo, value);
            this.Campos.Add(campo + "=?");

            return this;
        }

        public ISqlUpdateDbCommand Where<ValueType>(string campo, ValueType value)
        {
            var dp = this.CrearParametro(campo, value);

            this.WhereList.Add(campo + "=?");

            return this;
        }

        public IDbCommand ToDbCommand()
        {

            string sets = string.Empty;
            foreach (var item in this.Campos)
            {
                if (sets != string.Empty)
                    sets = sets + ",";

                sets = sets + item;

            }

            string wheres = string.Empty;
            foreach (var item in this.WhereList)
            {
                if (wheres != string.Empty)
                    wheres = wheres + " and ";

                wheres = wheres + item;

            }
            this.Comando.CommandText = "update " + this.Tabla + " set " + sets;

            if (wheres != string.Empty)
                this.Comando.CommandText += " where " + wheres;

            return this.Comando;

        }





    }



}

