using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.DTO.Usuarios;
using System;
namespace Inteldev.Core.Negocios
{
    public interface IGrabadorDTO<TEntidad,TDto>
    {
        GrabadorCarrier Grabar(TDto dto, Usuario Usuario);
    }
}
