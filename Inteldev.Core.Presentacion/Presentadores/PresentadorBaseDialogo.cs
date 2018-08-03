using System.Windows.Input;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Windows;

namespace Inteldev.Core.Presentacion.Presentadores
{
    public class PresentadorBaseDialogo<TEntidad> : PresentadorBase<TEntidad>, IPresentadorBaseDialogo<TEntidad>
        where TEntidad : DTOBase, new()
    {

        public ICommand CmdAceptar { get; set; }
        public ICommand CmdCancelar { get; set; }

        public PresentadorBaseDialogo()
        {
            this.CmdAceptar = new RelayCommand(a => TryCatch.Intentar(delegate(object o)
                {
                    this.Ventana.Close();
                    this.DialogoCerrado(true);
                }));
            this.CmdCancelar = new RelayCommand(c => TryCatch.Intentar(delegate(object o)
            {
                this.EntidadActual = null;
                this.Ventana.Close();
                this.DialogoCerrado(false);
            }));
        }

        public event Action<bool> DialogoCerrado;
    }
}
