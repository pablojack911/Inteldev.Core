using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Contratos;

namespace Inteldev.Core.Presentacion.Controladores
{
	/// <summary>
	/// Carga los elementos iniciales del sistema. Esto es login, controladores, etc. 
	/// </summary>
    public class Sistema : DependencyObject
    {
        private static Sistema instancia;

        public static Sistema Instancia
        {
            get
            {
                return instancia;
            }
        }

        public static void Comenzar()
        {
            instancia = new Sistema();
        }

        public string Nombre { get; set; }
        
		public string Icono { get; set; }
        
		public ObservableCollection<OpcionMenu> MenuPrincipal { get; set; }
        
		public Empresa  EmpresaActual
         {
            get
            {
                return (Empresa)this.GetValue(EmpresaActualProperty);
            }
            set
            {
                SetValue(EmpresaActualProperty, value);
            }
        }
        
		public Sucursal SucursalActual 
         {
            get
            {
                return (Sucursal)this.GetValue(SucursalActualProperty);
            }
            set
            {
                SetValue(SucursalActualProperty, value);
            }
        }
        
		public Usuario UsuarioActual
        {
            get
            {
                
                return (Usuario)this.GetValue(UsuarioActualProperty);
            }
            set
            {
                SetValue(UsuarioActualProperty, value);
            }
        }
        
		public ControladorLogin ControladorLogin { get; set; }
        
		public ControladorSeleccionEmpresa SeleccionEmpresa { get; set; }
        
		public ControladorMenu ControladorMenu { get; set; }

        public static readonly DependencyProperty EmpresaActualProperty = DependencyProperty.Register("EmpresaActual", typeof(Empresa), typeof(Sistema));
        
		public static readonly DependencyProperty SucursalActualProperty = DependencyProperty.Register("SucursalActual", typeof(Sucursal), typeof(Sistema));
        
		public static readonly DependencyProperty UsuarioActualProperty = DependencyProperty.Register("UsuarioActual", typeof(Usuario), typeof(Sistema));

        private ICommand  cmdCambiarUsuario;

        public ICommand  CmdCambiarUsuario
        {
            get { return cmdCambiarUsuario; }
            set { cmdCambiarUsuario = value; }
        }

        private ICommand cmdCambiarEmpresa;

        public ICommand CmdCambiarEmpresa
        {
            get { return cmdCambiarEmpresa; }
            set { cmdCambiarEmpresa = value; }
        }

        private Window MainWindow;

		//constructor
        public Sistema()
        {            
            this.ControladorLogin = new ControladorLogin();
            this.SeleccionEmpresa = new ControladorSeleccionEmpresa();
            this.ControladorMenu = new ControladorMenu();
            this.MainWindow = Application.Current.MainWindow;            
            this.MainWindow.Initialized += new EventHandler(MainWindow_Initialized);
            this.MenuPrincipal = new ObservableCollection<OpcionMenu>();
            this.CmdCambiarUsuario = new RelayCommand(c => TryCatch.Intentar(n => Login()));
            this.CmdCambiarEmpresa = new RelayCommand(c => TryCatch.Intentar(n => CambiarEmpresa()));
            this.MainWindow.DataContext = this;
        }

        
        

        void MainWindow_Initialized(object sender, EventArgs e)
        {
            this.Login();                     
        }

        public event EventHandler<System.EventArgs> cambioMenu;

        void Login()
        {
            this.ControladorLogin.Ejecutar();
            if (this.ControladorLogin.LoginOk)
            {
                this.UsuarioActual = this.ControladorLogin.UsuarioActual;
                this.EmpresaActual = this.ControladorLogin.EmpresaActual;
                //aca tengo que cambiar el menu. De alguna forma...
                //Sistema.Instancia.ControladorMenu.LimpiarOpciones();
                //this.MenuPrincipal.Clear();
                if (this.cambioMenu != null)
                    this.cambioMenu(this, null);
               // Sistema.CargarMenu();
                //this.CambiarEmpresa();
            }
            else
                this.MainWindow.Close();
        }

        private void CambiarEmpresa()
        {
            this.SeleccionEmpresa.Ejecutar();
            this.EmpresaActual = this.SeleccionEmpresa.EmpresaActual;
            this.SucursalActual = this.SeleccionEmpresa.SucursalActual;
        }


        public static void CargarMenu()
        {
			var menu = Sistema.Instancia.ControladorMenu.Cargar(Instancia.UsuarioActual, Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual);
            
            foreach (var m in menu)
            {                
                Instancia.MenuPrincipal.Add(m);
            }            
        }
        
    }
}
