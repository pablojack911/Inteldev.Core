using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Inteldev.Datos
{
    public abstract class LectorDeTabla : Inteldev.Datos.ILectorDeTabla
    {
        public string Tabla { get; set; }
        public LectorDeTabla(string tabla)
        {
            this.Tabla = tabla;
        }

        public DataTable Ejecutar()
        {
            var dt = new DataTable(this.Tabla);
            this.CargarDataTable(dt);
            return dt;
        }

        protected abstract void CargarDataTable(DataTable dt);

    }
}
