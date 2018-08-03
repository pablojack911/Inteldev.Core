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

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para ItemFormularioCheck.xaml
    /// </summary>
    public partial class ItemFormularioCheck : Grid
    {



        public bool Seleccionado
        {
            get { return (bool)GetValue(SeleccionadoProperty); }
            set { SetValue(SeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Seleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeleccionadoProperty =
            DependencyProperty.Register("Seleccionado", typeof(bool), typeof(ItemFormularioCheck));



        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioCheck));




        public ItemFormularioCheck()
        {
            InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        public IInputElement Campo
        {
            get { return this.checkBox; }
        }

    }
}
