using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.DTO.Usuarios;
using System;
namespace Inteldev.Core.Negocios
{
    public interface IBorradorDTO<TEntidad,TDto>
     where TDto : Inteldev.Core.DTO.DTOBase
    {
		ErrorCarrier Borrar(int id, Usuario Usuario);
		ErrorCarrier Borrar(TDto dto, Usuario Usuario);
    }
}
