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
using Inteldev.Core.Extenciones;
using System.Windows.Threading;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Interaction logic for ItemFormularioComboBoxEnums.xaml
    /// </summary>
    public partial class ItemFormularioComboBoxEnums : Grid
    {

        public ComboBox Combo { get { return this.control; } set { this.control = value; } }
        public object ItemsDetalle
        {
            get { return (object)GetValue(ItemsDetalleProperty); }
            set { SetValue(ItemsDetalleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsDetalle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsDetalleProperty =
            DependencyProperty.Register("ItemsDetalle", typeof(object), typeof(ItemFormularioComboBoxEnums));

        public object Seleccionado
        {
            get { return (object)GetValue(SeleccionadoProperty); }
            set { SetValue(SeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Seleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeleccionadoProperty =
            DependencyProperty.Register("Seleccionado", typeof(object), typeof(ItemFormularioComboBoxEnums));

        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioComboBoxEnums));

        public event Action EventoCombo;


        public ItemFormularioComboBoxEnums()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => this.cargar()));
        }



        public string[] Excluir
        {
            get { return (string[])GetValue(ExcluirProperty); }
            set { SetValue(ExcluirProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Excluir.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExcluirProperty =
            DependencyProperty.Register("Excluir", typeof(string[]), typeof(ItemFormularioComboBoxEnums));



        private object cargar()
        {
            if (Tipo != null)
            {
                var objectProvider = new ObjectDataProvider();
                objectProvider.ObjectType = typeof(EnumHelper);
                objectProvider.MethodName = "GetValuesAndDescriptions";
                objectProvider.MethodParameters.Add(this.Tipo);
                if (Excluir != null)
                    objectProvider.MethodParameters.Add(Excluir);
                Binding binding = new Binding();
                binding.Source = objectProvider;
                binding.Mode = BindingMode.OneWay;
                this.control.SetBinding(ComboBox.ItemsSourceProperty, binding);
            }
            return true;
        }

        public Type Tipo
        {
            get { return (Type)GetValue(TipoProperty); }
            set { SetValue(TipoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tipo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipoProperty =
            DependencyProperty.Register("Tipo", typeof(Type), typeof(ItemFormularioComboBoxEnums));



        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.EventoCombo != null)
                this.EventoCombo();
        }

        public ObjectDataProvider DataProvider { get; set; }
    }
}
