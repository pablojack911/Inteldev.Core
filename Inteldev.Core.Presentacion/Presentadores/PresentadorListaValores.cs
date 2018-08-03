using Inteldev.Core.Presentacion.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Core.Presentacion.Presentadores
{
    public class PresentadorListaValores<TMaestro, TDetalle, TValor> : PresentadorMaestroDetalle<TMaestro, TDetalle>, IPresentadorListaValores<TMaestro, TDetalle, TValor>
        where TMaestro : new()
        where TDetalle : new()
    {
        #region Comandos

        public ICommand CmdAgregar { get; set; }

        #endregion

        public PresentadorListaValores():base()
        {
            this.CmdAgregar = new RelayCommand(p => this.Agregar());
        }

        public bool Agregar()
        {
            var detalle = new TDetalle();
            detalle.GetType().GetProperty("Valor").SetValue(detalle, this.Valor, null);
            this.Detalle.Add(detalle);
            
            return true;
        }

        public override void Inicializar()
        {
            base.Inicializar();            
            this.Detalle.Clear();
            this.DetalleDTO.Clear();
        }

        public TValor Valor
        {
            get { return (TValor)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(TValor), typeof(PresentadorListaValores<TMaestro, TDetalle, TValor>));

    }
}
