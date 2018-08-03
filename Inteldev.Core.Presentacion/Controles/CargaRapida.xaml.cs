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
    /// Interaction logic for CargarRapida.xaml
    /// </summary>
    public partial class CargaRapida : Grid
    {
        public CargaRapida()
        {
            InitializeComponent();

        }
        
        public FrameworkElement Cabecera
        {
            get { return (FrameworkElement)GetValue(CabeceraProperty); }
            set { SetValue(CabeceraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cabecera.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CabeceraProperty =
            DependencyProperty.Register("Cabecera", typeof(FrameworkElement), typeof(CargaRapida));
        
        public FrameworkElement Cuerpo
        {
            get { return (FrameworkElement)GetValue(CuerpoProperty); }
            set { SetValue(CuerpoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cuerpo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CuerpoProperty =
            DependencyProperty.Register("Cuerpo", typeof(FrameworkElement), typeof(CargaRapida));
        
        public FrameworkElement Pie
        {
            get { return (FrameworkElement)GetValue(PieProperty); }
            set { SetValue(PieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PieProperty =
            DependencyProperty.Register("Pie", typeof(FrameworkElement), typeof(CargaRapida));
        
        public Visibility CuerpoVisible
        {
            get { return (Visibility)GetValue(CuerpoVisibleProperty); }
            set { SetValue(CuerpoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CuerpoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CuerpoVisibleProperty =
            DependencyProperty.Register("CuerpoVisible", typeof(Visibility), typeof(CargaRapida));
                
        public Visibility PieVisible
        {
            get { return (Visibility)GetValue(PieVisibleProperty); }
            set { SetValue(PieVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PieVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PieVisibleProperty =
            DependencyProperty.Register("PieVisible", typeof(Visibility), typeof(CargaRapida));

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    if (e.Property == CuerpoProperty && e.NewValue == null)
        //        this.CuerpoVisible = System.Windows.Visibility.Collapsed;

        //    if (e.Property == PieProperty && e.NewValue == null)
        //        this.PieVisible = System.Windows.Visibility.Collapsed;

        //    base.OnPropertyChanged(e);
        //}
    }
}
