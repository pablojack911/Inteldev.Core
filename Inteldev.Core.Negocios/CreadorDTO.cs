using Inteldev.Core.Negocios.Mapeador;
using Inteldev.Core.Modelo;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.DTO;
using Microsoft.Practices.Unity;
namespace Inteldev.Core.Negocios
{
    /// <summary>
    /// Los creadores Carrier se setean aca. O sea, que si queres hacer alguna validacion o algo con la entidad 
    /// que creas, hacelo aca, asi despues en la capa de presentacion, se muestra el cartel.
    /// </summary>
    /// <typeparam name="TEntidad"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public class CreadorDTO<TEntidad, TDto> : ICreadorDTO<TEntidad,TDto> 
        where TEntidad:EntidadBase
        where TDto:DTOBase
    {
        protected ICreador<TEntidad> CreadorEntidad;
        protected IMapeadorGenerico<TEntidad, TDto> Mapeador;
        protected string empresa;
        protected string entidad;
        public CreadorDTO(ICreador<TEntidad> creadorEntidad,IMapeadorGenerico<TEntidad, TDto> mapeador, string empresa, string entidad)
        {
            this.CreadorEntidad = creadorEntidad;
            this.Mapeador = mapeador;
            this.empresa = empresa;
            this.entidad = entidad;
        }

        public CreadorDTO(ICreador<TEntidad> creadorEntidad, string empresa, string entidad)
        {
            this.CreadorEntidad = creadorEntidad;
            ParameterOverride[] para = { new ParameterOverride("empresa", empresa), new ParameterOverride("entidad", typeof(TEntidad).Name.ToLower()) };
            this.Mapeador = (IMapeadorGenerico<TEntidad,TDto>)FabricaNegocios.Instancia.Resolver(typeof(IMapeadorGenerico<TEntidad,TDto>),para);
            this.empresa = empresa;
            this.entidad = entidad;
        }

        public virtual CreadorCarrier<TDto> Crear()        
        {
            var creadorCarrier = new CreadorCarrier<TDto>();
            creadorCarrier.SetEntidad(this.Mapeador.EntidadToDto(this.CreadorEntidad.Crear()));
            return creadorCarrier;                                 
        }

        public virtual CreadorCarrier<TDto> Crear(params int[] args)
        {
            var creadorCarrier = new CreadorCarrier<TDto>();
            creadorCarrier.SetEntidad(this.Mapeador.EntidadToDto(this.CreadorEntidad.Crear(args)));
            return creadorCarrier;                     
        }

        public virtual CreadorCarrier<TDto> Crear(params string[] args)
        {
            var creadorCarrier = new CreadorCarrier<TDto>();
            creadorCarrier.SetEntidad(this.Mapeador.EntidadToDto(this.CreadorEntidad.Crear(args)));
            return creadorCarrier;                     
        }
    }
}
