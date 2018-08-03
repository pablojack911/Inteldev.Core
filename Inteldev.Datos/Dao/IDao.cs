using System;
using System.Data;
namespace Inteldev.Datos.Dao
{
    public interface IDao
    {
        void CerrarTransaccion();
        void ComenzarTransaccion();
        void Conectar();
        string ConnectionString { get; }
        IDbCommand CrearDbCommand();
        void DescartarTransaccion();
        void Desconectar();
        void EjecutarComando(System.Data.IDbCommand comando);
        void EjecutarComando(string comando);
        System.Data.IDataReader EjecutarConsulta(string query);
        System.Data.IDataReader EjecutarConsulta(string query, params object[] pParameters);
        System.Data.IDataReader EjecutarSP(string storeProcedure, params object[] pParameters);
        object EjecutarFuncion(string cmd);
        TResult EjecutarFuncion<TResult>(string cmd) where TResult : class;
        IDbConnection DbConnection { get; }
    }
}
