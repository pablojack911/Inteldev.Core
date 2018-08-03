using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public class sqlInsertQuery : SqlQuery, ISqlInsert
    {
        string Tabla;
        List<string> Campos;
        ArrayList Values;
        public sqlInsertQuery(string tabla)
        {
            Tabla = tabla;
            this.Campos = new List<string>();
            this.Values = new ArrayList();
        }
        public ISqlInsert Campo<ValueType>(string campo, ValueType valor)
        {
            Campos.Add(campo);
            Values.Add(valor);

            return this;
        }
        public ISqlInsert Campo(string campo, dynamic valor)
        {   
            Campos.Add(campo);
            Values.Add(valor);

            return this;
        }
        public override string ToString()
        {
            string campos = "";
            string values = "";
            foreach (string item in this.Campos)
            {
                if (campos.Length > 0)
                {
                    campos = campos + ",";

                }
                campos = campos + item;
            }
            foreach (var item in this.Values)
            {
                if (values.Length > 0)
                {
                    values = values + ",";
                }
                values = values + this.ValueToString(item);
            }

            return "INSERT INTO " + Tabla + " (" + campos + ") VALUES (" + values + ")";
        }      
    }

    public struct Exprecion
    {
        string Contenido;
        public Exprecion(string contenido)
        {
            this.Contenido = contenido;
        }
        public override string ToString()
        {
            return Contenido;
        }
    }
}
