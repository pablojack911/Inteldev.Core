using Inteldev.Core.DTO;
using System;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
    public interface ICambioEntidad<TEntidad>
		where TEntidad : DTOBase
    {
		event EventHandler<ArgumentoGenerico<TEntidad>> CambioEntidad;
    }
}
