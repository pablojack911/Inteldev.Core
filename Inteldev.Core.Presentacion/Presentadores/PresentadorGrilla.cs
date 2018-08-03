using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using System.Collections;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System.Windows.Controls;
using Inteldev.Core.Extenciones;
using Inteldev.Core.DTO.Validaciones;
namespace Inteldev.Core.Presentacion.Presentadores
{
    public class PresentadorGrilla<TMaestro, TDetalle, TVista> : PresentadorMaestroDetalle<TMaestro, TDetalle>, IPresentadorGrilla<TMaestro, TDetalle, TVista>
        where TMaestro : DTOBase, new()
        where TDetalle : DTOBase, new()
        where TVista : FrameworkElement, new()
    {

        #region DP
        /// <summary>
        /// Propiedad a la que tienen que bindear las vistas para agregar una fila a la grilla.
        /// Ejemplo: en el caso de las observaciones, cuando agregas una observacion y se abre una vista
        /// esta vista tiene que bindear con esta propiedad. Mas adelante cuando se acepta se agrega 
        /// a la collecion detalle. 
        /// </summary>
        public TDetalle Objeto
        {
            get { return (TDetalle)GetValue(ObjetoProperty); }
            set { SetValue(ObjetoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Objeto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjetoProperty =
            DependencyProperty.Register("Objeto", typeof(TDetalle), typeof(PresentadorGrilla<TMaestro, TDetalle, TVista>));

        #endregion

        #region Comandos

        public ICommand CmdAgregar { get; set; }
        public ICommand CmdEditar { get; set; }
        public ICommand CmdBorrar { get; set; }
        public ICommand CmdAceptar { get; set; }
        public ICommand CmdCancelar { get; set; }

        #endregion

        #region Implementacion Comandos

        /// <summary>
        /// Comportamiento del boton Agregar
        /// </summary>
        /// <returns>True siempre??</returns>
        public virtual bool AgregarItem()
        {
            this.modoEdicion = false;
            this.Inicializar();
            CrearVentana();
            //Detalle.Add(this.Objeto);
            return true;
        }

        /// <summary>
        /// Comportamiento del boton Editar
        /// </summary>
        /// <returns>True siempre??</returns>
        public virtual bool Editar()
        {
            this.modoEdicion = true;
            this.Objeto = this.ItemSeleccionado.Clonar<TDetalle>() ;
            if (Objeto != null)
            {
                //this.ElementoDeGrilla.CapturarEstado(this.Objeto);
                CrearVentana();
            }
            return true;
        }

        /// <summary>
        /// Comportamiento del boton Aceptar
        /// </summary>
        /// <returns>True siempre??</returns>
        public virtual bool Aceptar()
        {
            if (this.modoEdicion)
            {
                this.Objeto.Mapear(this.ItemSeleccionado);
            }
            else
                this.Detalle.Add(this.Objeto);
            CerrarVentana();
            //this.Inicializar();
            return true;
        }

        /// <summary>
        /// Comportamiento del boton cancelar
        /// </summary>
        /// <returns>True siempre??</returns>
        public virtual bool Cancelar()
        {
            CerrarVentana();
            if (this.Objeto != null)
            {              

                if (this.Objeto.Id == 0 && !this.modoEdicion)//agregando
                {
                    this.ItemSeleccionado = Objeto;
                    this.BorrarItem();                  
                }
                this.Inicializar();
            }
            return false;
        }

        /// <summary>
        /// Utilizado para evaluar si se puede Eliminar.
        /// </summary>
        /// <returns></returns>
        public virtual bool PuedeEliminar()
        {
            return true;
        }
        /// <summary>
        /// Utilizado para evaluar si se puede editar.
        /// </summary>
        /// <returns></returns>
        public virtual bool PuedeEditar()
        {
            return this.ItemSeleccionado != null;
        }
        /// <summary>
        /// Utilizado para evaluar si se puede aceptar. 
        /// </summary>
        /// <returns></returns>
        public virtual bool PuedeAceptar()
        {
            //return true;
            return ValidadorEstatico.ValidadEntidad(this.Objeto);
        }
        /// <summary>
        /// Metodo para sobreescribir. En caso que las listas tengan un numero acotado de items, se devuelve false
        /// </summary>
        /// <returns></returns>
        public virtual bool PuedeAgregar()
        {
            return true;
        }

        #endregion

        #region  Campos

        protected BaseVentanaDialogo ventana;

        protected bool modoEdicion;

        //protected Patrones.Memento<TDetalle> ElementoDeGrilla;
        public Type VistaModeloDetalleType { get; set; }
        public object VistaModeloDetalleInstancia { get; set; }

        #endregion

        public virtual void CrearVentana()
        {
            this.ventana = new BaseVentanaDialogo();
            ventana.vistaPrincipal.Content = new TVista();
            ventana.DataContext = this;
            if (this.VistaModeloDetalleType != null)
            {
                var item = (this.ItemSeleccionado == null ? this.Objeto : this.ItemSeleccionado);
                this.VistaModeloDetalleInstancia = Activator.CreateInstance(this.VistaModeloDetalleType, new TDetalle());
                ventana.VistaPrincipal.DataContext = this.VistaModeloDetalleInstancia; //asigna datacontext como este presentador.
            }
            ventana.ShowDialog();
            var dpCollecction = this.Detalle;
            this.Detalle = null;
            this.Detalle = dpCollecction;

        }

        public void CerrarVentana()
        {
            this.ventana.Close();
        }

        /// <summary>
        /// Para los Data Grid, previene que se muestren en el grid las columnas Id y Nombre.
        /// </summary>
        /// <param name="Grid">Objeto DataGrid</param>
        /// <param name="Objeto">Objeto que contiene las columnas que se tienen que mostrar</param>
        public void quitarColumnasIdNombre(FrameworkElement Grid, Type Objeto)
        {
            Type gridType = Grid.GetType();
            var gridProperty = gridType.GetProperty("Grid").GetValue(Grid, null);
            var proper = gridProperty.GetType().GetProperty("AutoGenerateColumns");
            proper.SetValue(gridProperty, false, null);
            var properties = Objeto.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "Nombre" && property.Name != "Id" && property.Name != "Error")
                {
                    var column = new DataGridTextColumn();
                    dynamic columns = gridProperty.GetType().GetProperty("Columns").GetValue(gridProperty, null);
                    column.Header = property.Name;
                    column.Binding = new Binding(property.Name);
                    columns.Add(column);
                }
            }
        }

        public override void Inicializar()
        {
            base.Inicializar();
            this.Objeto = new TDetalle();
            this.ItemSeleccionado = null;
            //if (ventana != null)
            //	CerrarVentana();	
        }

        #region Constructor
        /// <summary>
        /// Overload para aceptar Ienumerable
        /// </summary>
        /// <param name="Objeto">Binding DTO</param>
        public PresentadorGrilla(TDetalle Objeto)
            : base()
        {
            if (Objeto == null)
                throw new ArgumentNullException("Propiedad Objeto no puede ser NULL");
            this.CmdAgregar = new RelayCommand(p => this.AgregarItem(), q=> this.PuedeAgregar());
            this.CmdEditar = new RelayCommand(p => this.Editar(), q => this.PuedeEditar());
            this.CmdBorrar = new RelayCommand(p => this.BorrarItem(), q => this.PuedeEliminar());
            this.CmdAceptar = new RelayCommand(p => this.Aceptar(), q => this.PuedeAceptar());
            this.CmdCancelar = new RelayCommand(p => this.Cancelar());
            this.Objeto = Objeto;
            //this.ElementoDeGrilla = new Patrones.Memento<TDetalle>(this.Objeto);
        }
        
        #endregion

    }
}
