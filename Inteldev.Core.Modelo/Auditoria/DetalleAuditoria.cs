using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Auditoria
{
	public class DetalleAuditoria : EntidadBase
	{
		public string NombreTabla { get; set; }
		public string NombreColumna { get; set; }
		public Accion Accion { get; set; }
		public string ValorAnterior { get; set; }
		public string ValorNuevo { get; set; }
	}
}
