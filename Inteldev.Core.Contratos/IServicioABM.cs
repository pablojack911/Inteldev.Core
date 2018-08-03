using System.Collections.Generic;
using System.ServiceModel;
using Inteldev.Core.DTO;
using System.Linq.Expressions;
using System;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.DTO.Carriers;

namespace Inteldev.Core.Contratos
{
    /// <summary>
    /// Interface de servicio basico ABM
    /// </summary>
    /// <typeparam name="TDto">Tipo de dato</typeparam>
    [ServiceContract]
    public interface IServicioABM<TDto> where TDto : DTOBase
    {

        [OperationContract]
        TDto ObtenerPorId(int id, CargarRelaciones cargarEntidades, string empresa);

        [OperationContract]
        TDto ObtenerPorCodigo(object codigo, CargarRelaciones cargarEntidades, string empresa, ListaParametrosDeBusqueda parametros = null);

        [OperationContract]
        IList<TDto> ObtenerLista(object param, CargarRelaciones cargaEntidades, string empresa);

        [OperationContract]
        List<ResultadoBusqueda<TDto>> ObtenerResultados(string busqueda, string empresa, ListaParametrosDeBusqueda parametros = null);

        [OperationContract]
        GrabadorCarrier Grabar(TDto Entidad, Usuario Usuario, string empresa);

        [OperationContract]
        ErrorCarrier Borrar(TDto Entidad, Usuario Usuario, string empresa);

        [OperationContract]
        CreadorCarrier<TDto> Crear(string empresa);

        [OperationContract]
        CreadorCarrier<TDto> CrearConParametros(string empresa, params int[] args);

        [OperationContract]
        bool EsValido(TDto Entidad);

    }

}
