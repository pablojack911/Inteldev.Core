using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Controles;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;

namespace Inteldev.Core.Presentacion.Presentadores
{
	//public class PresentadorListAbm<TMaestro,TDetalle> : PresentadorMaestroDetalle<TMaestro, TDetalle>
	//	where TMaestro : DTOBase, new()
	//	where TDetalle : new()
	//{

	//	//public PresentadorListAbm(List<TDetalle> DetalleDTO) : base(DetalleDTO)
	//	//{
	//	//	this.CmdAgregar = new RelayCommand(p => this.agregar());
	//	//	this.CmdEditar = new RelayCommand(p => this.editar());
	//	//	this.CmdBorrar = new RelayCommand(p => this.borrar());      
	//	//}

	//	//#region Comandos

	//	//public ICommand CmdAgregar { get; set; }
	//	//public ICommand CmdBorrar { get; set; }
	//	//public ICommand CmdEditar { get; set; }

	//	//#endregion

	//	//#region Implementacion Comandos

	//	//protected bool agregar()
	//	//{
	//	//	if (this.CrearItemDetalle == null)
	//	//	{
	//	//		presentador.EntidadActual = new TDetalle();
	//	//	}
	//	//	else
	//	//	{
	//	//		presentador.EntidadActual = this.CrearItemDetalle();
	//	//	}

	//	//	presentador.Ejecutar();
	//	//	if (presentador.EntidadActual != null)
	//	//	{
	//	//		//this.Detalle.Add(presentador.EntidadActual);
	//	//		return true;
	//	//	}
	//	//	else
	//	//		return false;
	//	//}
	//	//public Func<TDetalle> CrearItemDetalle { get; set; }

	//	//protected bool editar()
	//	//{
	//	//	//presentador.EntidadActual = this.ItemDetalleActual;
	//	//	presentador.Ejecutar();
	//	//	return true;
	//	//}

	//	//public bool borrar( )
	//	//{
	//	//	return true;
	//	//}

	//	//#endregion
	//}
}
