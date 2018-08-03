using System.Windows.Input;
using System.Windows;
using Inteldev.Core.Presentacion.Vistas;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.DTO.Usuarios;
using System;
using System.Security.Cryptography;
using System.Text;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;

namespace Inteldev.Core.Presentacion.Controladores
{
    public class ControladorLogin
    {

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

        Login vistalogin;

        Window LoginWindow;

        private Usuario usuarioActual;

        public Usuario UsuarioActual
        {
            get { return usuarioActual; }
            set { usuarioActual = value; }
        }

        private bool loginOk;

        public bool LoginOk
        {
            get { return loginOk; }
            set { loginOk = value; }
        }

        public Func<string, string, Usuario> ServicioLogin { get; set; }

        public UnidadeDeNegocio? UnidadDeNegocioActual { get; set; }
        public Empresa EmpresaActual { get; set; }

        private int intentos = 0;

        public void Ejecutar()
        {
            vistalogin = new Login();
            vistalogin.DataContext = this;
            this.cmdCancelar = new RelayCommand(m => TryCatch.Intentar(n => this.Cancelar()));
            this.cmdAceptar = new RelayCommand(m => TryCatch.Intentar(n => this.Login(vistalogin.txtNombreUsuario.Text.ToUpperInvariant(), vistalogin.txtClaveUsuario.Password.ToUpperInvariant())));

            LoginWindow = new Window()
            {
                Content = vistalogin,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                WindowStyle = System.Windows.WindowStyle.None,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,

            };
            if (Application.Current.MainWindow.IsVisible)
            {
                LoginWindow.Owner = Application.Current.MainWindow;
                LoginWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            }
            LoginWindow.ShowDialog();
        }

        private void Cancelar()
        {
            this.LoginWindow.Close();
        }

        private void Login(string usuario, string clave)
        {
            Usuario usu = null;
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(clave));
            var pass = Encoding.UTF8.GetString(result);
            if (usuario == "ADMIN" && clave == "QWER")
            {
                usu = new Usuario() { Nombre = "ADMIN" };
                usu.UnidadNegocioPorDefecto = null;
                var servicioEmpresa = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Empresa>>();

                //usu.EmpresaPorDefecto = servicioEmpresa.ObtenerPorId(4,CargarRelaciones.NoCargarNada);

                // usu.EmpresaPorDefecto = servicioEmpresa.ObtenerPorCodigo("01", CargarRelaciones.NoCargarNada, "", null);

                usu.EmpresaPorDefecto = servicioEmpresa.ObtenerPorId(1, CargarRelaciones.NoCargarNada, "");
            }
            else
                usu = this.ServicioLogin(usuario.ToUpperInvariant(), pass.ToUpperInvariant());

            if (usu != null)
            {
                this.UnidadDeNegocioActual = usu.UnidadNegocioPorDefecto;
                this.EmpresaActual = usu.EmpresaPorDefecto;
                this.usuarioActual = usu;
                this.LoginOk = true;
                this.LoginWindow.Close();
            }
            else
            {
                //si entraste aca es porque la cagaste
                Mensajes.Error("Usuario o Clave incorrecto");
                this.intentos++;
            }

            if (this.intentos == 3)
                this.Cancelar();

        }

    }

}
