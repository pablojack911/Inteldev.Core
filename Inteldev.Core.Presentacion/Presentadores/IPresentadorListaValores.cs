using System;
namespace Inteldev.Core.Presentacion.Presentadores
{
    public interface IPresentadorListaValores<TMaestro, TDetalle, TValor>
    {
        bool Agregar();
        System.Windows.Input.ICommand CmdAgregar { get; set; }
        TValor Valor { get; set; }
    }
}
