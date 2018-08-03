using System;
namespace Inteldev.Core.Negocios.Mapeador
{
    public interface IMapeo
    {
        AutoMapper.IMappingExpression<TDto, TEntidad> MapeoDtoToEntidad<TDto, TEntidad>();
        AutoMapper.IMappingExpression<TEntidad, TDto> MapeoEntidadToDto<TEntidad, TDto>();
    }
}
