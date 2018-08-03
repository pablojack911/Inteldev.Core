using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Inteldev.Core.Negocios.Busquedas
{

    public interface IParteBusqueda<TEntidad>
		where TEntidad : Inteldev.Core.Modelo.EntidadBase
    {
		MethodCallExpression ArmaConsulta(IQueryable queryableData);
        string Nombre { get; set; }
        Predicate<object> PuedeBuscar { get; set; }
		void AgregaParteIzquierdaBuscarPor(string propiedad, string subPropiedad, Type objetoDestino, Type objetoOrigen);
		void AgregaParteIzquierdaBuscarPor(string propiedad, Type objetoOrigen);
		void anidaPropiedad(Type objetoDestino, string subPropiedad);
		void CondicionWhereEqual( );
		void CondicionWhereContains( );
    }
}
