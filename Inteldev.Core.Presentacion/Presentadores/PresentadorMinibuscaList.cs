using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.DTO;
using System.Windows;
using System.Collections.ObjectModel;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Controladores;
using System.Windows.Input;
using System.Collections;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

namespace Inteldev.Core.Presentacion.Presentadores
{
    public class PresentadorMinibuscaList<TMaestro, TDetalle> : DependencyObject, IPresentadorMinibuscaList<TMaestro, TDetalle>
        where TMaestro : class, new()
        where TDetalle : DTOBase, new()
    {

        #region DP

        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(string), typeof(PresentadorMinibuscaList<TMaestro, TDetalle>));


        #endregion

        #region Campos

        //public IPresentadorMiniBusca<TDetalle> PMB { get; set; }
        //public IPresentadorMaestroDetalle<TMaestro, TDetalle> PMD { get; set; }
        public IPresentadorMiniBusca<TDetalle> PMB { get; set; }
        public IPresentadorMaestroDetalle<TMaestro, TDetalle> PMD { get; set; }

        #endregion

        #region Commands

        public ICommand CmdBorrar { get; set; }

        #endregion

        #region Constructores

        public PresentadorMinibuscaList(IPresentadorMiniBusca<TDetalle> PMB, IPresentadorMaestroDetalle<TMaestro, TDetalle> PMD)
        {
            this.PMB = PMB;
            this.PMB.CambioEntidad += PMB_CambioEntidad;
            this.CmdBorrar = new RelayCommand(p => this.BorrarItem());
            this.PMD = PMD;

        }

        #endregion

        private object BorrarItem()
        {
            var item = PMD.ItemSeleccionado as DTOBase;
            PMD.BorrarItem();
            return true;
        }
        /// <summary>
        /// Se captura evento de cambio de entidad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contiene la entidad que se busco</param>
        public void PMB_CambioEntidad(object sender, ArgumentoGenerico<TDetalle> e)
        {
            var objeto = e.GET();
            if (objeto.Id != 0)
            {
                var esta = PMD.DetalleDTO.FirstOrDefault(p => p.Id == objeto.Id);
                if (esta != null)
                {
                    PMD.DetalleDTO.Remove(esta);
                    PMD.Detalle.Remove(esta);
                }
                PMD.Detalle.Add(objeto);
            }
        }
    }
}
