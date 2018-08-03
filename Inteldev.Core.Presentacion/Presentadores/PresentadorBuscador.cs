using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Controles;
using System.Collections.ObjectModel;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Contratos;
using Inteldev.Core.Patrones;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System.ServiceModel;

namespace Inteldev.Core.Presentacion.Presentadores
{
    /// <summary>
    /// Usa el servicio del buscador para realizar una busqueda y guardarla en las propiedades que tiene el buscador, 
    /// para que luego se puedan acceder.
    /// </summary>
    /// <seealso cref="ResultadoBusqueda"/>
    public class PresentadorBuscador<TEntidad> : DependencyObject, IPresentadorBuscador<TEntidad>
        where TEntidad : DTOMaestro, new()
    {

        #region Comandos

        public Func<List<ParametrosMiniBusca>> ObtenerParametros { get; set; }

        private RelayCommand cmdBuscar;
        public ICommand CmdBuscar
        {
            get
            {
                if (cmdBuscar == null)
                    cmdBuscar = new RelayCommand(p => this.ObtenerResultados(p.ToString()), p => this.PuedeBuscar(p));
                return cmdBuscar;
            }
        }



        public List<string> ListaOmitidos { get; set; }



        private bool PuedeBuscar(object p)
        {
            int numero = -1;
            if (p != null)
            {
                int.TryParse(p.ToString(), out numero);
                if ((p.ToString().Length >= 2) || numero > 0)
                    return true;
            }
            return false;
        }


        public ICommand CmdSeleccionarResultado { get; set; }
        public ICommand CmdSeleccionarItem { get; set; }

        #endregion

        #region Propiedades Protegidas

        /// <summary>
        /// Representa la entidad que se selecciono
        /// </summary>
        protected DTOMaestro itemSeleccionado;

        /// <summary>
        /// Representa la lista de resultados que se seleccionaron
        /// </summary>
        protected ResultadoBusqueda<TEntidad> resultadoSeleccionado;

        #endregion

        #region Propiedades Publicas

        public ObservableCollection<ResultadoBusqueda<TEntidad>> Resultados { get; set; }

        public delegate void CambioItemEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Evento que se dispara cuando se selecciona una entidad en la vista de detalles.
        /// </summary>
        public event CambioItemEventHandler CambioItem;

        public IServicioABM<TEntidad> Servicio { get; set; }

        public ObservableCollection<TEntidad> Items { get; set; }

        public Func<string, ListaParametrosDeBusqueda, List<ResultadoBusqueda<TEntidad>>> ServicioBuscar { get; set; }
        public DTOMaestro ItemSeleccionado
        {
            get { return itemSeleccionado; }
            set
            {
                itemSeleccionado = value;
            }
        }

        public ResultadoBusqueda<TEntidad> ResultadoSeleccionado
        {
            get { return resultadoSeleccionado; }
            set
            {
                resultadoSeleccionado = value;
                Items.Clear();
                if (resultadoSeleccionado != null)
                    resultadoSeleccionado.Lista.ToList().ForEach(p => Items.Add(p));
            }
        }

        public Action<TEntidad> ActualizarDto { get; set; }

        #endregion

        #region Dependecy Properties

        public string textoBusqueda
        {
            get { return (string)GetValue(textoBusquedaProperty); }
            set { SetValue(textoBusquedaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for txtBusqueda.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textoBusquedaProperty =
            DependencyProperty.Register("textoBusqueda", typeof(string), typeof(PresentadorBuscador<TEntidad>));

        public TEntidad Entidad
        {
            get { return (TEntidad)GetValue(EntidadProperty); }
            set { SetValue(EntidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Entidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntidadProperty =
            DependencyProperty.Register("Entidad", typeof(TEntidad), typeof(PresentadorBuscador<TEntidad>));

        #endregion

        #region Constructor

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "Entidad")
            {
                if (ActualizarDto != null)
                    ActualizarDto(e.NewValue as TEntidad);
            }
        }


        public PresentadorBuscador()
        {
            this.Items = new ObservableCollection<TEntidad>();
            this.Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(items_CollectionChanged);
            this.Resultados = new ObservableCollection<ResultadoBusqueda<TEntidad>>();
            this.Resultados.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(resultados_CollectionChanged);
            this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
            ServicioBuscar = ((p, q) => Servicio.ObtenerResultados(p.ToString(), Sistema.Instancia.EmpresaActual.Codigo, q));
            this.CmdSeleccionarResultado = new RelayCommand(m => this.Muestra(), p => this.PuedeSeleccionarResultado());
            this.CmdSeleccionarItem = new RelayCommand(m => this.Muestra(), p => this.PuedeSeleccionarResultado());
            this.ListaOmitidos = new List<string>();
        }

        private object Muestra()
        {
            return true;
        }

        private bool PuedeSeleccionarResultado()
        {
            //return this.ItemSeleccionado != null;
            return true;
        }

        void resultados_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        void items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        #endregion

        #region Implementacion Comandos

        /// <summary>
        /// Busca el parametro en todos los campos de la entidad y los guarda en Resultados.
        /// Usa servicioBuscar para realizar la busqueda.
        /// </summary>
        /// <param name="param">string de busqueda</param>
        public bool ObtenerResultados(string param)
        {
            if (param != null)
            {
                var controlador = this;
                controlador.Items.Clear();
                controlador.Resultados.Clear();
                ListaParametrosDeBusqueda parametros = new ListaParametrosDeBusqueda();
                if (ObtenerParametros != null)
                {
                    parametros.Parametros = ObtenerParametros();
                }
                //aca pasar parametros

                try
                {
                    var resultados = this.ServicioBuscar(param, parametros);
                    if (resultados.Any())
                    {
                        resultados.ForEach(r => this.Resultados.Add(r));
                    }
                }
                catch (CommunicationException ex)
                {
                    Mensajes.Aviso("Demasiados resultados para '" + param + "'. Sea mas específico.");
                    this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
                }
            }
            return true;
        }

        #endregion

    }
}
