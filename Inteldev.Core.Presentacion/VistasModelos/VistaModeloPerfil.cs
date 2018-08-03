using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Core.Presentacion.VistasModelos
{
	public class VistaModeloPerfil : VistaModeloBase<PerfilUsuario>
	{
		#region DP's

		public List<Permiso> Menues
		{
			get { return (List<Permiso>)GetValue(MenuesProperty); }
			set { SetValue(MenuesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Menues.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MenuesProperty =
			DependencyProperty.Register("Menues", typeof(List<Permiso>), typeof(VistaModeloPerfil));

		public Permiso ItemArbolSeleccionado
		{
			get { return (Permiso)GetValue(ItemArbolSeleccionadoProperty); }
			set { SetValue(ItemArbolSeleccionadoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemArbolSeleccionado.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemArbolSeleccionadoProperty =
			DependencyProperty.Register("ItemArbolSeleccionado", typeof(Permiso), typeof(VistaModeloPerfil));

		#endregion

		public VistaModeloPerfil( )
		{
		}

		private void CambiaCascada(List<Permiso> permisos, NivelPermiso nivelPermiso)
		{
			if (permisos != null)
			{
				foreach (var item in permisos)
				{
					item.NivelPermiso = nivelPermiso;
					CambiaCascada(item.SubModulos, nivelPermiso);
				}
			}
		}

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if (e.Property.Name == "ItemArbolSeleccionado")
			{
				var valorNuevo = (Permiso)e.NewValue;
				var permi = this.Menues.FirstOrDefault(p=>p.Id == valorNuevo.Id);
				this.CambiaCascada(permi.SubModulos,valorNuevo.NivelPermiso);
			}
		}

		public VistaModeloPerfil(PerfilUsuario DTO)
			:base(DTO)
		{
			var servicesuMenu = FabricaClienteServicio.Instancia.CrearCliente<IServicioMenu>("ServicioMenu");
			var servicesuPerfil = FabricaClienteServicio.Instancia.CrearCliente<IServicioPerfilUsuario>("ServicioPerfilUsuario");
			PerfilUsuario perfil = new PerfilUsuario();
			this.Menues = new List<Permiso>();
			if (DTO.Id != 0)
			{
				perfil = DTO;
				var servicesuPermiso = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Permiso>>("ServicioPermiso");
				perfil.Permiso = servicesuPermiso.ObtenerPorId(perfil.Permiso.Id,CargarRelaciones.CargarTodo,Sistema.Instancia.EmpresaActual.Codigo);
			}
			else
			{
				perfil = servicesuPerfil.CargarPermisos(servicesuMenu.ObtenerMenuTodo(Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual));
			}
			this.Menues.Add(perfil.Permiso);
			DTO.Permiso = this.Menues.FirstOrDefault();
		}
	}
}
