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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Core.Presentacion.Controles
{
	/// <summary>
	/// Interaction logic for ItemFormularioID.xaml
	/// </summary>
	public partial class ItemFormularioID : Grid
	{

		public ulong ID
		{
			get { return (ulong)GetValue(IDProperty); }
			set { SetValue(IDProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IDProperty =
			DependencyProperty.Register("ID", typeof(ulong), typeof(ItemFormularioID));

		

		public ItemFormularioID( )
		{
			InitializeComponent();
		}
	}
}
