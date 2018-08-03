using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Inteldev.Core.DTO.Organizacion
{
    public enum UnidadeDeNegocio : int
    {
		[EnumMember]
		Preventa = 0,
		[EnumMember]
		Mayorista = 1,
		[EnumMember]
		Gestion = 2,
		[EnumMember]
		Representaciones = 3,
		[EnumMember]
		Logistica = 4
    }
}
