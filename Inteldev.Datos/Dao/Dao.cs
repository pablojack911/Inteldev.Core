using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Inteldev.Datos.Dao
{

    public class Dao : Inteldev.Datos.Dao.IDao
    {
        IDbConnection Conexion;
        IDataReader Datos;
        IDbTransaction Transaccion;

        public string ConnectionString {
            get
            {
                return Conexion.ConnectionString;
            }
            }

        public Dao(IDbConnection pConeccion)
        {
            this.Conexion = pConeccion;
        }
               

        public virtual void Conectar()
        {
            if (this.Conexion.State == ConnectionState.Closed)
                this.Conexion.Open();
        }

        public void Desconectar()
        {
            this.Conexion.Close();
        }

        public IDbCommand CrearDbCommand()
        {
            this.Conectar();
            IDbCommand oComando = this.Conexion.CreateCommand();
            oComando.CommandType = CommandType.Text;
            
            if (this.Transaccion!=null)
                oComando.Transaction = this.Transaccion;

            return oComando;
        }

        public IDataReader EjecutarConsulta(string query)
        {
            this.Conectar();

            IDbCommand oComando = CrearDbCommand();
            oComando.CommandText = query;
            this.Datos = oComando.ExecuteReader();
            return this.Datos;
        }
                
        public void EjecutarComando(string comando)
        {
            this.Conectar();
            IDbCommand oComando = CrearDbCommand();
            oComando.CommandText = comando;
            oComando.ExecuteNonQuery();
        }

        public void EjecutarComando(IDbCommand comando)
        {
            this.Conectar();            
            comando.ExecuteNonQuery();
        }
        
        public IDataReader EjecutarSP(string storeProcedure, object[] pParameters)
        {
            this.Conectar();
            IDbCommand oComando = CrearDbCommand();
            oComando.CommandText = storeProcedure;
            IDbDataParameter oParameter = oComando.CreateParameter();

            this.Datos = oComando.ExecuteReader();
            return this.Datos;
        }

        public void ComenzarTransaccion()
        {
            this.Transaccion = Conexion.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CerrarTransaccion()
        {
            this.Transaccion.Commit();            
        }

        public void DescartarTransaccion()
        {
            this.Transaccion.Rollback();
        }
        
        public IDataReader EjecutarConsulta(string query, params object[] pParameters)
        {
            throw new NotImplementedException();
        }

        public TResult EjecutarFuncion<TResult>(string cmd) where TResult : class
        {
            var func = this.CrearDbCommand();
            func.CommandText = cmd;
            func.CommandType = CommandType.Text;
            var result = func.ExecuteScalar() as TResult;
            return result ;
        }
        
        public object EjecutarFuncion(string cmd)
        {
            throw new NotImplementedException();
        }


        public IDbConnection DbConnection
        {
            get { return this.Conexion; }
        }
    }
}