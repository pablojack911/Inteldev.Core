using Inteldev.Core.DTO.Usuarios;
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
	/// Interaction logic for TreeViewPerfilUsuario.xaml
	/// </summary>
	public partial class TreeViewPerfilUsuario : UserControl
	{

		public Permiso ItemSeleccionado
		{
			get { return (Permiso)GetValue(ItemSeleccionadoProperty); }
			set { SetValue(ItemSeleccionadoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemSeleccionado.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemSeleccionadoProperty =
			DependencyProperty.Register("ItemSeleccionado", typeof(Permiso), typeof(TreeViewPerfilUsuario));

		public TreeViewPerfilUsuario( )
		{
			InitializeComponent();
		}

	}
}
