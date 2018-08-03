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
    /// Interaction logic for ItemFormularioCheckAjustable.xaml
    /// </summary>
    public partial class ItemFormularioCheckAjustable : Grid
    {
        public ItemFormularioCheckAjustable()
        {
            InitializeComponent();
        }

        public bool Seleccionado
        {
            get { return (bool)GetValue(SeleccionadoProperty); }
            set { SetValue(SeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Seleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeleccionadoProperty =
            DependencyProperty.Register("Seleccionado", typeof(bool), typeof(ItemFormularioCheckAjustable));



        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioCheckAjustable));

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}
