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
    /// Interaction logic for ItemFormularioCuit.xaml
    /// </summary>
    public partial class ItemFormularioCuit : Grid
    {
        public ItemFormularioCuit()
        {
            InitializeComponent();
        }

        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioCuit));

        public object Valor
        {
            get { return (string)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(object), typeof(ItemFormularioCuit));
    }

    
}
