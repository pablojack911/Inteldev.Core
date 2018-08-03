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

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para Telefono.xaml
    /// </summary>
    public partial class Telefono : Grid
    {
		public ushort CodigoDeArea
		{
			get { return (ushort)GetValue(CodigoDeAreaProperty); }
			set { SetValue(CodigoDeAreaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CodigoDeArea.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CodigoDeAreaProperty =
			DependencyProperty.Register("CodigoDeArea", typeof(ushort), typeof(Telefono));



		public ushort Prefijo
		{
			get { return (ushort)GetValue(PrefijoProperty); }
			set { SetValue(PrefijoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Prefijo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PrefijoProperty =
			DependencyProperty.Register("Prefijo", typeof(ushort), typeof(Telefono));



		public uint Numero
		{
			get { return (uint)GetValue(NumeroProperty); }
			set { SetValue(NumeroProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Numero.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NumeroProperty =
			DependencyProperty.Register("Numero", typeof(uint), typeof(Telefono));


        public Telefono()
        {
            InitializeComponent();
        }
    }
}
