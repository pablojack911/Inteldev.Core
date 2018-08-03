using System;
namespace Inteldev.Core.Presentacion.Presentadores
{
    public interface IPresentadorBuscador
    {
        System.Windows.Input.ICommand CmdBuscar { get; }
        System.Windows.Input.ICommand CmdSeleccionarItem { get; set; }
        System.Windows.Input.ICommand CmdSeleccionarResultado { get; set; }
        DTO.DTOMaestro ItemSeleccionado { get; set; }
    }
}
