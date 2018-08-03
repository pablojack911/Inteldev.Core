using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Controles;
using System;
using System.Collections.Generic;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
    public interface IPresentadorMiniBusca<TEntidad> : ICambioEntidad<TEntidad>
        where TEntidad : Inteldev.Core.DTO.DTOBase, new()
    {
        object Aceptar();
        Func<List<ParametrosMiniBusca>> ObtenerParametros { get; set; }
        object Buscar();
        object BuscarPorId(object p);
        object Cancelar();
        System.Windows.Input.ICommand CmdAceptar { get; set; }
        System.Windows.Input.ICommand CmdBuscarPorId { get; set; }
        System.Windows.Input.ICommand CmdCancelar { get; set; }
        System.Windows.Input.ICommand CmdVerBuscador { get; set; }
        bool PuedeAceptar();
        bool PuedeBuscar();
        TEntidad Entidad { get; set; }
        Action<TEntidad> ActualizarDto { get; set; }
        bool PuedeBuscarPorId(object p);
        bool PuedeCancelar();
        void SeleccionarEntidad(TEntidad entidad);
        void Reset();
        int cantidadNumeros { get; set; }
        CargarRelaciones CargaRelaciones { get; set; }
    }
}
