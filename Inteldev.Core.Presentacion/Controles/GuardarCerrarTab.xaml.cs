using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para GuardarCerrarTab.xaml
    /// </summary>
    public partial class GuardarCerrarTab : StackPanel
    {




		public string Etiqueta
		{
			get { return (string)GetValue(EtiquetaProperty); }
			set { SetValue(EtiquetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EtiquetaProperty =
		DependencyProperty.Register("Etiqueta", typeof(string), typeof(GuardarCerrarTab));
		
		
		

        public GuardarCerrarTab()
        {
            InitializeComponent();
            
            
        }
        private string texto;

        public string Texto
        {
            get { return texto; }
            set 
            { 
                texto = value;
                //this.Label.Content = texto;
            }
        }        
    }
}
