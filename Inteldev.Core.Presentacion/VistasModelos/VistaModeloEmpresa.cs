using Inteldev.Core.DTO.Organizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Core.Presentacion.VistasModelos
{
    /// <summary>
    /// Vista Modelo para administrar la vista de la Empresa
    /// </summary>
    public class VistaModeloEmpresa : VistaModeloBase<Empresa>, IDataErrorInfo
    {
        #region DP's
        /// <summary>
        /// Propiedad para gestionar la condicion de iva
        /// </summary>
        public object CondicionAnteIva
        {
            get { return (object)GetValue(CondicionAnteIvaProperty); }
            set { SetValue(CondicionAnteIvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CondicionAnteIva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CondicionAnteIvaProperty =
            DependencyProperty.Register("CondicionAnteIva", typeof(object), typeof(VistaModeloEmpresa));

        /// <summary>
        /// Propiedad para gestionar el cuit
        /// </summary>
        public string CUIT
        {
            get { return (string)GetValue(CUITProperty); }
            set { SetValue(CUITProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CUIT.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CUITProperty =
            DependencyProperty.Register("CUIT", typeof(string), typeof(VistaModeloEmpresa));


        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public VistaModeloEmpresa()
            : base()
        {

        }

        /// <summary>
        /// Constructor con parametro
        /// </summary>
        /// <param name="DTO">Empresa traida de bd</param>
        public VistaModeloEmpresa(Empresa DTO)
            : base(DTO)
        {
            this.CUIT = this.Modelo.CUIT;

            this.CondicionAnteIva = this.Modelo.CondicionAnteIVA;
        }

        #endregion

        /// <summary>
        /// Gestion de las propiedades de dependencia
        /// </summary>
        /// <param name="e">Cambios en las propiedades</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == CondicionAnteIvaProperty)
            {
                this.Modelo.CondicionAnteIVA = (DTO.Fiscal.CondicionAnteIva)e.NewValue;

                var memcuit = this.CUIT;
                this.CUIT = memcuit == null ? string.Empty : null;
                this.CUIT = memcuit;
            }
            if (e.Property == CUITProperty)
            {
                this.Modelo.CUIT = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
            }
            base.OnPropertyChanged(e);

        }

        #region Implementacion IDataErrorInfo
        /// <summary>
        /// Error...
        /// </summary>
        public string Error
        {
            get { return ""; }
        }

        /// <summary>
        /// Ponele
        /// </summary>
        /// <param name="columnName">Nombre columna</param>
        /// <returns>Un string</returns>
        public string this[string columnName]
        {
            get { return this.Modelo[columnName]; }
        }
        #endregion
    }
}
