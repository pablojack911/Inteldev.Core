using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Inteldev.Core.Presentacion.Controles
{
	/// <summary>
	/// Interaction logic for ItemIdNombre.xaml
	/// </summary>
	public partial class ItemIdNombre : Grid, IItemFormulario
	{


		public ulong ValorID
		{
			get { return (ulong)GetValue(ValorIDProperty); }
			set { SetValue(ValorIDProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ValorID.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValorIDProperty =
			DependencyProperty.Register("ValorID", typeof(ulong), typeof(ItemIdNombre));



		public object ValorNombre
		{
			get { return (object)GetValue(ValorNombreProperty); }
			set { SetValue(ValorNombreProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ValorNombre.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValorNombreProperty =
			DependencyProperty.Register("ValorNombre", typeof(object), typeof(ItemIdNombre));

		

		public ItemIdNombre( )
		{
			InitializeComponent();
		}

		public UIElement SetItem(string etiqueta, string bindingPath)
		{
			throw new NotImplementedException();
		}

		public void Refresh( )
		{
			throw new NotImplementedException();
		}
	}
}
