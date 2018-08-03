using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO
{
	public enum EstadoEntidad : int
	{
		[EnumMember]
		NoModificado = 0,
		[EnumMember]
		Modificado = 1,
		[EnumMember]
		Eliminado = 2,
		[EnumMember]
		Nuevo = 3,
		[EnumMember]
		NoHacerNada = 4,
		[EnumMember]
		Agregame = 5,
		[EnumMember]
		Quitar = 6
	}
}
