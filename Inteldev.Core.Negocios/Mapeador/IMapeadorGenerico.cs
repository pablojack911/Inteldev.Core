
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Inteldev.Core.Negocios.Mapeador
{
	public interface IMapeadorGenerico<TEntidad, TDto>
	{
		IMappingEngine MotorDeMapeador();
		TEntidad DtoToEntidad(TDto dto);
		TEntidad DtoToEntidad(object dto);
		TEntidad DtoToEntidad(TDto dto, TEntidad entidad);
		TDto EntidadToDto(TEntidad entidad);
		TDto EntidadToDto(TEntidad entidad, TDto dto);
		List<TDto> ToListDto(List<TEntidad> listaEntidades);
		List<TEntidad> ToListEntidad(object listaDto);
		List<TEntidad> ToListEntidad(List<TDto> listaDto);
	}
}
