using System;
using System.Collections;
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
    /// Lógica de interacción para ItemFormularioGrilla.xaml
    /// </summary>
    public partial class ItemFormularioGrilla : Grid
    {

        //public DataGrid DataGrid { get { return this.dataGrid1; } set { this.dataGrid1 = value; } }


        public DataGrid DataGrid
        {
            get { return (DataGrid)GetValue(DataGridProperty); }
            set { SetValue(DataGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridProperty =
            DependencyProperty.Register("DataGrid", typeof(DataGrid), typeof(ItemFormularioGrilla));

        public ObservableCollection<DataGridColumn> Columnas
        {
            get { return (ObservableCollection<DataGridColumn>)GetValue(ColumnasProperty); }
            set { SetValue(ColumnasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnasProperty =
            DependencyProperty.Register("Columnas", typeof(ObservableCollection<DataGridColumn>), typeof(ItemFormularioGrilla));



        public bool AutoGenerarColumnas
        {
            get { return (bool)GetValue(AutoGenerarColumnasProperty); }
            set { SetValue(AutoGenerarColumnasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoGenerarColumnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoGenerarColumnasProperty =
            DependencyProperty.Register("AutoGenerarColumnas", typeof(bool), typeof(ItemFormularioGrilla));


        public object Presentador
        {
            get { return (object)GetValue(PresentadorProperty); }
            set { SetValue(PresentadorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProperty =
            DependencyProperty.Register("Presentador", typeof(object), typeof(ItemFormularioGrilla));


        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioGrilla));




        public bool PuedeEditar
        {
            get { return (bool)GetValue(PuedeEditarProperty); }
            set { SetValue(PuedeEditarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PuedeEditar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuedeEditarProperty =
            DependencyProperty.Register("PuedeEditar", typeof(bool), typeof(ItemFormularioGrilla), new PropertyMetadata(true));



        public Visibility BotonEditarVisible
        {
            get { return (Visibility)GetValue(BotonEditarVisibleProperty); }
            set { SetValue(BotonEditarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonEditarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonEditarVisibleProperty =
            DependencyProperty.Register("BotonEditarVisible", typeof(Visibility), typeof(ItemFormularioGrilla), new PropertyMetadata(Visibility.Visible));



        public bool PuedeBorrar
        {
            get { return (bool)GetValue(PuedeBorrarProperty); }
            set { SetValue(PuedeBorrarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PuedeBorrar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuedeBorrarProperty =
            DependencyProperty.Register("PuedeBorrar", typeof(bool), typeof(ItemFormularioGrilla), new PropertyMetadata(true));



        public Visibility BotonEliminarVisible
        {
            get { return (Visibility)GetValue(BotonEliminarVisibleProperty); }
            set { SetValue(BotonEliminarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonEliminarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonEliminarVisibleProperty =
            DependencyProperty.Register("BotonEliminarVisible", typeof(Visibility), typeof(ItemFormularioGrilla), new PropertyMetadata(Visibility.Visible));




        public ItemFormularioGrilla()
        {

            InitializeComponent();

            this.Columnas = dataGrid1.Columns;

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == PuedeEditarProperty)
            {
                if (this.PuedeEditar == false)
                    this.BotonEditarVisible = System.Windows.Visibility.Collapsed;
                else
                    this.BotonEditarVisible = System.Windows.Visibility.Visible;
            }
            if (e.Property == PuedeBorrarProperty)
            {
                if (this.PuedeBorrar == false)
                    this.BotonEliminarVisible = System.Windows.Visibility.Collapsed;
                else
                    this.BotonEliminarVisible = System.Windows.Visibility.Visible;
            }
            base.OnPropertyChanged(e);
        }


    }
}
