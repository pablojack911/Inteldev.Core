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
using System.Windows.Shapes;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Interaction logic for BaseVentanaListado.xaml
    /// </summary>
    public partial class BaseVentanaListado : Window
    {

        public ContentControl Contenido
        {
            get { return this.contenido; }
            set { this.contenido = value; }
        }
        public ContentControl Filtros
        {
            get { return this.filtros; }
            set { this.filtros = value; }
        }


        public string Titulo
        {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Titulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloProperty =
            DependencyProperty.Register("Titulo", typeof(string), typeof(BaseVentanaListado));



        public BaseVentanaListado()
        {
            InitializeComponent();
            var contenido = (IInputElement)this.Filtros.Content;
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
