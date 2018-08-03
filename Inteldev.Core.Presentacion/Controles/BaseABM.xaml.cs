using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Threading;
namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para BaseABM.xaml
    /// </summary>
    /// 
    public partial class BaseABM : Window, IBaseABM
    {

        public Menu MenuSuperior
        {
            get { return menu1; }
            set { menu1 = value; }
        }

        public ToolBarPanel ToolBarDerecho
        {
            get { return toolbarDerecho; }
            set { toolbarDerecho = value; }
        }

        public TabControl TabControlDerecho
        {
            get { return tabControlDerecho; }
            set { tabControlDerecho = value; }
        }

        public ContentControl PanelIzquierdo
        {
            get { return tabControlIzquierdo; }
            set { tabControlIzquierdo = value; }
        }

        public object Contexto
        {
            get { return this.DataContext; }
            set { this.DataContext = value; }
        }

        public BaseABM()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {


            //if (e.Key == Key.Return)
            //{

            //    var txt = e.Source as TextBox;

            //    if (txt != null )
            //    {

            //        bool llOk=false;
            //        foreach (KeyBinding item in txt.InputBindings)
            //        {
            //            if (item.Key == Key.Enter)
            //                llOk = true;
            //        }

            //        if (!llOk)
            //        {
            //             focusDirection = FocusNavigationDirection.Next;

            //            TraversalRequest request = new TraversalRequest(focusDirection);

            //            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            //            if (elementWithFocus != null && elementWithFocus is TextBox)
            //                elementWithFocus.MoveFocus(request);


            //            e.Handled = true;
            //        }
            //    }
            //}
        }
    }
}
