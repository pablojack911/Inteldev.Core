using System.Windows.Controls;

namespace Inteldev.Core.Presentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Grid
    {
        public Login()
        {
            InitializeComponent();
            txtNombreUsuario.Focus();
        }

        private void txtNombreUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                txtClaveUsuario.Focus();
        }

        private void txtClaveUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                btnIngresar.Command.Execute(null);
        }

    }
}
