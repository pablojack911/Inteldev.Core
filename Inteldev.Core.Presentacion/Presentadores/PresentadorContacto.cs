using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;


namespace Inteldev.Core.Presentacion.Presentadores
{

	public class PresentadorContacto<TMaestro> : PresentadorGrilla<TMaestro, Contacto, ItemContacto>, Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorContacto<TMaestro>
		where TMaestro : DTOBase, new()
	{

		#region Campos

        private IPresentadorGrilla<TMaestro, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono> presentadorTelefono;
		private IPresentadorDomicilio presentadorDomicilio;

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="PresentadorTelefono">Instancia del Presentador Telefono</param>
		/// <param name="PresentadorDomicilio"></param>
		/// <param name="Objeto">Objeto Contacto. Pasa aca el DTO y listo</param>
        public PresentadorContacto(IPresentadorGrilla<TMaestro, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono> PresentadorTelefono, Contacto Objeto, IPresentadorDomicilio PresentadorDomicilio)
			: base(Objeto)
		{
			if (PresentadorTelefono == null)
				throw new ArgumentNullException("PresentadorTelefono no puede ser NULL");
			if(PresentadorDomicilio == null)
				throw new ArgumentNullException("Presentador Domicilio no puede ser NULL");
			this.presentadorTelefono = PresentadorTelefono;
			this.presentadorDomicilio = PresentadorDomicilio;
		}

		public override void CrearVentana()
		{
			this.ventana = new BaseVentanaDialogo();
			ItemContacto vistaContacto = new ItemContacto();
			ventana.vistaPrincipal.Content = vistaContacto;
			ventana.DataContext = this; //asigna datacontext como este presentador.
			presentadorTelefono.DTO = this.Objeto.Telefonos;
			presentadorDomicilio.Item = this.Objeto.Domicilio;
			vistaContacto.telefonos.Presentador = this.presentadorTelefono;
			vistaContacto.domicilio.Presentador = this.presentadorDomicilio;
			this.ventana.ShowDialog();
		}

		#region Implementacion Comandos

		public override bool AgregarItem( )
		{
            this.Inicializar();
            this.CrearVentana();
            Objeto.Telefonos = (List<Inteldev.Core.DTO.Locacion.Telefono>) presentadorTelefono.DetalleDTO;
            this.modoEdicion = false;
			return true;
		}

		public override bool Editar( )
		{
			if (ItemSeleccionado != null)
			{
				this.Objeto = this.ItemSeleccionado;
                this.modoEdicion = true;
				CrearVentana();
			}
			return true;
		}

		public override bool Aceptar( )
		{
			return base.Aceptar();
		}


		#endregion

		
	}
}
