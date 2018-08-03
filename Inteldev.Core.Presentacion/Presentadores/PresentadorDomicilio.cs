using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Core.Presentacion.Presentadores
{
	public class PresentadorDomicilio : DependencyObject, Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorDomicilio
	{
               
		public Domicilio Item
		{
			get { return (Domicilio)GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemProperty =
			DependencyProperty.Register("Item", typeof(Domicilio), typeof(PresentadorDomicilio));

		public List<Calle> Calles
		{
			get { return (List<Calle>)GetValue(CallesProperty); }
			set { SetValue(CallesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Calles.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CallesProperty =
			DependencyProperty.Register("Calles", typeof(List<Calle>), typeof(PresentadorDomicilio));

		private IServicioABM<Calle> servicioCalle;
               

		public PresentadorDomicilio( )
		{
            try
            {
                servicioCalle = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Calle>>();
                Calles = servicioCalle.ObtenerLista(1, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
            }
            catch (Exception exc)
            {
                Mensajes.Error(exc);
            }
		}
		

	}
}
