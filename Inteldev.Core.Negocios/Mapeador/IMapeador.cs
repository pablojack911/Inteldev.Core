using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Negocios.Mapeador
{
	/// <summary>
	/// Interface usada por Unity para resolver dependencias
	/// </summary>
	public interface IMapeador
	{		
		TEntidad DtoToEntidad<TDto, TEntidad>(TDto DTO);
        TEntidad DtoToEntidad<TDto, TEntidad>(TDto DTO,TEntidad Entidad);

		TDto EntidadToDto<TEntidad, TDto>(TEntidad Entidad);
		TDto EntidadToDto<TEntidad, TDto>(TEntidad Entidad, TDto DTO);

		List<TDto> ListaToDto<TDto, TEntidad>(List<TEntidad> listaEntidades);
		List<TEntidad> ListaToEntidad<TDto, TEntidad>(List<TDto> listaDTO);
	}
}

