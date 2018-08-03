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

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Interaction logic for EntidadMaestro.xaml
    /// </summary>
    public partial class EntidadMaestro : UserControl
    {
        public EntidadMaestro()
        {
            InitializeComponent();
        }

        public IInputElement Campo
        {
            get { return this.txtNombre.Campo; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
