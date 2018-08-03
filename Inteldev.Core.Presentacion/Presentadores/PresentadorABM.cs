using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Inteldev.Core.Extenciones;
using System.Windows.Input;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.DTO.Validaciones;
using Inteldev.Core.DTO.Carriers;
using System.Windows.Controls;
using Inteldev.Core.Presentacion.VistasModelos;
using System.ServiceModel;

namespace Inteldev.Core.Presentacion.Presentadores
{
    public class PresentadorABM<TEntidad, TVistaModelo> : PresentadorBase<TEntidad>, Inteldev.Core.Presentacion.Presentadores.IPresentadorABM
        where TEntidad : DTOMaestro, new()
        where TVistaModelo : VistaModeloBase<TEntidad>, new()
    {
        #region Constructor

        public PresentadorABM()
        {
            this.Inicializador(FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>(),
                                 FabricaPresentadores._Resolver<IPresentadorBuscador<TEntidad>>());
        }

        public PresentadorABM(IServicioABM<TEntidad> servicio, IPresentadorBuscador buscador)
        {
            this.Inicializador(servicio, buscador);
        }

        public void Inicializador(IServicioABM<TEntidad> servicio, IPresentadorBuscador buscador)
        {
            this.EtiquetaBotonNuevo = "Nuevo";
            this.Servicio = servicio;
            this.Buscador = buscador;
            //this.VistaModeloListado = FabricaPresentadores.Instancia.Resolver<VistaModeloLista<TEntidad>>();

            this.Entidades = new System.Collections.ObjectModel.ObservableCollection<TEntidad>();
            this.EntidadesAbiertas = new ObservableCollection<TVistaModelo>();
            this.Configurar();
            if (Sistema.Instancia.EmpresaActual == null)
            {
                Sistema.Instancia.EmpresaActual = new DTO.Organizacion.Empresa() { Codigo = "" };
            }
            var parametros = new ListaParametrosDeBusqueda();
            this.Servicio.ObtenerPorCodigo("xxxxxxxxxxxxxxxxx", CargarRelaciones.NoCargarNada, "01", parametros);
        }

        #endregion

        public virtual void Configurar()
        {
            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Crear(Servicio.Crear(Sistema.Instancia.EmpresaActual.Codigo)), this.PuedeCrearNuevo()));
            this.CmdGrabar = new ComandoGrabar(i => this.Grabar(), i => this.PuedeGrabar());
            this.CmdVer = new RelayCommand(m => TryCatch.Intentar(i => this.Ver(this.Servicio.ObtenerPorId(this.Buscador.ItemSeleccionado.Id, CargarRelaciones.CargarTodo, Sistema.Instancia.EmpresaActual.Codigo)), true), m => this.PuedeVer());
            this.CmdEditar = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(this.Servicio.ObtenerPorId(this.Buscador.ItemSeleccionado.Id, CargarRelaciones.CargarTodo, Sistema.Instancia.EmpresaActual.Codigo)), true), m => this.PuedeEditar());
            this.CmdBorrar = new CommandoBorrar(m => this.Borrar(), m => this.PuedeBorrar());
            this.CmdClonar = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(this.EntidadActual.ClonarSinID<TEntidad>())), m => this.PuedeClonar());
            this.CmdCerrarPestaña = new RelayCommand(m => TryCatch.Intentar(i => this.Cancelar()), m => this.PuedeCancelar(m));
            this.CmdListar = new RelayCommand(m => TryCatch.Intentar(i => this.Listar()), m => this.PuedeListar());
            //this.CmdImprimir = new RelayCommand(m => TryCatch.Intentar(i => this.Imprimir(this.EntidadActual)), m => this.PuedeImprimir());

            this.VistaTemplate = FabricaVistas.Instancia.BuscaVista(typeof(TEntidad));

            var controlBuscador = new BuscadorInicial();

            //presentador Buscador para el buscador :)
            this.Buscador.CmdSeleccionarItem = this.CmdEditar;
            controlBuscador.DataContext = this.Buscador;
            controlBuscador.txtBusqueda.Focus();

            this.VistaABM = new BaseABM();
            this.VistaABM.PanelIzquierdo.Content = controlBuscador;
        }

        public virtual bool PuedeListar()
        {
            return true;
        }

        public void Listar()
        {
            var ventanaListado = new BaseVentanaListado();
            ventanaListado.Titulo = "Lista de " + typeof(TEntidad).Name.SplitCamelCase();
            var vistaListado = FabricaVistas.Instancia.BuscaVista(typeof(TEntidad), TipoVista.Listado);
            if (vistaListado == null)
                vistaListado = typeof(Listado);

            var datos = this.Servicio.ObtenerLista("***", Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
            var vmListado = new VistaModeloLista<TEntidad>(datos);

            ventanaListado.DataContext = vmListado;
            ventanaListado.Contenido.Content = Activator.CreateInstance(vistaListado);
            ventanaListado.Show();
        }

        //public virtual bool PuedeImprimir()
        //{
        //    return false;
        //}

        //public virtual object Imprimir(TEntidad tEntidad)
        //{
        //    throw new NotImplementedException();
        //}

        public object Crear(CreadorCarrier<TEntidad> creadorCarrier)
        {
            if (!creadorCarrier.GetError())
            {
                this.Editar(creadorCarrier.GetEntidad());
                return true;
            }
            else
            {
                Mensajes.Aviso(creadorCarrier.GetMensaje());
                return false;
            }
        }

        public object Grabar()
        {
            try
            {
                var result = this.Servicio.Grabar(this.EntidadActual, Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
                if (!result.getError())
                    this.Cancelar();
                //else
                //    Mensajes.Error(result.getMensaje()); //se encarga el trycatch en mostrar el error
                return result;
            }
            catch (EndpointNotFoundException) //Servidor Cerrado
            {
                Mensajes.Error("Servicio no disponible.\nPongase en contacto con Sistemas.");
                return 0;
            }
            catch (CommunicationException)
            {
                Mensajes.Error("Servicio ocupado.\nIntente en 5 segundos.");
                this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
                //var result = this.Servicio.Grabar(this.EntidadActual, Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
                //if (!result.getError())
                //    this.Cancelar();
                //else
                //    Mensajes.Error(result.getMensaje());
                //return result;
                return 0;
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
                this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
                return 0;
            }
        }

        public object Borrar()
        {
            try
            {
                var result = this.Servicio.Borrar(this.EntidadActual, Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
                if (result.borroOk)
                    this.Cancelar();
                return result;
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
                this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
                return 0;
            }
        }

        string titulo;
        public string Titulo
        {
            get
            {
                return this.titulo;
            }
            set
            {
                this.titulo = value;
                ((Window)this.VistaABM).Title = value;
            }
        }


        #region Comandos

        /// <summary>
        /// Define los comandos de los botones que estan en todo ABM
        /// </summary>
        public ICommand CmdNuevo { get; set; }
        public ICommand CmdEditar { get; set; }
        public ICommand CmdGrabar { get; set; }
        public ICommand CmdBorrar { get; set; }
        public ICommand CmdClonar { get; set; }
        public ICommand CmdVer { get; set; }
        public ICommand CmdCerrarPestaña { get; set; }
        public ICommand CmdListar { get; set; }

        //public ICommand CmdImprimir { get; set; }

        #endregion


        #region Campos
        protected IServicioABM<TEntidad> Servicio;
        IPresentadorBuscador Buscador;
        public Type VistaTemplate { get; set; }
        #endregion

        #region DP's

        public TVistaModelo VistaModeloActual
        {
            get { return (TVistaModelo)GetValue(VistaModeloActualProperty); }
            set { SetValue(VistaModeloActualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VistaModeloActual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VistaModeloActualProperty =
            DependencyProperty.Register("VistaModeloActual", typeof(TVistaModelo), typeof(PresentadorABM<TEntidad, TVistaModelo>));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            //if(e.Property.Name=="VisibilidadBotonCerrar")
            //{
            //     if(this.VistaModeloActual
            //}
            if (e.Property.Name == "VistaModeloActual")
            {
                var vistaModelo = (TVistaModelo)e.NewValue;
                if (vistaModelo == null)
                    this.EntidadActual = null;
                else
                    this.EntidadActual = vistaModelo.Modelo;
            }

        }

        public static DependencyProperty EtiquetaBotonNuevoProperty = DependencyProperty.Register("EtiquetaBotonNuevo", typeof(string), typeof(PresentadorABM<TEntidad, TVistaModelo>));

        public string EtiquetaBotonNuevo
        {
            get { return GetValue(EtiquetaBotonNuevoProperty).ToString(); }
            set { SetValue(EtiquetaBotonNuevoProperty, value); }
        }

        public ObservableCollection<TVistaModelo> EntidadesAbiertas
        {
            get { return (ObservableCollection<TVistaModelo>)GetValue(EntidadesAbiertasProperty); }
            set { SetValue(EntidadesAbiertasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EntidadesAbiertas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntidadesAbiertasProperty =
            DependencyProperty.Register("EntidadesAbiertas", typeof(ObservableCollection<TVistaModelo>), typeof(PresentadorABM<TEntidad, TVistaModelo>));



        //public Visibility VisibilidadBotonCerrar
        //{
        //    get { return (Visibility)GetValue(VisibilidadBotonCerrarProperty); }
        //    set { SetValue(VisibilidadBotonCerrarProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for VisibilidadBotonCerrar.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty VisibilidadBotonCerrarProperty =
        //    DependencyProperty.Register("VisibilidadBotonCerrar", typeof(Visibility), typeof(PresentadorABM<TEntidad, TVistaModelo>), new PropertyMetadata(Visibility.Visible));



        #endregion

        #region Metodos Publicos

        public void Ejecutar()
        {
            ((Window)VistaABM).Show();
            ((Window)VistaABM).Activate();
        }

        public void AbrirEntidad(TEntidad entidad, bool soloLectura = false)
        {
            try
            {
                if (entidad != null)
                {
                    if (EntidadesAbiertas.Count > 2)
                    {
                        Mensajes.Error("Máximo de pestañas abiertas alcanzado.\n\nCierre las pestañas abiertas e intente nuevamente.");
                    }
                    else
                    {
                        var vm = (TVistaModelo)Activator.CreateInstance(typeof(TVistaModelo), entidad);
                        vm.SoloLectura = soloLectura;
                        vm.Editable = !vm.SoloLectura;
                        this.EntidadesAbiertas.Add(vm);
                        this.vistaABM.TabControlDerecho.SelectedIndex = this.EntidadesAbiertas.Count - 1;
                        this.vistaABM.TabControlDerecho.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
                this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>();
            }
        }

        public virtual void Editar(TEntidad entidad)
        {
            this.AbrirEntidad(entidad);
        }

        public virtual void Ver(TEntidad entidad)
        {
            //falta deshabilitar edicion
            this.AbrirEntidad(entidad, true);
        }

        public virtual void Cancelar()
        {
            this.EntidadesAbiertas.Remove(this.VistaModeloActual);
        }

        public virtual bool PuedeCancelar(object m)
        {
            if (m == null)
                return true;
            else
                return m.Equals(this.VistaModeloActual);
        }

        public virtual bool PuedeGrabar()
        {
            if (this.EntidadesAbiertas.Count == 0)
                return false;
            else
                return this.VistaModeloActual != null && !this.VistaModeloActual.SoloLectura && ValidadorEstatico.ValidadEntidad(this.EntidadActual);
        }

        public virtual bool PuedeCrearNuevo()
        {
            return true;
        }

        public virtual bool PuedeVer()
        {
            return Buscador.ItemSeleccionado != null && !this.EntidadesAbiertas.Any(x => x.Modelo.Codigo == Buscador.ItemSeleccionado.Codigo);
        }

        public virtual bool PuedeEditar()
        {
            return Buscador.ItemSeleccionado != null && !this.EntidadesAbiertas.Any(x => x.Modelo.Codigo == Buscador.ItemSeleccionado.Codigo);
        }

        public virtual bool PuedeBorrar()
        {
            return this.VistaModeloActual != null && !this.VistaModeloActual.SoloLectura && ObtenerId(this.EntidadActual) > 0;
        }

        public virtual bool PuedeClonar()
        {
            return ObtenerId(this.EntidadActual) > 0;
        }


        #endregion

        #region Metodos Privados
        private object CrearVista()
        {
            return Activator.CreateInstance(VistaTemplate);
        }

        private int ObtenerId(TEntidad Entidad)
        {

            if (Entidad == null)
                return 0;
            else
                return ((DTOBase)Entidad).Id;

        }
        #endregion

        #region Vistas

        private IBaseABM vistaABM;

        /// <summary>
        /// Vista ABM. La usa para satear el presentador a una vista.
        /// </summary>
        public IBaseABM VistaABM
        {
            get { return vistaABM; }
            set
            {
                vistaABM = value;
                ((System.Windows.Window)vistaABM).DataContext = this;
                this.SetearVista();
            }
        }

        private void SetearVista()
        {
            DataTemplate dt = new DataTemplate(typeof(TEntidad));
            FrameworkElementFactory fef = new FrameworkElementFactory(VistaTemplate);
            //fef.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            fef.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
            fef.SetValue(FrameworkElement.MarginProperty, new Thickness(10));
            dt.VisualTree = fef;
            this.vistaABM.TabControlDerecho.ContentTemplate = dt;

        }


        #endregion

    }

    public class PresentadorABMB<TEntidad, TVistaModelo, TEntidadBuscador> : PresentadorABM<TEntidad, TVistaModelo>
        where TEntidad : DTOMaestro, new()
        where TVistaModelo : VistasModelos.VistaModeloBase<TEntidad>, new()
        where TEntidadBuscador : DTOMaestro, new()
    {
        public PresentadorABMB()
            : base(FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>(),
                  FabricaPresentadores._Resolver<IPresentadorBuscador<TEntidadBuscador>>())
        {

        }
    }


    //public class PresentadorABMC<TEntidad, TVistaModelo> : PresentadorABM<TEntidad, TVistaModelo>
    //    where TEntidad : DTOMaestro, new()
    //    where TVistaModelo : VistasModelos.VistaModeloBase<TEntidad>, new()
    //{

    //    public PresentadorABMC()
    //        : base(FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>(), FabricaPresentadores._Resolver<IPresentadorBuscador<TEntidad>>())
    //    {
    //        //this.Inicializador(FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TEntidad>>(),
    //        //FabricaPresentadores._Resolver<IPresentadorBuscador<TEntidad>>());
    //    }

    //    //public PresentadorABMC(IServicioABM<TEntidad> servicio, IPresentadorBuscador<TEntidad> buscador)
    //    //{
    //    //    this.Inicializador(servicio, buscador);
    //    //}

    //}
}


