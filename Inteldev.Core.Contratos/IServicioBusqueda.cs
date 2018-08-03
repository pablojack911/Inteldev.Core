using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Inteldev.Core.Contratos
{
    /// <summary>
    /// Utilizado para las busquedas en el BuscadorInicial
    /// </summary>
    [ServiceContract]
    public interface IServicioBusqueda<TDtoReducido, TDto>
        where TDto : DTOMaestro
        where TDtoReducido : DTOMaestro
    {
        [OperationContract]
        List<ResultadoBusqueda<TDto>> ObtenerResultados(string busqueda, string empresa, ListaParametrosDeBusqueda parametros = null);

        [OperationContract]
        List<ResultadoBusqueda<TDtoReducido>> ObtenerResultadosReducidos(string busqueda, string empresa, ListaParametrosDeBusqueda parametros = null);
    }
}
