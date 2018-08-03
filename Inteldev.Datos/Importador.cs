using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Datos.Dao;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace Inteldev.Datos
{
    public class Importador : Inteldev.Datos.IImportador
    {
        IDao dao;

        List<Tuple<ILectorDeTabla, TipoInsert>> lectores;

        public DateTime Version { get; set; }

        public Importador(IDao dao)
        {
            this.dao = dao;
            this.lectores = new List<Tuple<ILectorDeTabla, TipoInsert>>();
        }

        public void AgregarLectordeTabla(ILectorDeTabla lector, TipoInsert tipoInsert)
        {
            if (!this.lectores.Any(p => p.Item1.Tabla == lector.Tabla))
                this.lectores.Add(new Tuple<ILectorDeTabla, TipoInsert>(lector, tipoInsert));
        }

        public bool Ejecutar()
        {
            bool ok = true;
            foreach (var item in lectores)
            {
                var dt = item.Item1.Ejecutar();

                if (item.Item2 == TipoInsert.Masivo)
                    this.InsertMasivo(dt);
                else
                {
                    //if (this.TablaVacia(dt.TableName))
                    //    this.InsertMasivo(dt);
                    //else 
                    this.InsertDiferencias(dt);
                }
            }

            return ok;
        }

        private void InsertMasivo(DataTable dt)
        {

            SqlBulkCopy bulk = new SqlBulkCopy(dao.DbConnection as SqlConnection);

            bulk.DestinationTableName = dt.TableName;

            bulk.WriteToServer(dt);
            dao.Desconectar();
        }

        private void InsertDiferencias(DataTable dt)
        {
            var campos = dt.Columns.OfType<DataColumn>().Select(col => col.ColumnName);
            var selectCampos = string.Join(",", campos);
            var dataTableFinal = dt.Clone();
            var columnaRowVersion = dataTableFinal.Columns.Add("rowversion");
            foreach (DataRow item in dt.Rows)
            {
                var where = "";

                foreach (var campoClave in dt.PrimaryKey)
                {
                    if (where.Length > 0)
                        where = where + " and ";

                    where = where + string.Format("{0}={1}", campoClave, item[campoClave] is string ? ("'" + item[campoClave].ToString() + "'") : item[campoClave]);
                }

                if (where.Length == 0)
                    throw new Exception("Falta definir Clave Primaria");

                var selectCmd = string.Format("select {0} from {1} where {2}", selectCampos, dt.TableName, where);

                var dr = dao.EjecutarConsulta(selectCmd);

                var dtRec = new DataTable();
                dtRec.Load(dr);
                bool hayCambios = true;
                if (dtRec.Rows.Count > 0)
                {
                    hayCambios = !DataRowComparer<DataRow>.Default.Equals(item, dtRec.Rows[0]);
                }

                if (hayCambios)
                {
                    //var insert = new Dao.SqlInsertDbCommand(dt.TableName,this.dao.CrearDbCommand());

                    //foreach (var col in campos)
                    //{
                    //    insert.Campo(col, item[col]);
                    //}

                    //if (this.Version != null)
                    //    insert.Campo<DateTime>("rowversion", this.Version);

                    //dao.EjecutarComando(insert.ToDbCommand());

                    DataRow newrow = dataTableFinal.NewRow();
                    newrow.ItemArray = item.ItemArray;
                    newrow.SetField<DateTime>(columnaRowVersion, this.Version);

                    dataTableFinal.Rows.Add(newrow);
                }

            }
            this.InsertMasivo(dataTableFinal);
            dao.Desconectar();
        }

        //public bool TablaVacia(string tabla)
        //{
        //    var result = this.dao.EjecutarFuncion<object>("select count(*) from "+tabla);
        //    return Convert.ToBoolean(result);

        //}

    }

    public enum TipoInsert
    {
        Masivo,
        Diferencial
    }

}