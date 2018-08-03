using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
namespace Inteldev.Core.Negocios.Busquedas
{
    public interface IContextoDeBusqueda<TEntidad,Tdto>
     where Tdto : Inteldev.Core.DTO.DTOBase
    {
        System.Collections.Generic.List<Inteldev.Core.DTO.ResultadoBusqueda<Tdto>> Buscar(string busqueda, ListaParametrosDeBusqueda parametros = null);
    }
}
