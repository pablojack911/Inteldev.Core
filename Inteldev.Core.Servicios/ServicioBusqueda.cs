using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios;
using Inteldev.Core.Negocios.Busquedas;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServicioBusqueda<TEntidad, TDto, TDtoReducido> : IServicioBusqueda<TDtoReducido, TDto>
        where TEntidad : EntidadMaestro
        where TDto : DTOMaestro
        where TDtoReducido : DTOMaestro
    {
        public List<DTO.ResultadoBusqueda<TDto>> ObtenerResultados(string busqueda, string empresa, DTO.ListaParametrosDeBusqueda parametros = null)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscaResultados = (IContextoDeBusqueda<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IContextoDeBusqueda<TEntidad, TDto>), para);
            //a cambiar el contexto de busqueda para que acepte parametros
            var resultado = buscaResultados.Buscar(busqueda, parametros);
            return resultado;
        }

        public List<DTO.ResultadoBusqueda<TDtoReducido>> ObtenerResultadosReducidos(string busqueda, string empresa, DTO.ListaParametrosDeBusqueda parametros = null)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscaResultados = (IContextoDeBusqueda<TEntidad, TDtoReducido>)FabricaNegocios.Instancia.Resolver(typeof(IContextoDeBusqueda<TEntidad, TDtoReducido>), para);
            //a cambiar el contexto de busqueda para que acepte parametros
            var resultado = buscaResultados.Buscar(busqueda, parametros);
            return resultado;
        }
    }
}
