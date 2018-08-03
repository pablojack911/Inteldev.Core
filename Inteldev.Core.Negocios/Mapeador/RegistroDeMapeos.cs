using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Negocios.Mapeador
{
	/// <summary>
	/// Fabrica de Mapeos. Implementacion en Servidor Fixius.
	/// </summary>
	public abstract class RegistroDeMapeos : Mapeador
	{

		public RegistroDeMapeos()
		{
			this.RegistrarBiDefault<Inteldev.Core.Datos.EvaluarConcurrencia.ValoresDeConcurrencia, Inteldev.Core.DTO.Carriers.ValoresDeConcurrencia>();
			this.RegistrarBiDefault<Inteldev.Core.Datos.EvaluarConcurrencia, Inteldev.Core.DTO.Carriers.EvaluarConcurrencia>();
		}

		/// <summary>
		/// Agrego un mapeo a la lista. Registra Bidirreccionalemente
		/// </summary>
		/// <param name="Entidad">tipo de entidad</param>
		/// <param name="DTO">tipo de dto</param>
		public void RegistrarBiDefault<TEntidad,TDto>()
		{
			this.MapeoDtoToEntidad<TDto,TEntidad>();
			this.MapeoEntidadToDto<TEntidad,TDto>();
		}

		/// <summary>
		/// Para que configuren los registros de mapeos
		/// </summary>
		public abstract void Configurar();

	}
}
