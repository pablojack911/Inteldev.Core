using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO.Validaciones
{
	public interface IValidador
	{
		/// <summary>
		/// Valida la entidad entera y te dice si esta bien o no
		/// </summary>
		/// <param name="Entidad">instancia de la entidad a validar</param>
		/// <returns>True si es valida, False si no lo es</returns>
		bool ValidaEntidad(object Entidad);
		/// <summary>
		/// Valida una propiedad de la entidad. Si no hay error no devuelve nada.
		/// </summary>
		/// <param name="Entidad">entidad a validad</param>
		/// <param name="Propiedad">propiedad de la entidad a validar</param>
		/// <returns>mensaje de error para mostrar</returns>
		string ValidaPropiedad(object Entidad, string Propiedad);
	}
}
