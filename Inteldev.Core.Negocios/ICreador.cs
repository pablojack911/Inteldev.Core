using System;
using Inteldev.Core.Modelo;

namespace Inteldev.Core.Negocios
{
	/// <summary>
	/// Interfaz que deben implementar los creadores de entidades
	/// </summary>
	/// <typeparam name="TEntidad">tipo de entidad a crear</typeparam>
    public interface ICreador<TEntidad> where TEntidad : EntidadBase
    {
		/// <summary>
		/// Metodo que crea la entidad
		/// </summary>
		/// <returns>la entidad creada</returns>
        TEntidad Crear();
        TEntidad Crear(params object[] args);
    }

}
