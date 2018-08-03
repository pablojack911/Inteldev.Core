using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Negocios.Usuarios
{
	/// <summary>
	/// 
	/// </summary>
    public interface ILogin
    {
		/// <summary>
		/// Autentica un usuario y devuelve el objeto usuario
		/// </summary>
		/// <param name="usuario">nombre de usuario</param>
		/// <param name="clave">contraseña del usuario</param>
		/// <returns>objeto usuario</returns>
        Usuario Autenticar(string usuario, string clave);
    }

}
