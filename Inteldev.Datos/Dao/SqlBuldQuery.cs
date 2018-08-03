using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Inteldev.Datos.Dao
{
    public class SqlBuildQuery
    {
        public static ISqlInsertDbCommand InsertInto(string tabla, IDbCommand comando)
        {            
            return new SqlInsertDbCommand(tabla,comando);
        }
        public static ISqlInsert InsertInto(string tabla)
        {
            return new sqlInsertQuery(tabla);
        }

        public static ISqlUpdateDbCommand Update(string tabla, IDbCommand comando)
        {
            return new SqlUpdateDbCommand(tabla, comando);
        }

        public static ISqlUpdate Update(string tabla)
        {
            return new SqlUpdate(tabla);
        }       

        public static DbType TypeToDbType(Type T)
        {
            
            switch (T.FullName)
            {

                case "System.Int64":

                    return DbType.Int64;

                    break;

                case "System.Int32":

                    return DbType.Int32;

                    break;

                case "System.Int16":

                    return DbType.Int16;

                    break;

                case "System.Decimal":

                    return DbType.Decimal;

                    break;

                case "System.Double":

                    return DbType.Double;

                    break;

                case "System.Boolean":

                    return DbType.Boolean;

                    break;

                case "System.String":

                    return DbType.String;

                    break;

                case "System.DateTime":

                    return DbType.DateTime;

                    break;

                default:

                    return DbType.Object;

                    break;

            }

        }
    }
   
  
 
    

}
