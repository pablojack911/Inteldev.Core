using Inteldev.Core.Modelo;
using System.Linq;
using System.Collections;
using System;
using System.Diagnostics;
using Inteldev.Core.Datos;

namespace Inteldev.Core.Negocios
{

    public class CreadorGenerico<TEntidad> : LogicaDeNegociosBase<TEntidad>, ICreador<TEntidad> where TEntidad : EntidadBase
    {

        TEntidad entidad;

        public CreadorGenerico(string empresa, string entidad)
            : base(empresa, entidad)
        {
        }

        public TEntidad Crear()
        {
            entidad = Contexto.Crear();
            CargarDatos(entidad);
            return entidad;
        }

        public TEntidad Crear(params object[] args)
        {
            entidad = Contexto.Crear();
            CargarDatos(entidad);
            return entidad;
        }

        protected virtual void CargarDatos(TEntidad Entidad)
        {
        }
    }
}
