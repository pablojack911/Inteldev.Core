using System;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Usuarios;
using Inteldev.Core.DTO.Carriers;
using System.Collections.Generic;
using Inteldev.Core.Datos;

namespace Inteldev.Core.Negocios
{
    /// <summary>
    /// Interface que deben implementar los grabadores. 
    /// </summary>
    /// <typeparam name="TEntidad">tipo de entidad. Restringe la implementacion de la entidad a solo aquellas que
    /// implementen EntidadBase</typeparam>
    /// <see cref="EntidadBase"/>
    public interface IGrabador<TEntidad> where TEntidad : EntidadBase
    {
        /// <summary>
        /// Metodo que graba una entidad
        /// </summary>
        /// <param name="Entidad">entidad a grabar</param>
        /// <returns>True si fue grabado exitosamente. False si no lo hizo</returns>
        GrabadorCarrier Grabar(TEntidad Entidad, Usuario Usuario = null);

        GrabadorCarrier GrabarNuevo(TEntidad Entidad, Usuario Usuario = null);

        GrabadorCarrier GrabarExistente(TEntidad Entidad, Usuario Usuario = null);

        GrabadorCarrier GrabarListaImportador(List<TEntidad> listaEntidades, Usuario Usuario = null);

        GrabadorCarrier GuardarCambios(); //POCHO -> BORRAR! Solo para probar

        /// <summary>
        /// Graba los cambios realizados sobre la entidad.
        /// </summary>
        bool grabarDatos { get; set; }

        //METODOS NUEVOS - 2015
        /// <summary>
        /// Metodo encargado de hacer el pasaje de valores simples de una entidad a otra.
        /// </summary>
        /// <param name="origen">Entidad de origen</param>
        /// <param name="destino">Entidad de destino</param>
        /// <param name="cntxt">Contexto donde se encuentran las entidades</param>
        void SetearValores(EntidadBase origen, EntidadBase destino, IDbContext cntxt);

        /// <summary>
        /// Metodo utilizado para actualizar las propiedades de referencia.
        /// </summary>
        /// <param name="entidad">Entidad a actualizar</param>
        /// <param name="Propiedad">Nombre de la propiedad de referencia</param>
        /// <returns>Id de la FK</returns>
        int? SetearFk(EntidadBase entidad, string Propiedad);

        /// <summary>
        /// Metodo para actualizar los valores entre dos colecciones que mantienen una relacion Muchos a Muchos
        /// </summary>
        /// <typeparam name="TipoEntidad">Tipo de la entidad de las listas</typeparam>
        /// <param name="actualizar">Lista de entidades tal cual como esta en la base de datos hasta el momento</param>
        /// <param name="modificado">Lista de entidades actualizadas desde el frente</param>
        /// <param name="cntxt">Contexto actual de trabajo</param>
        void ActualizarColeccionMuchosAMuchos<TipoEntidad>(ICollection<TipoEntidad> actualizar,
                                                   ICollection<TipoEntidad> modificado,
                                                   IDbContext cntxt,
                                                   Action<TipoEntidad> setsFk = null) where TipoEntidad : EntidadBase;
        /// <summary>
        /// Actualiza en el contexto entre 2 listas de valores.
        /// </summary>
        /// <typeparam name="TipoEntidad">Tipo de la entidad de las listas.</typeparam>
        /// <param name="actualizar">Lista de entidades tal cual como esta en la base de datos hasta el momento.</param>
        /// <param name="modificado">Lista de entidades actualizadas desde el frente.</param>
        /// <param name="cntxt">Contexto actual de trabajo.</param>
        void ActualizarColeccionUnoAMuchos<TipoEntidad>(ICollection<TipoEntidad> actualizar,
                                                   ICollection<TipoEntidad> modificado,
                                                   IDbContext cntxt,
                                                   Action<TipoEntidad> setsFk = null) where TipoEntidad : EntidadBase;
    }

}
