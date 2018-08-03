using Inteldev.Core.DTO.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
	public class Numerador : DTOMaestro
	{
		[DataMember]
		public int Numero { get; set; }
		[DataMember]
		public int Prenumero { get; set; }
		[DataMember]
		public Empresa Empresa { get; set; }
		[DataMember]
		public int EmpresaId { get; set; }
		[DataMember]
		public Sucursal Sucursal { get; set; }
		[DataMember]
		public int SucursalId { get; set; }
		[DataMember]
		public Documentos TipoDocumento { get; set; }
	}
}
