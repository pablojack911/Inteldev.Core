using Inteldev.Core.DTO;
using System;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
    public interface IPresentadorBaseDialogo<TEntidad> : IPresentadorBase<TEntidad>
        where TEntidad : DTOBase, new()
    {
        System.Windows.Input.ICommand CmdAceptar { get; set; }
        System.Windows.Input.ICommand CmdCancelar { get; set; }

        event Action<bool> DialogoCerrado;
    }
}
