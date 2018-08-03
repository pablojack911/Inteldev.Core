using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Lógica de interacción para ItemFormulario.xaml
    /// </summary>
    public partial class ItemFormulario : Grid
    {
        public ItemFormulario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Propiedad que representa al Label del control
        /// </summary>
        public string Etiqueta
        {
            get { return (string)GetValue(EtiquetaProperty); }
            set { SetValue(EtiquetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Etiqueta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaProperty =
            DependencyProperty.Register("Etiqueta", typeof(string), typeof(ItemFormulario));

        /// <summary>
        /// Propiedad que representa el valor actual del control
        /// </summary>
        public object Valor
        {
            get { return (string)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(object), typeof(ItemFormulario));

        /// <summary>
        /// Propiedad que se utiliza para manipular la propiedad IsEnabled del control
        /// </summary>
        public bool Habilitado
        {
            get { return (bool)GetValue(HabilitadoProperty); }
            set { SetValue(HabilitadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Habilitado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HabilitadoProperty =
            DependencyProperty.Register("Habilitado", typeof(bool), typeof(ItemFormulario), new PropertyMetadata(true));

        /// <summary>
        /// Propiedad que indica el tipo de entrada posible para el control
        /// </summary>
        public TipoDeEntrada FiltroEntrada
        {
            get { return (TipoDeEntrada)GetValue(FiltroEntradaProperty); }
            set { SetValue(FiltroEntradaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FiltroEntrada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FiltroEntradaProperty =
            DependencyProperty.Register("FiltroEntrada", typeof(TipoDeEntrada), typeof(ItemFormulario), new PropertyMetadata(TipoDeEntrada.Alfanumerico));


        /// <summary>
        /// Propiedad que asigna el TextAlignment del control. Por defecto, Left
        /// </summary>
        public TextAlignment OrientacionDelTextBox
        {
            get { return (TextAlignment)GetValue(OrientacionDelTextBoxProperty); }
            set { SetValue(OrientacionDelTextBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrientacionDelTextBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientacionDelTextBoxProperty =
            DependencyProperty.Register("OrientacionDelTextBox", typeof(TextAlignment), typeof(ItemFormulario), new PropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// Campo que se asocia con la vista para bindear el cursor 
        /// </summary>
        public IInputElement Campo
        {
            get { return this.txtCampo; }
        }

        /// <summary>
        /// Cantidad maxima de caracteres que acepta el Control 
        /// </summary>
        public int TamañoMaximo
        {
            get { return (int)GetValue(TamañoMaximoProperty); }
            set { SetValue(TamañoMaximoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TamañoMaximo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TamañoMaximoProperty =
            DependencyProperty.Register("TamañoMaximo", typeof(int), typeof(ItemFormulario), new PropertyMetadata(0));

        /// <summary>
        /// Valor maximo en numeros que acepta el control
        /// </summary>
        public decimal ValorNumericoMaximo
        {
            get { return (decimal)GetValue(ValorNumericoMaximoProperty); }
            set { SetValue(ValorNumericoMaximoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValorNumericoMaximo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorNumericoMaximoProperty =
            DependencyProperty.Register("ValorNumericoMaximo", typeof(decimal), typeof(ItemFormulario), new PropertyMetadata(decimal.MaxValue));


        /// <summary>
        /// Propiedad utilizada para formatear las vistas de los controles que contengan decimales
        /// </summary>
        public int CantidadDeDecimales
        {
            get { return (int)GetValue(CantidadDeDecimalesProperty); }
            set { SetValue(CantidadDeDecimalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CantidadDeDecimales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CantidadDeDecimalesProperty =
            DependencyProperty.Register("CantidadDeDecimales", typeof(int), typeof(ItemFormulario), new PropertyMetadata(4));



        public string FormatoDeString
        {
            get { return (string)GetValue(FormatoDeStringProperty); }
            set { SetValue(FormatoDeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatoDeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatoDeStringProperty =
            DependencyProperty.Register("FormatoDeString", typeof(string), typeof(ItemFormulario), new PropertyMetadata(string.Empty));


        private void txtCampo_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool ok = false;
            string entrada = e.Text;
            decimal dec = 0;

            switch (this.FiltroEntrada)
            {
                case TipoDeEntrada.Alfabetico:
                    ok = !decimal.TryParse(entrada, out dec);
                    break;
                case TipoDeEntrada.Alfanumerico:
                    ok = true;
                    break;
                case TipoDeEntrada.NumericoEntero:
                    decimal ent = 0;
                    ok = decimal.TryParse(entrada, out ent);
                    break;
                case TipoDeEntrada.NumericoDecimal:
                    var valEnString = this.txtCampo.Text;
                    ok = (entrada == "." && !valEnString.Contains('.')) || decimal.TryParse(entrada, out dec);
                    break;
                default:
                    break;
            }

            if (ok)
                ok = this.TamañoMaximo == 0 || txtCampo.Text.Length < this.TamañoMaximo;

            //if (ok)
            //    ok = this.ValorNumericoMaximo

            e.Handled = !ok;
        }

        //private void txtCampo_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        //{
        //    if (this.FiltroEntrada == TipoDeEntrada.NumericoDecimal)
        //    {
        //        var sb = new StringBuilder();
        //        sb.Append("0.");
        //        sb.Append("0".PadLeft(this.CantidadDeDecimales, '0'));
        //        var formatoSalida = sb.ToString();
        //        decimal value = 0M;
        //        if (decimal.TryParse(txtCampo.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        //        {
        //            txtCampo.Text = value.ToString(formatoSalida);
        //        }
        //    }
        //}

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var bindingTextDelTextbox = this.txtCampo.GetBindingExpression(TextBox.TextProperty);
        //    if (this.FormatoDeString == "DECIMAL")
        //    {
        //        //var nuevoBinding = bindingTextDelTextbox;

        //        //var nuevoBinding = new Binding();
        //        //foreach (var item in typeof(Binding).GetProperties())
        //        //{
        //        //    if (item.Name == "StringFormat")
        //        //        typeof(Binding).GetProperty(item.Name).SetValue(nuevoBinding, "{}{0.000}", null);
        //        //    else
        //        //    {
        //        //        var valorPropiedadBTB = typeof(Binding).GetProperty(item.Name).GetValue(bindingTextDelTextbox, null);
        //        //        typeof(Binding).GetProperty(item.Name).SetValue(nuevoBinding, valorPropiedadBTB, null);
        //        //    }
        //        //}
        //        //bindingTextDelTextbox.ParentBinding.StringFormat = "{}{0.000}";
        //        //this.txtCampo.SetBinding(TextBox.TextProperty, nuevoBinding);
        //    }
        //}
    }

    /// <summary>
    /// Manipulador de entrada de informacion
    /// </summary>
    public enum TipoDeEntrada
    {
        /// <summary>Solo letras</summary>
        Alfabetico,
        /// <summary>Letras y numeros</summary>
        Alfanumerico,
        /// <summary>Solo numeros</summary>
        NumericoEntero,
        /// <summary>Numeros y punto</summary>
        NumericoDecimal
    }
}
