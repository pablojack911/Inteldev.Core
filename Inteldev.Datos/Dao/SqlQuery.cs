using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Inteldev.Datos.Dao
{
    public abstract class SqlQuery
    {
        public delegate string Funcion(dynamic s);        

        public string ListaToString(IList lista,string separador, Funcion func )
        {            

            string resultado = "";
            foreach (string item in lista)
            {
                if (resultado.Length > 0)
                {
                    resultado = resultado + separador;
                }
                 
                resultado = resultado + func(item);

            }
            return resultado;
        }

        public IList UnirListas(IList lista1, IList lista2, string separador)
        {
            var list = new List<string>();
            for (int i = 0; i < lista1.Count; i++)
            {
                var par = lista1[i] + separador + this.ValueToString(lista2[i]);
                list.Add(par);
            }
            return list;
        }

        public string ValueToString(dynamic value)
        {
            if (value is string)
            {
                if (((string)value).EndsWith(")"))
                    return value;
                else
                    return "'" + value + "'";
            }
            else
            {
                string val = value.ToString();
                return val.Replace(',','.') ;
            }

        }

        public string KeyValuePairToString(KeyValuePair<string ,dynamic> kvp, string separador)
        {
            return kvp.Key + separador + this.ValueToString(kvp.Value);
        }
    }
}
