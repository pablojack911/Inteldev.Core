using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

namespace Inteldev.Core.Presentacion.Presentadores
{
    /// <summary>
    /// Presentador Principal. Contiene la ventana y la vista. Sirve de union entre las dos.
    /// </summary>
    /// <typeparam name="TEntidad"></typeparam>
    public class PresentadorBase<TEntidad> : DependencyObject, IPresentadorBase<TEntidad>
        where TEntidad : DTOBase, new()
    {

        #region DP's

        public TEntidad EntidadActual
        {
            get
            {
                return this.GetValue(EntidadActualProperty) as TEntidad;
            }
            set
            {
                if (this.Entidades != null)
                {
                    if (!this.Entidades.Contains(value))
                        this.Entidades.Add(value);
                }
                this.SetValue(EntidadActualProperty, value);
            }
        }

        public static readonly DependencyProperty EntidadActualProperty = DependencyProperty.Register("EntidadActual", typeof(TEntidad), typeof(PresentadorBase<TEntidad>));

        public ObservableCollection<TEntidad> Entidades
        {
            get
            {
                return this.GetValue(EntidadesProperty) as ObservableCollection<TEntidad>;
            }
            set
            {
                this.SetValue(EntidadesProperty, value);
            }
        }

        public static readonly DependencyProperty EntidadesProperty = DependencyProperty.Register("Entidades", typeof(ObservableCollection<TEntidad>), typeof(PresentadorBase<TEntidad>));


        #endregion

        #region Propiedades Privadas

        private Window ventana;
        private FrameworkElement vista;

        #endregion

        #region Public Wrappers

        public Window Ventana
        {
            get
            {
                return ventana;
            }
            set
            {
                ventana = value;
                ventana.DataContext = this;
            }
        }



        public FrameworkElement Vista
        {
            get
            {
                return vista;
            }
            set
            {
                vista = value;
            }
        }

        #endregion

        public void Ejecutar()
        {
            this.vista.DataContext = this.EntidadActual;

            this.ventana.ShowDialog();
        }
    }
}
