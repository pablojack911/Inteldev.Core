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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Core.Presentacion.Vistas
{
    /// <summary>
    /// Interaction logic for VistaEmpresa.xaml
    /// </summary>
    public partial class VistaEmpresa : UserControl
    {
        /// <summary>
        /// Constructor de la vista
        /// </summary>
        public VistaEmpresa()
        {
            InitializeComponent();
            string[] excluyentes = { "ConsumidorFinal" };
            this.comboBoxCondIva.Excluir = excluyentes;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
