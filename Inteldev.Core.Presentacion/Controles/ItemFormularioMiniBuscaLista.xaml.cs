using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using Inteldev.Core.Extenciones;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para ItemFormularioMiniBuscaLista.xaml
    /// </summary>
    public partial class ItemFormularioMiniBuscaLista : Grid
    {

        public object Presentador
        {
            get { return (object)GetValue(PresentadorProperty); }
            set { SetValue(PresentadorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProperty =
            DependencyProperty.Register("Presentador", typeof(object), typeof(ItemFormularioMiniBuscaLista));

        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioMiniBuscaLista));



        public object Items
        {
            get { return (object)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(object), typeof(ItemFormularioMiniBuscaLista));



        public object ItemSeleccionado
        {
            get { return (object)GetValue(ItemSeleccionadoProperty); }
            set { SetValue(ItemSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSeleccionadoProperty =
            DependencyProperty.Register("ItemSeleccionado", typeof(object), typeof(ItemFormularioMiniBuscaLista));


        public ObservableCollection<DataGridColumn> Columnas
        {
            get { return (ObservableCollection<DataGridColumn>)GetValue(ColumnasProperty); }
            set { SetValue(ColumnasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnasProperty =
            DependencyProperty.Register("Columnas", typeof(ObservableCollection<DataGridColumn>), typeof(ItemFormularioMiniBuscaLista));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

            base.OnPropertyChanged(e);
            if (e.Property.Name == "Items")
            {
                if (this.Presentador != null)
                {
                    var pmd = this.Presentador.Reflection().GetValue("PMD");
                    if (pmd != null)
                        pmd.Reflection().SetValue("DTO", this.Items);
                }
            }
        }


        public ItemFormularioMiniBuscaLista()
        {
            InitializeComponent();
            this._txtId.KeyDown += _txtId_KeyDown;
            this._txtId.Tag = "ItemFormularioMiniBuscaList";//Esto es para que no lo tome el keyDown del  App en Fixius
            this.Columnas = dataGrid1.Columns;

        }

        void _txtId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Tab)
            {
                string valorOriginal = _txtId.Text;

                ICommand cmdBuscar = this.Presentador.Reflection().GetValue<Object>("PMB").Reflection().GetValue<ICommand>("CmdBuscarPorId");
                cmdBuscar.Execute(this._txtId.Text); //Se ejecuta el command para refrescar la propiedad de dependencia bindeada con el Txt

                if (valorOriginal == "") //si el valor original del txtId es "" el foco se transfiere al Boton Buscar
                {
                    this._btnBuscar.Focus();
                }
                else
                {
                    this._txtId.Text = "";
                    this._txtId.Focus();
                }
            }
        }
    }
}
