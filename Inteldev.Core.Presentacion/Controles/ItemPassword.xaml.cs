using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
	/// Interaction logic for ItemPassword.xaml
	/// </summary>
	public partial class ItemPassword : Grid
	{

		public string Clave
		{
			get { return (string)GetValue(ClaveProperty); }
			set { SetValue(ClaveProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Clave.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ClaveProperty =
			DependencyProperty.Register("Clave", typeof(string), typeof(ItemPassword));		

		public ItemPassword( )
		{
			InitializeComponent();
		}
	}
}
