//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Input;
//using System.Collections.ObjectModel;
//using Inteldev.Core.Presentacion.Presentadores;
//using Inteldev.Core.Presentacion.Presentadores.Interfaces;

//namespace Inteldev.Core.Presentacion.Controladores
//{
//    public class ControladorArbol<TEntidad> : DependencyObject, IPresentadorBuscador<TEntidad> where TEntidad : class
//    {

//        public delegate void CambioItemSeleccionadoEventHandler(TEntidad ItemSeleccionado);
//        public event PresentadorBuscador<TEntidad>.CambioItemEventHandler CambioItemSeleccionado;
                
//        public ICommand CmdBuscar { get; set; }

//        public ICommand CmdSeleccionarItem { get; set; }

//        public static DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<TEntidad>), typeof(ControladorArbol<TEntidad>));

//        public ObservableCollection<TEntidad> Items
//        {
//            get
//            {
//                return this.GetValue(ItemsProperty) as ObservableCollection<TEntidad>;
//            }

//            set
//            {
//                this.SetValue(ItemsProperty, value);
//            }
//        }

//        private TEntidad itemSeleccionado;

//        public TEntidad ItemSeleccionado
//        {
//            get { return itemSeleccionado; }
//            set
//            {
//                itemSeleccionado = value;
//                //if (this.CambioItemSeleccionado != null)
//                //    this.CambioItemSeleccionado(this.itemSeleccionado);
//            }
//        }

//        public Func<string, List<Servicios.DTO.ResultadoBusqueda<TEntidad>>> ServicioBuscar { get; set; }

//        FrameworkElement vistaBuscador;
//        public FrameworkElement VistaBuscador
//        {
//            get
//            {
//                return vistaBuscador;
//            }
//            set
//            {
//                vistaBuscador = value;
//                vistaBuscador.DataContext = this;


//            }
//        }

//        public ControladorArbol()
//        {
//            this.Items = new System.Collections.ObjectModel.ObservableCollection<TEntidad>();

//        }

//        public void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
//        {
//            this.ItemSeleccionado = e.NewValue as TEntidad;
//        }

//    }
//}
