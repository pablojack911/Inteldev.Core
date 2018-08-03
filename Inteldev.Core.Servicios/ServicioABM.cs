using System;
using System.Collections.Generic;
using System.Linq;
using Inteldev.Core.DTO;
using Inteldev.Core.Negocios;
using Inteldev.Core.Contratos;
using Inteldev.Core.Negocios.Busquedas;
using Inteldev.Core.Modelo;
using System.ServiceModel;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.DTO.Carriers;
using Microsoft.Practices.Unity;

namespace Inteldev.Core.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServicioABM<TDto, TEntidad> : IServicioABM<TDto>
        where TDto : DTOMaestro, new()
        where TEntidad : EntidadMaestro
    {
        public virtual TDto ObtenerPorId(int id, CargarRelaciones cargarEntidades, string empresa)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscador = (IBuscadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IBuscadorDTO<TEntidad, TDto>), para);
            var dto = buscador.BuscarSimple(id, cargarEntidades);
            return dto;
        }

        public virtual TDto ObtenerPorCodigo(object codigo, CargarRelaciones cargarEntidades, string empresa, ListaParametrosDeBusqueda parametros = null)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscador = (IBuscadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IBuscadorDTO<TEntidad, TDto>), para);
            var dto = buscador.BuscarPorCodigo<TEntidad>(codigo, cargarEntidades, parametros == null ? null : parametros.Parametros);
            return dto;
        }

        public virtual IList<TDto> ObtenerLista(object param, CargarRelaciones cargarEntidades, string empresa)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscador = (IBuscadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IBuscadorDTO<TEntidad, TDto>), para);
            return buscador.BuscarLista(param, cargarEntidades).ToList();
        }

        public virtual List<ResultadoBusqueda<TDto>> ObtenerResultados(string busqueda, string empresa, ListaParametrosDeBusqueda parametros = null)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var buscaResultados = (IContextoDeBusqueda<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IContextoDeBusqueda<TEntidad, TDto>), para);
            //a cambiar el contexto de busqueda para que acepte parametros
            var resultado = buscaResultados.Buscar(busqueda, parametros);
            return resultado;
        }

        public virtual GrabadorCarrier Grabar(TDto ODto, Usuario Usuario, string empresa = "")
        {
            //var antes = DateTime.Now;
            if (Usuario == null)
            {
                Usuario = new Usuario();
                Usuario.Nombre = "Anonymous";
            }
            GrabadorCarrier grabadorhel = new GrabadorCarrier();
            if (ODto != null)
            {
                ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
                var grabador = (IGrabadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IGrabadorDTO<TEntidad, TDto>), para);
                if (grabador == null)
                {
                    grabadorhel.setError(true);
                    grabadorhel.setMensaje("No se pudo guardar.\nCódigo de error: 0xSABM_G");
                }
                else
                    grabadorhel = grabador.Grabar(ODto, Usuario);
            }
            else
            {
                grabadorhel.setError(true);
                grabadorhel.setMensaje("No se pudo guardar.\nCódigo de error: 0xSABM_ODtoNull");
                //necesito mejor descripcion del error. 
            }
            return grabadorhel;
        }

        public ErrorCarrier Borrar(TDto ODto, Usuario Usuario, string empresa = "")
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var borrador = (IBorradorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(IBorradorDTO<TEntidad, TDto>), para);
            var result = borrador.Borrar(ODto, Usuario);
            return result;
        }

        public virtual CreadorCarrier<TDto> Crear(string empresa)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var creador = (ICreadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(ICreadorDTO<TEntidad, TDto>), para);
            var dto = creador.Crear();
            return dto;
        }

        public virtual CreadorCarrier<TDto> CrearConParametros(string empresa, params int[] args)
        {
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            var creador = (ICreadorDTO<TEntidad, TDto>)FabricaNegocios.Instancia.Resolver(typeof(ICreadorDTO<TEntidad, TDto>), para);
            return creador.Crear(args);
        }

        public bool EsValido(TDto Entidad)
        {
            throw new NotImplementedException();
        }

    }
}