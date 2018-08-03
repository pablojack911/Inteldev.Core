using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using Inteldev.Core.DTO.Locacion;
using System.Diagnostics;
using System;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Extenciones;
using System.Collections.Generic;
using System.Windows.Input;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para ItemFormularioMiniBusca.xaml
    /// </summary>
    public partial class ItemFormularioMiniBusca : Grid
    {
        public TextBox Texto { get { return this._txtId; } set { this._txtId = value; } }
        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        public override void EndInit()
        {
            base.EndInit();
        }
        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioMiniBusca));

        public ItemFormularioMiniBusca()
        {
            InitializeComponent();
            //AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandleKeyDownEvent);
            this._txtId.KeyDown += _txtId_KeyDown;
            this._txtId.Tag = "ItemFormularioMiniBusca";//Esto es para que no lo tome el keyDown del  App en Fixius
        }

        //private void HandleKeyDownEvent(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return || e.Key == Key.Tab)
        //    {
        //        if (_txtId.Text == "")
        //        {
        //            this._btnBuscar.Focus();
        //            e.Handled = true;
        //        }
        //        else
        //        {                    
        //            var focusDirection = FocusNavigationDirection.Next;

        //            TraversalRequest request = new TraversalRequest(focusDirection);

        //            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

        //            if (elementWithFocus != null)
        //                elementWithFocus.MoveFocus(request);

        //            elementWithFocus = Keyboard.FocusedElement as UIElement;

        //            if (elementWithFocus != null)
        //                elementWithFocus.MoveFocus(request);
        //            e.Handled = false;
        //        }
        //    }
        //}

        void _txtId_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return || e.Key == Key.Tab)
            {
                string valorOriginal = _txtId.Text;

                ICommand cmdBuscar = this.Presentador.Reflection().GetValue<ICommand>("CmdBuscarPorId");
                cmdBuscar.Execute(this._txtId.Text); //Se ejecuta el command para refrescar la propiedad de dependencia bindeada con el Txt

                if (valorOriginal == "") //si el valor original del txtId es "" el foco se transfiere al Boton Buscar
                {
                    this._btnBuscar.Focus();
                }
                else
                {
                    if (_txtId.Text == "") //esto ocurre si no se encontro ninguna entidad en el command buscar por id. txtId queda "" y necesita ganar foco para volver a buscar.
                    {
                        this._txtId.Focus();
                    }
                    else //se encontro la entidad buscada, se salta el foco 2 veces hacia el proximo control.
                    {
                        var focusDirection = FocusNavigationDirection.Next;

                        TraversalRequest request = new TraversalRequest(focusDirection);

                        UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                        if (elementWithFocus != null)
                            elementWithFocus.MoveFocus(request);

                        elementWithFocus = Keyboard.FocusedElement as UIElement;

                        if (elementWithFocus != null)
                            elementWithFocus.MoveFocus(request);

                    }
                }
            }
        }


        public object Entidad
        {
            get { return (object)GetValue(EntidadProperty); }
            set
            {
                SetValue(EntidadProperty, value);

            }
        }

        // Using a DependencyProperty as the backing store for Entidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntidadProperty =
            DependencyProperty.Register("Entidad", typeof(object), typeof(ItemFormularioMiniBusca));



        public object Presentador
        {
            get { return (object)GetValue(PresentadorProperty); }
            set { SetValue(PresentadorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProperty =
            DependencyProperty.Register("Presentador", typeof(object), typeof(ItemFormularioMiniBusca));


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "Entidad")
            {
                if (Presentador != null)
                {
                    Presentador.Reflection().SetValue("Entidad", this.Entidad);
                    Action<Object> action = (param => this.Entidad = this.Presentador.GetType().GetProperty("Entidad").GetValue(this.Presentador, null));
                    Presentador.Reflection().SetValue("ActualizarDto", action);
                }
                if (e.NewValue == null)
                {
                    this._txtId.Text = string.Empty;
                }
            }
        }

        public IInputElement Campo
        {
            get { return this._txtId; }
        }

    }
}
