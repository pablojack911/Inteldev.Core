using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inteldev.Core.Modelo;
using Inteldev.Datos.Dao;
using Inteldev.Core.Datos;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;

namespace Inteldev.Core.Negocios
{
    public abstract class GrabadorFox<TEntidad> : IGrabadorFox<TEntidad> where TEntidad : EntidadBase
    {
        private Modo modo;
        protected string usuario;

        public Modo Modo
        {
            get { return modo; }
        }


        public GrabadorFox(Inteldev.Core.Datos.IDao dao)
        {
            this.Dao = dao;
            this.CamposValores = new Dictionary<string, object>();
            this.modo = Negocios.Modo.Nada;
        }

        public Inteldev.Core.Datos.IDao Dao { get; set; }

        public string Tabla { get; set; }

        public string ClavePrimaria { get; set; }

        public object ValorClavePrimaria { get; set; }

        public Dictionary<string, object> CamposValores { get; set; }

        private string cmd = "";

        public abstract void Configurar(TEntidad entidad);
        public abstract void ConfigurarCamposValores(TEntidad entidad);

        public void SetearValores(string propiedad, object valor, object valorSiEsNull)
        {
            object newvalor;
            if (valor == null)
                newvalor = valorSiEsNull;
            else
                newvalor = valor;

            this.CamposValores.Add(propiedad, newvalor);
        }

        public virtual bool Grabar(TEntidad entidad)
        {
            this.Configurar(entidad);
            this.ConfigurarCamposValores(entidad);
            Dao.EjecutarComando("SET NULL OFF");

            var oleDbCommand = Dao.Connection.CreateCommand() as OleDbCommand;

            oleDbCommand.CommandType = System.Data.CommandType.Text;

            if (this.ExisteCodigo())
            {
                oleDbCommand = this.ActualizarSqlBuildQuery(this.Tabla, CamposValores, oleDbCommand) as OleDbCommand;
            }
            else
            {
                oleDbCommand = this.InsertarSqlBuildQuery(this.Tabla, CamposValores, oleDbCommand) as OleDbCommand;
            }

            this.AntesDeGrabar(entidad);

            //oleDbCommand.CommandText = cmd;

            Dao.EjecutarComando(oleDbCommand);
            Dao.Desconectar();

            return true;
        }

        public virtual void AntesDeGrabar(TEntidad entidad)
        {
        }

        public bool Borrar(TEntidad entidad)
        {
            this.Configurar(entidad);
            bool Ok = false;
            try
            {
                Dao.EjecutarComando(string.Format("delete from {0} where {1} = '{2}'", this.Tabla, this.ClavePrimaria, this.ValorClavePrimaria));
                this.Dao.Desconectar();
                Ok = true;
            }
            catch (Exception exc)
            {
            }

            return Ok;
        }

        protected bool ExisteCodigo()
        {

            var resultado = Dao.EjecutarConsulta(string.Format("select {0} from {1} where {0} = '{2}'", this.ClavePrimaria, this.Tabla, this.ValorClavePrimaria));
            var ok = resultado.RecordsAffected > 0;

            if (ok)
                this.modo = Modo.Update;
            else
                this.modo = Modo.Insert;

            return ok;
        }

        protected IDbCommand InsertarSqlBuildQuery(string tabla, Dictionary<string, object> myDic, OleDbCommand oleDbCommand)
        {
            var oSqlBuildQuery = SqlBuildQuery.InsertInto(tabla, oleDbCommand);

            foreach (KeyValuePair<string, object> item in myDic)
            {
                oSqlBuildQuery.Campo<Object>(item.Key, item.Value);
            }
            return oSqlBuildQuery.ToDbCommand();
        }

        protected IDbCommand ActualizarSqlBuildQuery(string tabla, Dictionary<string, object> myDic, OleDbCommand oleDbCommand)
        {
            var oSqlBuildQuery = SqlBuildQuery.Update(tabla, oleDbCommand);

            foreach (KeyValuePair<string, object> item in myDic)
            {
                oSqlBuildQuery.Campo<Object>(item.Key, item.Value);
            }
            oSqlBuildQuery.Where<string>(this.ClavePrimaria, this.ValorClavePrimaria.ToString());

            return oSqlBuildQuery.ToDbCommand();
        }

        public int BoolToInt(bool valor)
        {
            return valor ? 1 : 0;
        }

        public string DateTimeToDateFox(DateTime date)
        {
            return string.Format("date({0},{1},{2})", date.Year, date.Month, date.Day);
        }

        public string DateTimeActual()
        {
            //var year = DateTime.Now.Year;
            //var month = DateTime.Now.Month;
            //var day = DateTime.Now.Day;
            //var hh = DateTime.Now.Hour;
            //var mm = DateTime.Now.Minute;
            //var ss = DateTime.Now.Second;
            //return string.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hh, mm, ss);
            return DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }
        public string Usuario
        {
            get
            {
                return this.usuario;
            }
            set
            {
                this.usuario = value;
            }
        }
    }

    public enum Modo
    {
        Insert,
        Update,
        Nada
    }
}
