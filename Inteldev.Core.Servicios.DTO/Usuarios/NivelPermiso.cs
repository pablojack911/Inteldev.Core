using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Usuarios
{
    public enum NivelPermiso : int
    {
		[EnumMember]
		Denegado = 0,
		[EnumMember]
		Administrador = 1,
		[EnumMember]
		Ver = 2,
		[EnumMember]
		Editar = 3,
		[EnumMember]
		Crear = 4,
		[EnumMember]
		Borrar = 5,
		[EnumMember]
		Personalizado = 6
    }
}
