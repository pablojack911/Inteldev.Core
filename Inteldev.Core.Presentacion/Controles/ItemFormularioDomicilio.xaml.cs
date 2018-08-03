using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Collections;
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
    /// Lógica de interacción para ItemFormularioDomicilio.xaml
    /// </summary>
    public partial class ItemFormularioDomicilio : Grid
    {
		public IPresentadorDomicilio Presentador
		{
			get { return (IPresentadorDomicilio)GetValue(PresentadorProperty); }
			set { SetValue(PresentadorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProperty =
			DependencyProperty.Register("Presentador", typeof(IPresentadorDomicilio), typeof(ItemFormularioDomicilio));



        public Localidad Localidad
        {
            get { return (Localidad)GetValue(LocalidadProperty); }
            set { SetValue(LocalidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Localidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocalidadProperty =
            DependencyProperty.Register("Localidad", typeof(Localidad), typeof(ItemFormularioDomicilio));
        
        public Provincia Provincia
        {
            get { return (Provincia)GetValue(ProvinciaProperty); }
            set { SetValue(ProvinciaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Provincia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProvinciaProperty =
            DependencyProperty.Register("Provincia", typeof(Provincia), typeof(ItemFormularioDomicilio));


        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormularioDomicilio));
                

        public ItemFormularioDomicilio()
        {
			InitializeComponent();
            if (PresentadorMapa == null)
            {
                this.VisibilidadCoordenadas(System.Windows.Visibility.Hidden);
            }
            
        }
               
       

        public IPresentadorBaseDialogo<Domicilio> PresentadorMapa
        {
            get { return (PresentadorBaseDialogo<Domicilio>)GetValue(PresentadorMapaProperty); }
            set { SetValue(PresentadorMapaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMapa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMapaProperty =
            DependencyProperty.Register("PresentadorMapa", typeof(IPresentadorBaseDialogo<Domicilio>), typeof(ItemFormularioDomicilio));


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "PresentadorMapa")
            {
                System.Windows.Visibility visible = System.Windows.Visibility.Hidden;
                if (PresentadorMapa != null)
                {
                    visible = System.Windows.Visibility.Visible;
                    this.PresentadorMapa.DialogoCerrado += PresentadorMapa_DialogoCerrado;
                }
                this.VisibilidadCoordenadas(visible);
               
            }
        }

        
        void PresentadorMapa_DialogoCerrado(bool acepto)
        {
            if (acepto)
            {
                txtLatitud.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                txtLongitud.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        void VisibilidadCoordenadas(Visibility visible)
        {
            txbCoordenadas.Visibility = visible;
            txbLatitud.Visibility = visible;
            txbLongitud.Visibility = visible;
            txtLatitud.Visibility = visible;
            txtLongitud.Visibility = visible;
            btnMapa.Visibility = visible;
        }
        
    }
}
