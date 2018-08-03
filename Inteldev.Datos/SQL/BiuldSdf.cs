using System;
using System.Data;
using System.Configuration;
using System.Data.SqlServerCe;
/// <summary>
/// Summary description for BiuldSdf
/// </summary>
/// 
namespace Inteldev.Datos.Sql
{
    public class BuildSdf
    {
        public BuildSdf()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        static public void Crear(string connString)
        {
            SqlCeEngine MotorSqlCe = new SqlCeEngine(connString);

            MotorSqlCe.CreateDatabase();

        }

        static public SqlCeConnection CrearConeccion(string connString)
        {
            return new SqlCeConnection(connString);
        }

       
        static public BuildTabla CrearTabla(string pNombre, SqlCeConnection pConeccion)
        {
            return new BuildTabla(pNombre, pConeccion);
        }
    }
}