using System;
using System.Windows;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
    public interface IPresentadorGrilla<TMaestro, TDetalle, TVista> : IPresentadorMaestroDetalle<TMaestro, TDetalle>
     where TDetalle : Inteldev.Core.DTO.DTOBase, new()
    {
        bool Aceptar();
        bool AgregarItem();
        bool Cancelar();
        bool PuedeAceptar();
        void CerrarVentana();
        System.Windows.Input.ICommand CmdAceptar { get; set; }
        System.Windows.Input.ICommand CmdAgregar { get; set; }
        System.Windows.Input.ICommand CmdBorrar { get; set; }
        System.Windows.Input.ICommand CmdCancelar { get; set; }
        System.Windows.Input.ICommand CmdEditar { get; set; }
        void CrearVentana();
        void quitarColumnasIdNombre(FrameworkElement Grid, Type Objeto);
        bool Editar();
        void Inicializar();
        TDetalle Objeto { get; set; }
    }
}
