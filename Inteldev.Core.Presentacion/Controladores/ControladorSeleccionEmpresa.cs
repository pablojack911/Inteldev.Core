using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.DTO.Organizacion;
using System;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Contratos;

namespace Inteldev.Core.Presentacion.Controladores
{
    public class ControladorSeleccionEmpresa : DependencyObject
    {
        public static readonly DependencyProperty EmpresasProperty = DependencyProperty.Register("Empresas",
                                                                                                typeof(ObservableCollection<Empresa>),
                                                                                                typeof(ControladorSeleccionEmpresa));
        public static readonly DependencyProperty SucursalesProperty = DependencyProperty.Register("Sucursales", 
                                                                                                typeof(ObservableCollection<Sucursal>), 
                                                                                                typeof(ControladorSeleccionEmpresa));

        public ObservableCollection<Empresa> Empresas
        {
            get
            {
                return (ObservableCollection<Empresa>)this.GetValue(EmpresasProperty);
            }
            set
            {
                this.SetValue(EmpresasProperty, value);
            }
        }
        public ObservableCollection<Sucursal> Sucursales
        {
            get
            {
                return (ObservableCollection<Sucursal>)this.GetValue(SucursalesProperty);
            }
            set
            {
                this.SetValue(SucursalesProperty, value);
            }
        }

        private ICommand cmdAceptar;
        public ICommand CmdAceptar
        {
            get { return cmdAceptar; }
            set { cmdAceptar = value; }
        }

        private ICommand cmdCancelar;
        public ICommand CmdCancelar
        {
            get { return cmdCancelar; }
            set { cmdCancelar = value; }
        }

        public Empresa  EmpresaActual { get; set; }
        public Sucursal SucursalActual { get; set; }

        public Vistas.VistaCambiaEmpresa vista;

        Window CambiarEmpresaWindow;
        public void Ejecutar()
        {
            //aca es a quien llama cuando queres cambiar
            if (Application.Current.Windows.Count == 1)
            {
                this.obtenerEmpresas();
                this.obtenerSucursales();
                this.vista = new Vistas.VistaCambiaEmpresa();
                this.vista.DataContext = this;
                this.cmdCancelar = new RelayCommand(m => TryCatch.Intentar(n => this.Cancelar()));
                this.cmdAceptar = new RelayCommand(m => TryCatch.Intentar(n => this.CambiarEmpresa((Empresa)this.vista.cboEmpresa.SelectedItem, (Sucursal)this.vista.cboSucursal.SelectedItem)));
                CambiarEmpresaWindow = new Window()
                {
                    Content = this.vista,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                    Height = 300,
                    Width = 450,
                    WindowStyle = System.Windows.WindowStyle.None,
                };
                if (Application.Current.MainWindow.IsVisible)
                {
                    CambiarEmpresaWindow.Owner = Application.Current.MainWindow;
                    CambiarEmpresaWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                }
                CambiarEmpresaWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Podes Cambiar de unidad de negocio si tenes mas de una ventana abierta");
            }
        }

		public event EventHandler<System.EventArgs> cambioMenu;

        public void CambiarEmpresa(Empresa empresa, Sucursal sucursal)
        {
            this.EmpresaActual = empresa;
            this.SucursalActual = sucursal;
			Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual = (UnidadeDeNegocio)this.vista.comboUnidad.SelectedItem;
			Sistema.Instancia.ControladorLogin.UsuarioActual.UnidadDeNegocioActual = (UnidadeDeNegocio)this.vista.comboUnidad.SelectedItem;
			Sistema.Instancia.ControladorLogin.UsuarioActual.EmpresaActual = this.EmpresaActual;
			//aca tiene que actualizar el menu de alguna forma...
			this.cambioMenu(this, null);
            this.CambiarEmpresaWindow.Close();
        }

        private void Cancelar()
        {
            CambiarEmpresaWindow.Close();
        }

        private void obtenerEmpresas()
        { 
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Empresa>>();

            var listaEmpresa = servicio.ObtenerLista(1, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo);

            var empresas = new ObservableCollection<Empresa>(listaEmpresa);

            this.Empresas = empresas;            
        }

        private void obtenerSucursales()
        {
            var sucursales = new ObservableCollection<Sucursal>();
            sucursales.Add(new Sucursal(){Nombre="Casa Central"});
            sucursales.Add(new Sucursal(){Nombre="Pinamar"});
            this.Sucursales= sucursales;
        }
    }
}
