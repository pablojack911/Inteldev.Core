using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Datos
{
    public interface IDao
    {
        IDbConnection Connection { get; set; }
        void CerrarTransaccion();
        void ComenzarTransaccion();
        void Conectar();
        IDbCommand CrearDbCommand();
        void DescartarTransaccion();
        void Desconectar();
        void EjecutarComando(IDbCommand comando);
        void EjecutarComando(string comando);
        IDataReader EjecutarConsulta(string query);
        IDataReader EjecutarSP(string storeProcedure, object[] pParameters);
    }
}
