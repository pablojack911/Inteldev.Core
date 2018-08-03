
using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Lógica de interacción para ItemFormularioContactos.xaml
	/// </summary>
	public partial class ItemFormularioContactos : Grid
	{
		

		public object Presentador
		{
			get { return (object)GetValue(PresentadorProperty); }
			set {SetValue(PresentadorProperty, value);}
		}

		// Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProperty =
			DependencyProperty.Register("Presentador", typeof(object), typeof(ItemFormularioContactos));
		

		public string Etiqueta
		{
			get { return (string)GetValue(EtiquetaProperty); }
			set { SetValue(EtiquetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EtiquetaProperty =
			DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioContactos));

		

		public ItemFormularioContactos( )
		{
			InitializeComponent();
		}

	}
}