using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios
{
    public interface ICreadorDTO<TEntidad, TDto>
        where TEntidad : EntidadBase
        where TDto : DTOBase
    {
        CreadorCarrier<TDto> Crear();
        CreadorCarrier<TDto> Crear(params int[] args);
        CreadorCarrier<TDto> Crear(params string[] args);
    }
}
