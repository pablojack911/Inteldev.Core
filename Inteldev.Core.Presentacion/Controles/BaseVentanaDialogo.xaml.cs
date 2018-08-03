using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para Buscador.xaml
    /// </summary>
    public partial class BaseVentanaDialogo : Window
    {

        public ContentControl VistaPrincipal { get { return this.vistaPrincipal; } set { this.vistaPrincipal = value; } }

        public BaseVentanaDialogo()
        {
            InitializeComponent();
            this.ContentRendered += BaseVentanaDialogo_Activated;
        }

        void BaseVentanaDialogo_Activated(object sender, System.EventArgs e)
        {
            var contenido = (IInputElement)this.VistaPrincipal.Content;
            if (contenido != null)
                Keyboard.Focus(contenido);

            var focusDirection = FocusNavigationDirection.Next;
            TraversalRequest request = new TraversalRequest(focusDirection);
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
            if (elementWithFocus != null)
                elementWithFocus.MoveFocus(request);
        }
    }
}
