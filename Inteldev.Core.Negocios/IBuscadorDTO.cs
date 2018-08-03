using Inteldev.Core.Datos;
using Inteldev.Core.DTO;
using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Inteldev.Core.Negocios
{
    public interface IBuscadorDTO<TEntidad,TDto>
    {
		List<TDto> BuscarLista(object param, CargarRelaciones cargarEntidades);
        TDto BuscarSimple(object busqueda,CargarRelaciones cargarEntidades);
		TDto BuscarPorCodigo<TMaestro>(object busqueda, CargarRelaciones cargarEntidades, List<Inteldev.Core.DTO.ParametrosMiniBusca> parametros) where TMaestro : EntidadMaestro;
    }
}
