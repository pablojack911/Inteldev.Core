using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Inteldev.Core.Extenciones;
namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Interaction logic for BuscadorInicial.xaml
    /// </summary>
    public partial class BuscadorInicial : UserControl
    {

        private List<string> listaOmitidos;
        public BuscadorInicial()
        {
            InitializeComponent();
            this.DataContextChanged += UserControl_DataContextChanged;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var prop = this.DataContext.GetType().GetProperty("ListaOmitidos");
            var valueprop = prop.GetValue(this.DataContext, null);
            listaOmitidos = (List<string>)valueprop;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = e.Column.Header.ToString().SplitCamelCase();
            var propdesc = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
            //si no encuentro el astributo IncluirEnBuscadorAttribute en la propiedad cancelo la generacion de la columna
            if (!propdesc.Attributes.OfType<IncluirEnBuscadorAttribute>().Any() || listaOmitidos.Any(p => p == propdesc.Name))
            {
                e.Cancel = true;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtBusqueda.Focus();
            //txtBusqueda.Focus();
        }
    }
}
