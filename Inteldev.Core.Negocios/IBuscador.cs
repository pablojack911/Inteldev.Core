using System;
using Inteldev.Core.Modelo;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Inteldev.Core.Datos;

namespace Inteldev.Core.Negocios
{
    /// <summary>
    /// Interfaz que deben implementar los buscadores de entidades
    /// </summary>
    /// <typeparam name="TEntidad">tipo de entidad a buscar</typeparam>
    public interface IBuscador<TEntidad> where TEntidad : class
    {
        /// <summary>
        /// Busca y devuelve en una lista las entidades que cumplen con la condicion de busqueda
        /// </summary>
        /// <param name="busqueda">Funcion que evalua la condicion de busqueda y devuelve bool</param>
        /// <returns>Lista de entidades.</returns>
        /// <seealso cref="EntidadBase"/>
        List<TEntidad> BuscarLista(Expression<Func<TEntidad, bool>> busqueda, CargarRelaciones cargaEntidades);

        List<TEntidad> BuscarLista(CargarRelaciones cargarentidades);

        /// <summary>
        /// Esto es para la parte de busqueda, que para armar la expresion necesito la consulta simple.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntidad> ConsultaSimple(CargarRelaciones cargarEntidades);

        /// <summary>
        /// Busca una sola entidad
        /// </summary>
        /// <param name="busqueda">ID</param>
        /// <returns>entidad que tiene el mismo ID que el objeto de busqueda</returns>
        TEntidad BuscarSimple(object busqueda);

        TMaestro BuscarPorCodigo<TMaestro>(object busqueda, List<ParametrosMiniBusca> parametros = null) where TMaestro : EntidadMaestro;

        /// <summary>
        /// Sobrecarga de BuscarLista. Es para que las partes de busqueda puedan usar expresiones dinamicas
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        List<TEntidad> BuscarLista(MethodCallExpression busqueda, CargarRelaciones cargarEntidades);

        List<TMaestro> BuscarDiferencia<TMaestro>(List<string[]> codigosImportados) where TMaestro : EntidadMaestro, new();

        bool ExisteCodigo<TMaestro>(string Codigo) where TMaestro : EntidadMaestro;


        CargarRelaciones CargarEntidadesRelacionadas { get; set; }

        /// <summary>
        /// Obtiene una lista de codigos para evaluar cual es el proximo disponible
        /// </summary>
        /// <param name="desde">la busqueda ocurre a partir del parametro desde</param>
        /// <param name="tamañoMaximo">Maximo valor de numeros del numerador</param>
        /// <returns>lista de codigos</returns>
        List<string> ObtenerListaCodigos<TMaestro>(long desde, int tamañoMaximo) where TMaestro : EntidadMaestro;

        List<string> ObtenerListaCodigos<TMaestro>(string prefijo, long desde, int tamañoMaximo) where TMaestro : EntidadMaestro;

    }

}
