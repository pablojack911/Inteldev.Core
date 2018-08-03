using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Patrones;
using Inteldev.Core.Modelo;
using AutoMapper;
using System.Collections.ObjectModel;

namespace Inteldev.Core.Negocios.Mapeador
{
    /// <summary>
    /// Mapeador generico de DTO - Entidad, Entidad-DTO
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de entidad</typeparam>
    /// <typeparam name="Tdto">Tipo de DTO</typeparam>
    public class Mapeador : Singleton<Mapeador>, IMapeador, IMapeo
    {
        public IMappingEngine Engine
        {
            get
            {
                return Mapper.Engine;
            }
        }
        /// <summary>
        /// Agrega mapeo de Entidad a DTO
        /// </summary>
        /// <returns>IMappingexpression</returns>
        public IMappingExpression<TEntidad, TDto> MapeoEntidadToDto<TEntidad, TDto>()
        {
            return Mapper.CreateMap<TEntidad, TDto>(MemberList.Destination);
        }

        /// <summary>
        /// Agrega mapeo de DTO a Entidad
        /// </summary>
        /// <returns>IMappingExpression</returns>
        public IMappingExpression<TDto, TEntidad> MapeoDtoToEntidad<TDto, TEntidad>()
        {
            return Mapper.CreateMap<TDto, TEntidad>(MemberList.Destination);
        }

        /// <summary>
        /// Con el DTO que pasas te devuelvo la Entidad mapeada con todo cargadito.
        /// </summary>
        /// <param name="DTO">DTO con los datos a mapear</param>
        /// <returns>Entidad mapeadita y cargadita</returns>
        public TEntidad DtoToEntidad<TDto, TEntidad>(TDto DTO)
        {
            return Mapper.Map<TDto, TEntidad>(DTO);
        }

        /// <summary>
        /// Con la entidad que pasas te devuelvo el DTO mapeao y cargado
        /// </summary>
        /// <param name="Entidad">Entidad con los datos a mapear</param>
        /// <returns>DTO cargado</returns>
        public TDto EntidadToDto<TEntidad, TDto>(TEntidad Entidad)
        {
            return Mapper.Map<TEntidad, TDto>(Entidad);
        }

        /// <summary>
        /// Dada una lista de entidades, te devuelvo una lista de dto's mapeadas
        /// </summary>
        /// <param name="listaEntidades">lista de entidades</param>
        /// <returns>lista de dtos</returns>
        public List<TDto> ListaToDto<TDto, TEntidad>(List<TEntidad> listaEntidades)
        {
            List<TDto> listaDTO = new List<TDto>();
            if (listaEntidades == null)
            {
                return listaDTO;
            }
            else
            {
                return Mapper.Map(listaEntidades, listaDTO);
            }
        }

        /// <summary>
        /// Dada una lista de DTO's, te devuelvo una lista de entidades mapeadas
        /// </summary>
        /// <param name="listaDTO">lista con los dto's</param>
        /// <returns>lista de entidades</returns>
        public List<TEntidad> ListaToEntidad<TDto, TEntidad>(List<TDto> listaDTO)
        {
            List<TEntidad> listaEntidad = new List<TEntidad>();
            if (listaDTO == null)
            {
                return listaEntidad;
            }
            else
            {
                return Mapper.Map(listaDTO, listaEntidad);
            }
        }

        public TEntidad DtoToEntidad<TDto, TEntidad>(TDto DTO, TEntidad Entidad)
        {
            return Mapper.Map<TDto, TEntidad>(DTO, Entidad);
        }

        public TDto EntidadToDto<TEntidad, TDto>(TEntidad Entidad, TDto DTO)
        {
            return Mapper.Map<TEntidad, TDto>(Entidad, DTO);
        }
    }
}
