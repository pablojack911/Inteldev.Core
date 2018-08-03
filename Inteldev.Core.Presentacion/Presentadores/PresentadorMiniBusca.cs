using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Inteldev.Core.Patrones;
using Inteldev.Core.Contratos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.ClienteServicios;

namespace Inteldev.Core.Presentacion.Presentadores
{
    /// <summary>
    /// Presentador del Mini Buscador. 
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de entidad</typeparam>
    public class PresentadorMiniBusca<TEntidad> : PresentadorBuscador<TEntidad>, IPresentadorMiniBusca<TEntidad>
        where TEntidad : DTOMaestro, new()
    {
        #region Constructor

        public List<ParametrosMiniBusca> Parametros
        {
            get { return (List<ParametrosMiniBusca>)GetValue(ParametrosProperty); }
            set { SetValue(ParametrosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Parametros.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParametrosProperty =
            DependencyProperty.Register("Parametros", typeof(List<ParametrosMiniBusca>), typeof(PresentadorMiniBusca<TEntidad>));

        public int cantidadNumeros { get; set; }

        public CargarRelaciones CargaRelaciones { get; set; }


        public PresentadorMiniBusca()
            : base()
        {
            this.CmdAceptar = new RelayCommand(p => this.Aceptar(), p => this.PuedeAceptar());
            this.CmdCancelar = new RelayCommand(p => this.Cancelar(), P => this.PuedeCancelar());
            this.CmdVerBuscador = new RelayCommand(p => this.Buscar(), p => this.PuedeBuscar());
            this.CmdBuscarPorId = new RelayCommand(p => this.BuscarPorId(p), p => this.PuedeBuscarPorId(p));
            this.CmdSeleccionarItem = new RelayCommand(p => this.Aceptar(), p => this.PuedeAceptar());
            this.CargaRelaciones = CargarRelaciones.CargarEntidades;
        }

        private object BuscarConParametros()
        {
            throw new NotImplementedException();
        }

        public void SeleccionarEntidad(TEntidad entidad)
        {
            if (entidad == null && this.Entidad == null)
                this.Entidad = new TEntidad();
            else
            {
                if (entidad != null)
                {
                    var parametros = new ListaParametrosDeBusqueda();
                    if (ObtenerParametros != null)
                        parametros.Parametros = ObtenerParametros();
                    this.Entidad = this.Servicio.ObtenerPorCodigo(entidad.Codigo, this.CargaRelaciones, Sistema.Instancia.EmpresaActual.Codigo, parametros);
                }
                else
                    this.Entidad = new TEntidad();
            }

            //this.Entidad = entidad;
            if (this.Entidad != null && this.CambioEntidad != null)
            {

                //este lo usa el presentador maestro detalle. NO LO BORRES                 
                var args = new ArgumentoGenerico<TEntidad>(this.Entidad);
                this.CambioEntidad(this, args);
            }
        }


        public bool PuedeCancelar()
        {
            return true;
        }

        public object Cancelar()
        {
            this.itemSeleccionado = null;
            this.Ventana.Close();
            return true;
        }

        public bool PuedeBuscarPorId(object p)
        {
            return p.ToString().Length > 0;
        }

        public virtual object BuscarPorId(object p)
        {
            try
            {
                var parametros = new ListaParametrosDeBusqueda();
                if (ObtenerParametros != null)
                    parametros.Parametros = ObtenerParametros();
                var ent = this.Servicio.ObtenerPorCodigo(p.ToString().PadLeft(this.cantidadNumeros, '0'), CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo, parametros);
                this.SeleccionarEntidad(ent);
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
                this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
            }
            return true;
        }

        public virtual bool PuedeBuscar()
        {
            return true;
        }


        public object Buscar()
        {
            this.Ventana = new BaseVentanaDialogo();
            //this.Ventana.MinHeight = 480;
            //this.Ventana.MinWidth = 480;
            var buscador = new BuscadorInicial();
            //buscador.MinHeight = this.Ventana.MinHeight;
            //buscador.MinWidth = this.Ventana.MinWidth;
            this.Ventana.vistaPrincipal.Content = buscador;
            this.Ventana.SizeToContent = SizeToContent.WidthAndHeight;
            this.Resultados.Clear();
            this.textoBusqueda = string.Empty;
            this.Ventana.DataContext = this;
            //this.Ventana.DataContext = new PresentadorMiniBusca<TEntidad>();
            this.Ventana.ShowDialog();
            return true;
        }

        public bool PuedeAceptar()
        {
            return this.itemSeleccionado != null;
        }

        public object Aceptar()
        {
            this.Ventana.Close();
            this.SeleccionarEntidad((TEntidad)this.itemSeleccionado);
            return itemSeleccionado;
        }

        #endregion

        #region Comandos

        public ICommand CmdAceptar { get; set; }
        public ICommand CmdCancelar { get; set; }

        public ICommand CmdBuscarPorId { get; set; }
        public ICommand CmdVerBuscador { get; set; }


        #endregion

        #region Campos

        private BaseVentanaDialogo Ventana;

        #endregion

        public event EventHandler<ArgumentoGenerico<TEntidad>> CambioEntidad;

        public void Reset()
        {
            this.Entidad = null;
        }


    }
}
