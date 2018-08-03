using System;
using System.Collections;
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
	/// Interaction logic for ItemFormularioComboBox.xaml
	/// </summary>
	public partial class ItemFormularioComboBox : Grid
	{
		public ComboBox Combo { get { return this.combo; } set { this.combo = value; } }

		public object ItemsDetalle
		{
			get { return (object)GetValue(ItemsDetalleProperty); }
			set { SetValue(ItemsDetalleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemsDetalle.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemsDetalleProperty =
			DependencyProperty.Register("ItemsDetalle", typeof(object), typeof(ItemFormularioComboBox));

		public object Seleccionado
		{
			get { return (object)GetValue(SeleccionadoProperty); }
			set { SetValue(SeleccionadoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Seleccionado.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SeleccionadoProperty =
			DependencyProperty.Register("Seleccionado", typeof(object), typeof(ItemFormularioComboBox));

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);
        //    if (Seleccionado != null)
        //    {
        //        if (e.Property.Name.ToString() == "Seleccionado" && ItemsDetalle != null)
        //        {
        //            var id = (int)e.NewValue.GetType().GetProperty("Id").GetValue(Seleccionado, null);
        //            var items = ItemsDetalle as IEnumerable<DTO.DTOBase>;
        //            this.combo.SelectedItem = items.FirstOrDefault(p => p.Id == id);
        //        }
        //    }
        //}

		public string Etiqueta
		{
			get { return (string)GetValue(EtiquetaProperty); }
			set { SetValue(EtiquetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EtiquetaProperty =
			DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioComboBox));

		public ItemFormularioComboBox( )
		{
			InitializeComponent();
		}

	}
}
