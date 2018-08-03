using Inteldev.Core.Modelo.Organizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo
{
	public class Numerador : EntidadMaestro
	{
		public int Numero { get; set; }
		public int Prenumero { get; set; }
		public Empresa Empresa { get; set; }
		[ForeignKey("Empresa")]
		public int? EmpresaId { get; set; }
		public Sucursal Sucursal { get; set; }
		[ForeignKey("Sucursal")]
		public int? SucursalId { get; set; }
		public Documentos TipoDocumento { get; set; }
	}
}
