using System;
using System.Data;
using System.Configuration;
using System.Web;


/// <summary>
/// Summary description for BuildTabla
/// </summary>
/// 
namespace Inteldev.Datos.Sql
{
    public class BuildTabla
    {
        string Nombre;
        string Campo;
        IDbConnection Coneccion;
        public BuildTabla(string pNombre, IDbConnection pConeccion)
        {
            Nombre = pNombre;
            Campo = "";
            Coneccion = pConeccion;
        }

        public BuildTabla addCampo(string pNombre, TiposDeDatos pTipo)
        {
            return addCampo(pNombre, pTipo, 0, 0);
        }

        public BuildTabla addCampo(string pNombre, TiposDeDatos pTipo, int pTamaño)
        {
            return addCampo(pNombre, pTipo, pTamaño, 0);
        }

        public BuildTabla addCampo(string pNombre, TiposDeDatos pTipo, int pTamaño, int pDecimales)
        {
            string campo = pNombre.Trim() + " " + pTipo.ToString();

            if (pTipo != TiposDeDatos.INT)    
            {
                campo = campo + " (" + pTamaño.ToString();

                if (pTipo == TiposDeDatos.DECIMAL)
                    campo = campo + "," + pDecimales;

                campo = campo + ")";

            }
            if (this.Campo == "")
                this.Campo = this.Nombre.Trim() + " (" + campo.Trim();
            else
                this.Campo = this.Campo + "," + campo;

            return this;
        }

        public override string ToString()
        {
            return "Create Table "+Campo + ")";
        }
        public void Crear()
        {
            IDbCommand comando = Coneccion.CreateCommand();
            comando.CommandText = this.ToString();
            if (comando.Connection.State == ConnectionState.Closed)
                comando.Connection.Open();

            comando.ExecuteNonQuery();
        }
        public string test()
        {
            return this.ToString();
        }
    }

    public enum TiposDeDatos
    {
        CHAR,
        VARCHAR,
        NVARCHAR,
        INT,
        DECIMAL
    }
}