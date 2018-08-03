using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Datos;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Organizacion;
using Microsoft.Practices.Unity;
using Inteldev.Core.DataSwitch;

namespace Inteldev.Core.Negocios
{
	/// <summary>
	/// Setea el contexto.
	/// </summary>
    public class LogicaDeNegociosBase<TEntidad>
        where TEntidad : EntidadBase
    {
        protected IDataSwitch<TEntidad> Contexto;
		/// <summary>
		/// Setea el contexto general
		/// </summary>
		/// <param name="contexto">Contexto general</param>
        public LogicaDeNegociosBase(string empresa, string entidad)
        {
            ParameterOverride[] parameters = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", entidad) };
            this.Contexto = (IDataSwitch<TEntidad>)FabricaNegocios.Instancia.Resolver(typeof(IDataSwitch<TEntidad>), parameters);
        }

        public LogicaDeNegociosBase()
        {
        }

    }
}
