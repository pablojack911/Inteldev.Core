using Inteldev.Core.Datos;
using Inteldev.Core.DTO;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios.Mapeador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios
{
    public class BuscadorDTO<TEntidad, TDto> : Inteldev.Core.Negocios.IBuscadorDTO<TEntidad, TDto>
        where TEntidad : EntidadBase
    {
        protected IBuscador<TEntidad> BuscadorEntidad;
        protected IMapeadorGenerico<TEntidad, TDto> Mapeador;

        public BuscadorDTO(IBuscador<TEntidad> buscadorEntidad, IMapeadorGenerico<TEntidad, TDto> mapeador)
        {
            this.BuscadorEntidad = buscadorEntidad;

            this.Mapeador = mapeador;
        }

        public virtual List<TDto> BuscarLista(object param, CargarRelaciones cargarEntidades)
        {
            return this.Mapeador.ToListDto(this.BuscadorEntidad.BuscarLista(e => e.Id > 0, cargarEntidades)).ToList();
        }

        public virtual TDto BuscarSimple(object busqueda, CargarRelaciones cargarEntidades)
        {
            this.BuscadorEntidad.CargarEntidadesRelacionadas = cargarEntidades;
            var res = this.BuscadorEntidad.BuscarSimple(busqueda);
            return Mapeador.EntidadToDto(res);
        }

        public virtual TDto BuscarPorCodigo<TMaestro>(object busqueda, CargarRelaciones cargarEntidades, List<Inteldev.Core.DTO.ParametrosMiniBusca> parametros = null)
            where TMaestro : EntidadMaestro
        {
            this.BuscadorEntidad.CargarEntidadesRelacionadas = cargarEntidades;
            //tengo que modificar el buscador entidad tambien.
            var mapeadorParametros = FabricaNegocios._Resolver<IMapeadorGenerico<Inteldev.Core.Modelo.ParametrosMiniBusca, Core.DTO.ParametrosMiniBusca>>();
            var result = this.BuscadorEntidad.BuscarPorCodigo<TMaestro>(busqueda, mapeadorParametros.ToListEntidad(parametros)) as TEntidad;
            return Mapeador.EntidadToDto(result);
        }
    }
}
