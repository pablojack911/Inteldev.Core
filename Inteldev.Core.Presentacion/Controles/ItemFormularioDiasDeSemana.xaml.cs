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
    /// Interaction logic for ItemFormularioDiasDeVisita.xaml
    /// </summary>
    public partial class ItemFormularioDiasDeSemana : UserControl
    {
        public ItemFormularioDiasDeSemana()
        {
            InitializeComponent();
        }

        public string Titulo
        {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Titulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloProperty =
            DependencyProperty.Register("Titulo", typeof(string), typeof(ItemFormularioDiasDeSemana));





        public Object DiasDeSemana
        {
            get { return (Object)GetValue(DiasDeSemanaProperty); }
            set { SetValue(DiasDeSemanaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DiasDeSemana.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiasDeSemanaProperty =
            DependencyProperty.Register("DiasDeSemana", typeof(Object), typeof(ItemFormularioDiasDeSemana));

        

        

    }
}

