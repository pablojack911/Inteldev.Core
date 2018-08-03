using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Controles;

namespace Inteldev.Core.Presentacion.Presentadores
{

	public class PresentadorMaestroDetalle<TMaestro, TDetalle> : DependencyObject, IPresentadorMaestroDetalle<TMaestro, TDetalle>
		where TMaestro : new()
		where TDetalle : new()
	{

        public TMaestro Maestro { get; set; }

		#region DP's

		/// <summary>
		/// Propiedad que bindea con el DTO. Esta separada de la otra DP por ser de distinto tipo
		/// </summary>
		//public System.Collections.Generic.List<TDetalle> DetalleDTO
        public IList<TDetalle> DetalleDTO
		{
            get { return (IList<TDetalle>)GetValue(DetalleDTOProperty); }
			set { SetValue(DetalleDTOProperty, value); }
		}

		// Using a DependencyProperty as the backing store for DetalleDTO.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DetalleDTOProperty =
            DependencyProperty.Register("DetalleDTO", typeof(IList<TDetalle>), typeof(PresentadorMaestroDetalle<TMaestro, TDetalle>));

		/// <summary>
		/// Propiedad que bindea con el datagrid, el cual se actualiza automaticamente cuando cambia esta colleccion.
		/// </summary>
		public ObservableCollection<TDetalle> Detalle
		{
			get { return (ObservableCollection<TDetalle>)GetValue(DetalleProperty); }
			set { SetValue(DetalleProperty, value); }
		}

		public static readonly DependencyProperty DetalleProperty = DependencyProperty.Register("Detalle", typeof(ObservableCollection<TDetalle>), typeof(PresentadorMaestroDetalle<TMaestro, TDetalle>));

		public TDetalle ItemSeleccionado
		{
			get { return (TDetalle)GetValue(ItemSeleccionadoProperty); }
			set { SetValue(ItemSeleccionadoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemDetalleActual.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemSeleccionadoProperty =
			DependencyProperty.Register("ItemSeleccionado", typeof(TDetalle), typeof(PresentadorMaestroDetalle<TMaestro, TDetalle>));

		#endregion

		#region Event Handlers

		/// <summary>
		/// Manejador de evento cuando cambia la collecion observable detalle actualize la lista y se actualice el DTO
		/// </summary>
		/// <param name="sender">Quien tira el evento</param>
		/// <param name="e">Parametros del evento</param>
		private void Detalle_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					foreach (TDetalle item in e.NewItems)
					{
						this.DetalleDTO.Add(item);
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Move:

					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					foreach (TDetalle item in e.OldItems)
					{
						this.DetalleDTO.Remove(item);
					}

					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
					foreach (TDetalle item in e.OldItems)
					{

					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
			}
		}

		#endregion

		public IEnumerable DTO
		{
			get 
			{
				if (DetalleDTO == null)
					throw new MemberAccessException("DTO no esta inicializado");
				return DetalleDTO; 
			}
			set 
			{
                DetalleDTO = value as System.Collections.Generic.IList<TDetalle>;
                
                this.Detalle = new ObservableCollection<TDetalle>(value as IEnumerable<TDetalle> );

				this.Detalle.CollectionChanged += Detalle_CollectionChanged;
			}
		}

		public PresentadorMaestroDetalle()
		{
			this.CmdBorrar = new RelayCommand(p => this.BorrarItem());
		}


		#region Comandos

		public ICommand CmdBorrar { get; set; }

		#endregion

		#region Implementacion Comandos

		/// <summary>
		/// Borra el ItemSeleccionado de la collecion Detalle. Despues se Actualiza DetalleDTO cuando captura el evento
		/// </summary>
		/// <returns></returns>
		public virtual bool BorrarItem( )
		{
			if (ItemSeleccionado != null)
			{
				this.Detalle.Remove(ItemSeleccionado);
			}
			return true;
		}

		#endregion

		public virtual void Inicializar( )
		{
			
		}
                
    }

}
