using System;
using Inteldev.Core.Modelo;
using System.Collections.Generic;
using Inteldev.Core.Modelo.Usuarios;
using Inteldev.Core.DTO.Carriers;

namespace Inteldev.Core.Negocios
{
    /// <summary>
    /// Interface que deben implementar los borradores
    /// </summary>
    /// <typeparam name="TEntidad">tipo de entidad a borrar</typeparam>
    public interface IBorrador<TEntidad> where TEntidad : EntidadBase
    {
        /// <summary>
        /// Borra una entidad de la base de datos. Se borra efectivamente si GrabarCambios esta en true (por defecto)
        /// <see cref="GrabarCambios"/>
        /// </summary>
        /// <param name="entidad">entidad a borrar</param>
        /// <returns>Si borro o no la entidad</returns>
        ErrorCarrier Borrar(TEntidad entidad, Usuario Usuario);

        ErrorCarrier BorrarLista(List<TEntidad> listaEntidades, Usuario Usuario);

        ErrorCarrier Borrar(IList<TEntidad> listaEntidad, Usuario Usuario);

        ErrorCarrier Borrar(int id, Usuario Usuario);
        /// <summary>
        /// Flag para saber si cuando se borra una entidad efectivamente se borra de la base de datos o no.
        /// </summary>
        bool GrabarCambios { get; set; }

        /// <summary>
        /// Utilizado por ImportadorDatos -> BulkInsert
        /// Caso: PadronIIBB
        /// </summary>
        /// <returns></returns>
        ErrorCarrier BorrarTodo();
    }

}
