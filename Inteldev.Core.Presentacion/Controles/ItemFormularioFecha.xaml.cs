using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para ItemFormularioFecha.xaml
    /// </summary>
    public partial class ItemFormularioFecha : Grid
    {



		public object Fecha
		{
			get { return (object)GetValue(FechaProperty); }
			set { SetValue(FechaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Fecha.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FechaProperty =
			DependencyProperty.Register("Fecha", typeof(object), typeof(ItemFormularioFecha));

		
		
		public string Etiqueta
		{
			get { return (string)GetValue(EtiquetaProperty); }
			set { SetValue(EtiquetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EtiquetaProperty =
			DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioFecha));

        public DatePicker TextFecha { get { return this.txtCampo; } set { this.txtCampo = value; } }

        public ItemFormularioFecha()        
        {
            InitializeComponent();
			
        }

    }
}
