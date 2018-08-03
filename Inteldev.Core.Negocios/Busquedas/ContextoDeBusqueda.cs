using Inteldev.Core.Datos;
using Inteldev.Core.DTO;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios.Mapeador;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Busquedas
{
    public class ContextoDeBusqueda<TEntidad, Tdto> : Inteldev.Core.Negocios.Busquedas.IContextoDeBusqueda<TEntidad, Tdto>
        where TEntidad : EntidadBase
        where Tdto : DTOBase
    {

        IBlockDeBusqueda<TEntidad> Block;
        IBuscador<TEntidad> Buscador;
        IMapeadorGenerico<TEntidad, Tdto> Mapeador;

        public ContextoDeBusqueda(IBuscador<TEntidad> buscador, IBlockDeBusqueda<TEntidad> block, IMapeadorGenerico<TEntidad, Tdto> mapeador)
        {
            this.Block = block;
            this.Buscador = buscador;
            this.Mapeador = mapeador;
        }

        public List<ResultadoBusqueda<Tdto>> Buscar(string busqueda, ListaParametrosDeBusqueda parametros = null)
        {
            List<ResultadoBusqueda<Tdto>> resultado = new List<ResultadoBusqueda<Tdto>>();
            Block.Busqueda = busqueda;
            List<object> listaPropiedades = new List<object>();
            var dto = Activator.CreateInstance(typeof(Tdto));
            var propiedades = dto.GetType().GetProperties();
            foreach (var prop in propiedades)
            {
                if (prop.GetCustomAttributes(true).OfType<IncluirEnBuscadorAttribute>().Any())
                {
                    listaPropiedades.Add(prop);
                }
            }
            var mapeador = FabricaNegocios._Resolver<IMapeadorGenerico<Modelo.ParametrosMiniBusca, DTO.ParametrosMiniBusca>>();
            Block.AgregarPartes(listaPropiedades, mapeador.ToListEntidad(parametros.Parametros));
            foreach (var parte in Block.ObtenerPartes())
            {
                //var consulta = parte.ArmaConsulta(Buscador.ConsultaSimple(CargarRelaciones.CargarEntidades));
                //var lista = Buscador.BuscarLista(consulta, CargarRelaciones.CargarEntidades);
                var consulta = parte.ArmaConsulta(Buscador.ConsultaSimple(CargarRelaciones.NoCargarNada));
                var lista = Buscador.BuscarLista(consulta, CargarRelaciones.NoCargarNada);
                if (lista != null)
                {
                    var parteResultado = new ResultadoBusqueda<Tdto>();
                    parteResultado.Nombre = parte.Nombre;
                    parteResultado.Lista = Mapeador.ToListDto(lista.OrderBy(x => x.GetType().GetProperty(parte.Nombre)).ToList());
                    if (parteResultado.CantidadDeItems != 0)
                        resultado.Add(parteResultado);
                }
            }
            if (resultado.Count == 0)
            {
                var parteResultado = new ResultadoBusqueda<Tdto>();
                parteResultado.Nombre = "Sin resultados";
                parteResultado.Lista = Mapeador.ToListDto(new List<TEntidad>());
                resultado.Add(parteResultado);
            }
            return resultado;
        }
    }
}
