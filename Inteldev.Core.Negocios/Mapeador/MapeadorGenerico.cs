
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Mapeador
{
    public class MapeadorGenerico<TEntidad, TDto> : Inteldev.Core.Negocios.Mapeador.IMapeadorGenerico<TEntidad, TDto>
    {

        public TEntidad DtoToEntidad(TDto dto)
        {
            return Mapeador.Instancia.DtoToEntidad<TDto, TEntidad>(dto);
        }

        public TEntidad DtoToEntidad(object dto)
        {
            var valor = (TDto)dto;
            return Mapeador.Instancia.DtoToEntidad<TDto, TEntidad>(valor);
        }

        public TEntidad DtoToEntidad(TDto dto, TEntidad entidad)
        {
            return Mapeador.Instancia.DtoToEntidad<TDto, TEntidad>(dto, entidad);
        }

        public TDto EntidadToDto(TEntidad entidad)
        {
            return Mapeador.Instancia.EntidadToDto<TEntidad, TDto>(entidad);
        }

        public TDto EntidadToDto(TEntidad entidad, TDto dto)
        {
            return Mapeador.Instancia.EntidadToDto<TEntidad, TDto>(entidad, dto);
        }

        public List<TDto> ToListDto(List<TEntidad> listaEntidades)
        {
            return Mapeador.Instancia.ListaToDto<TDto, TEntidad>(listaEntidades);
        }

        public List<TEntidad> ToListEntidad(object listaDto)
        {
            var listaDTOPosta = (listaDto as IEnumerable<object>).Cast<TDto>().ToList();
            return Mapeador.Instancia.ListaToEntidad<TDto, TEntidad>(listaDTOPosta);
        }

        public List<TEntidad> ToListEntidad(List<TDto> listaDto)
        {
            return Mapeador.Instancia.ListaToEntidad<TDto, TEntidad>(listaDto);
        }

        public AutoMapper.IMappingEngine MotorDeMapeador()
        {
            return Mapeador.Instancia.Engine;
        }
    }
}
