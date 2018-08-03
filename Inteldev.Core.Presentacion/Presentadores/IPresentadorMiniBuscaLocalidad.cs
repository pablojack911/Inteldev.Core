using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
namespace Inteldev.Core.Presentacion.Presentadores
{
    public interface IPresentadorMiniBuscaLocalidad : IPresentadorMiniBusca<Localidad>
    {
        int ProvinciaId { get; set; }
    }
}
