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
    /// Lógica de interacción para ItemFormularioTextMultilinea.xaml
    /// </summary>
    public partial class ItemFormularioTextMultilinea : Grid
    {



		public string Etiqueta
		{
			get { return (string)GetValue(EtiquetaProperty); }
			set { SetValue(EtiquetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EtiquetaProperty =
			DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioTextMultilinea));



		public string Texto
		{
			get { return (string)GetValue(TextoProperty); }
			set { SetValue(TextoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Texto.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TextoProperty =
			DependencyProperty.Register("Texto", typeof(string), typeof(ItemFormularioTextMultilinea));

        public ItemFormularioTextMultilinea()
        {
            InitializeComponent();
        }
       
    }
}
