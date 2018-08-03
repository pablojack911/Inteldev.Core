using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Presentacion.ClienteServicios;

namespace Inteldev.Core.Presentacion
{
	/// <summary>
	/// Clase que implementa Fabrica de iversion de control.
	/// </summary>
    public class FabricaIoC : Inteldev.Core.Patrones.FabricaIoC<FabricaIoC>
    {

        public object ResolverGenerico(Type objeto, params Type[] parametros)
        {
            var objetogenerico = objeto.MakeGenericType(parametros);            
            return Activator.CreateInstance(objetogenerico);

                //Type typeList = typeof(List<>);
                //Type actualType = typeList.MakeGenericType(tipo.GetGenericArguments());
                //return Activator.CreateInstance(actualType);
            
        }

    }

}