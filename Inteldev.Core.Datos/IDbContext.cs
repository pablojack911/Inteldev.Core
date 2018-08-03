using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Usuarios;
using System.Data.Entity.Infrastructure;
using Inteldev.Core.Modelo.Auditoria;

namespace Inteldev.Core.Datos
{
    /// <summary>
    /// Interface con metodos para que debe usar para realizar un ABM
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Devuelve el set del tipo pasado por parametro
        /// </summary>
        /// <param name="tipo">Tipo</param>
        /// <returns>DbSet</returns>
        DbSet Set(Type tipo);

        /// <summary>
        /// Setea la entidad que debe ser una entidad que deriva de la EntidadBase.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de la entidad</typeparam>
        /// <returns>DbSet del tipo de entidad</returns>
        IDbSet<TEntidad> Set<TEntidad>() where TEntidad : EntidadBase;

        /// <summary>
        /// Se obtiene el elemento en el contexto.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad.</typeparam>
        /// <param name="entidad">Entidad a buscar en el contexto.</param>
        /// <returns></returns>
        DbEntityEntry<TEntidad> Entry<TEntidad>(TEntidad entidad) where TEntidad : EntidadBase;

        DbContextTransaction EmpezarTransaccion();

        /// <summary>
        /// Guarda cambios en la base y devuelve el resultado de guardar los cambios.
        /// </summary>
        /// <returns>resultado de guardar los cambios</returns>
        EvaluarConcurrencia SaveChanges();

        /// <summary>
        /// Setear un flag a un objeto para marcarlo como que esta borrado
        /// </summary>
        /// <param name="objeto">objeto a setear flag</param>
        void MarcarComoBorrado(object objeto);

        /// <summary>
        /// Setea un flag a un objeto para marcarlo como modificado
        /// </summary>
        /// <param name="objeto">objeto a setear flag</param>
        void MarcarComoModificado(object objeto);

        /// <summary>
        /// Setea un flag a un objeto para marcarlo como no-modificado. Es el anti-metodo de MarcarComoModificado
        /// </summary>
        /// <see cref="MarcarComoModificado"/>
        /// <param name="objeto">objeto a setear flag</param>
        void MarcarComoNoModificado(object objeto);

        /// <summary>
        /// Verifica si un objeto esta o no borrado.
        /// </summary>
        /// <param name="objeto">Objeto a consultar</param>
        /// <returns>True si esta borrado. False si todavia existe</returns>
        bool EstaBorrado(object objeto);

        /// <summary>
        /// Verifica si un objeto esta o no modificado
        /// </summary>
        /// <param name="objeto">Objeto a consultar</param>
        /// <returns>True si esta modificado. False si no lo esta</returns>
        bool EstaModificado(object objeto);

        /// <summary>
        /// no me queda muy claro
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad</typeparam>
        /// <param name="objeto">objeto a incluir en la relacion</param>
        /// <returns>Entidad</returns>
        IQueryable<TEntidad> IncluirRelaciones<TEntidad>(IQueryable<TEntidad> objeto, CargarRelaciones cargarRelaciones) where TEntidad : EntidadBase;

        /// <summary>
        /// Inserta objeto. No se inserta realmente hasta lo haber llamado a guardar cambios
        /// <see cref="SaveChanges"/>
        /// </summary>
        /// <param name="objeto">objeto a insertar</param>
        void Insertar<TEntidad>(TEntidad objeto, Usuario Usuario = null) where TEntidad : EntidadBase;

        /// <summary>
        /// Busca una entidad por si ID. Si tiene entidades que dependen de ella o ella depende de otras setear 
        /// cargar entidades en true
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad a buscar</typeparam>
        /// <param name="Id">ID de la entidad a buscar</param>
        /// <param name="cargarEntidadesRelacionadas">Si carga las dependencias o no</param>
        /// <returns>Entidad que se busca</returns>
        TEntidad BuscarPorId<TEntidad>(int Id, CargarRelaciones cargarEntidadesRelacionadas) where TEntidad : EntidadBase;

        TEntidad BuscarPorCodigo<TEntidad>(string codigo, CargarRelaciones cargarEntidadesRelacionadas) where TEntidad : EntidadMaestro;

        /// <summary>
        /// Actualiza los datos de una entidad. Este no se guarda hasta llamar a SaveChanges.
        /// </summary>
        /// <param name="objeto">objeto a actualizar</param>
        void Actualizar<TEntidad>(TEntidad objeto, Usuario Usuario = null) where TEntidad : EntidadBase;

        /// <summary>
        /// Borrar una entidad. Esta no se borra hasta llamar a SaveChanges.
        /// </summary>
        /// <param name="objeto">objeto a borrar</param>
        void Borrar<TEntidad>(TEntidad objeto, Usuario Usuario) where TEntidad : EntidadBase;

        /// <summary>
        /// Agrega una relacion a una entidad
        /// </summary>
        /// <param name="objeto">objeto a agregar a la entidad</param>
        void Adjuntar(object objeto);

        /// <summary>
        /// Carga la entidad, opcionalmente carga las entidades relacionadas.
        /// </summary>
        /// <typeparam name="TEntidad">tipo de entidad</typeparam>
        /// <param name="cargarEntidadesRelacionadas">Cargar o no las dependencias de la entidad</param>
        /// <returns>entidad a consultar</returns>
        IQueryable<TEntidad> Consultar<TEntidad>(CargarRelaciones cargarEntidadesRelacionadas) where TEntidad : EntidadBase;

        /// <summary>
        /// Borra el contenido de todas las propiedades de la entidad.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad</typeparam>
        void Vaciar<TEntidad>() where TEntidad : EntidadBase;

        /// <summary>
        /// Crea una entidad y devuelve la instancia
        /// </summary>
        /// <typeparam name="TEntidad">tipo de entidad a crear</typeparam>
        /// <returns>devuelve la entidad creada</returns>
        TEntidad Crear<TEntidad>() where TEntidad : EntidadBase;

        //void MarcarRelacionesComoNoModificado(object objeto);

        /// <summary>
        /// Metodo encargado de agregar una auditoria en la base cuando se realize una accion sobre un camppo.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad que se trabaja</typeparam>
        /// <param name="objeto">Entidad manipulada</param>
        /// <param name="accion">Agrega, Modifica o Elimina</param>
        /// <param name="Usuario">Usuario que realizo la accion</param>
        void insertaAuditoria<TEntidad>(TEntidad objeto, Accion accion, Usuario Usuario) where TEntidad : EntidadBase;

    }
}
