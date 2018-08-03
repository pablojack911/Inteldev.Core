using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo.Auditoria
{
	public class Auditoria
	{
		public Auditoria( )
		{
			this.Fecha = DateTime.Now;
		}
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public string UsuarioSistema { get; set; }
		public string UsuarioWindows { get; set; }
		public string Windows { get; set; }
		public string Sucursal { get; set; }
		public string Empresa { get; set; }
		public string NombrePC { get; set; }
		public string IP { get; set; }
		public string UnidadDeNegocio { get; set; }
		public string Entidad { get; set; }

		public ICollection<DetalleAuditoria> Detalle { get; set; }
	}
}


