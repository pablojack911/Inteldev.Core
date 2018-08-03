using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Busquedas
{
    /// <summary>
    /// Hace consultas dinamicas
    /// </summary>
    public class ParteBusqueda<TEntidad>
        where TEntidad : EntidadBase
    {
        /// <summary>
        /// Tipo de entidad sobre la cual se realiza la busqueda. Teniendo en cuenta un ejemplo, si
        /// yo quiero buscar provinciaId en proveedor. Proveedor es el tipo de entidad.
        /// </summary>
        private Type tipoEntidad;

        public string Nombre { get; set; }

        /// <summary>
        /// parte izquierda de la expression. Ejemplo: Si lo que queremos buscar es provinciaId = 1. 
        /// La parte izquierda es ProvinciaId
        /// </summary>
        private Expression left;
        /// <summary>
        /// Parte derecha de la expression. Ejemplo: Si lo que queremos buscar es provinciaId = 1
        /// 1 es la parte derecha.
        /// </summary>
        private Expression right;

        /// <summary>
        /// Resultado de la expression. Seria la expresion de juntar las partes izquierda y derecha. 
        /// Entonces quedaria provinciaId = 1. Siendo provinciaId la parte izquierda, y 1 la parte derecha
        /// </summary>
        private Expression result;

        /// <summary>
        /// Pensando en una expression lamba p=>p.provinciaId = 1. pe es el p de la expression lambda
        /// </summary>
        private ParameterExpression pe;

        /// <summary>
        /// Funcion que devuelve bool para saber si puede o no buscar.
        /// </summary>
        public Predicate<object> PuedeBuscar { get; set; }

        /// <summary>
        /// Setea el tipo de entidad sobre el cual se va a hacer la busqueda.
        /// </summary>
        public ParteBusqueda()
        {
            this.tipoEntidad = typeof(TEntidad);
            this.pe = Expression.Parameter(this.tipoEntidad, "p");
        }

        public virtual void Cargar(object Busqueda, string name)
        {

        }


        public void SetearParteIzquierda(string propiedad)
        {
            var value = this.tipoEntidad.GetProperty(propiedad);
            if (value != null)
                this.left = Expression.Property(pe, value);
        }

        /// <summary>
        /// Agrega una parte de busqueda para buscar sobre un campo especifico de un articulo
        /// </summary>
        /// <param name="propiedad">Propiedad que contiene el elemento que quiero buscar. Ejemplo: Proveedor</param>
        /// <param name="subPropiedad">Propiedad por la que busco. Ejemplo: ID si es que quiero buscar Id de 
        /// proveedor. </param>
        /// <param name="objetoDestino">Tipo de objeto. Ejemplo: el tipo de objeto de Proveedor. </param>
        /// <param name="objetoOrigen">El tipo de objeto que contiene el objeto Destino. Ejemplo: Articulo contiene
        /// a Proveedor. En este caso objeto Origen es Articulo</param>
        public void SetearParteIzquierda(string propiedad, string subPropiedad, Type objetoDestino)
        {
            left = Expression.Property(pe, this.tipoEntidad.GetProperty(propiedad));
            this.anidaPropiedad(objetoDestino, subPropiedad);
        }

        /// <summary>
        /// Agrega una condicion al where. Por si queres buscar por mas de una condicion. 
        /// </summary>
        /// <param name="propiedad"></param>
        /// <param name="valor"></param>
        /// <param name="tipo"></param>
        public void AgregaCondicionAnd(string propiedad, object valor, string tipo)
        {
            var res = this.GetResult();
            var right = this.right;
            this.SetearParteIzquierda(propiedad);
            this.SetearParteDerecha(valor, tipo);
            this.JuntaExpressionIgual();
            this.AnidarCondicionAnd(res, this.GetResult());
            this.right = right;
        }

        /// <summary>
        /// Anida una expresion. Basicamente lo que hace sirve para buscar entidades de entidades especificas. 
        /// Ejemplo: Siguiendo siempre con el caso de que quiero buscar un proveedor dentro de un articulo. 
        /// Si quiero buscar una entidad que tiene el campo id dentro de proveedor, entonces, primero usas
        /// agregaParteBuscarPor para buscar en proveedor, y luego llamas a anidaPropiedad para buscar dentro de la 
        /// propiedad de la propiedad de un proveedor.
        /// </summary>
        /// <param name="objetoDestino"></param>
        /// <param name="subPropiedad"></param>
        public void anidaPropiedad(Type objetoDestino, string subPropiedad)
        {
            left = Expression.Property(left, objetoDestino.GetProperty(subPropiedad));
        }

        /// <summary>
        /// Setea la parte derecha de la busqueda. Por ahora solo acepta string
        /// </summary>
        /// <param name="busqueda">busqueda de la comparacion</param>
        public void SetearParteDerecha(object busqueda, Type Tipo)
        {
            this.right = Expression.Constant(busqueda, Tipo);
        }

        public void SetearParteDerecha(object busqueda, string Tipo)
        {
            Type tipo = Type.GetType(Tipo);
            if (tipo == typeof(int?))
            {
                int id;
                int.TryParse(busqueda.ToString(), out id);
                var result = (int?)id;
                this.right = Expression.Constant(result, tipo);
            }
            else
            {
                var busq = Convert.ChangeType(busqueda, tipo);
                this.right = Expression.Constant(busq, tipo);
            }
        }

        /// <summary>
        /// Junta los expression left y right con la condicion "=". Por ejemplo provinciaId = 1. Parte izquierda
        /// es provinciaId, parteDerecha 1. Junto los dos con un =. Y lo mando a result
        /// </summary>
        public void JuntaExpressionIgual()
        {
            if (left != null && right != null)
                this.result = Expression.Equal(left, right);
        }

        /// <summary>
        /// idem JuntaExpressionIgual, pero con un contains
        /// </summary>
        public void JuntaExpressionContains()
        {
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            this.result = Expression.Call(left, method, this.right);
        }

        public Expression GetResult()
        {
            return this.result;
        }

        public void AnidarCondicionAnd(Expression e1, Expression e2)
        {
            this.result = Expression.And(e1, e2);
        }

        /// <summary>
        /// idem JuntaExpressionContains pero busca que finalice con 
        /// </summary>
        public void JuntaExpressionEndsWith()
        {
            MethodInfo method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            this.result = Expression.Call(left, method, this.right);
        }

        /// <summary>
        /// idem JuntaExpressionEndsWith pero que empiece con
        /// </summary>
        public void JuntaExpressionStartsWith()
        {
            MethodInfo method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            this.result = Expression.Call(left, method, this.right);
        }

        public void JuntaExpressionEspecial(Expression query)
        {
            this.result = query;
        }

        /// <summary>
        /// Arma la consulta a buscar. Nada mas
        /// </summary>
        /// <param name="queryableData">Necesito el IQueryable para poder armar la expresion</param>
        /// <returns>La consulta a buscar o null si no puede buscar</returns>
        public MethodCallExpression ArmaConsulta(IQueryable queryableData)
        {
            if (this.result != null)
            {
                //armo expresion final
                MethodCallExpression whereCallExpression = Expression.Call(
                  typeof(Queryable),
                  "Where",
                  new Type[] { queryableData.ElementType },
                  queryableData.Expression,
                  Expression.Lambda<Func<TEntidad, bool>>(result, new ParameterExpression[] { this.pe }));
                if (PuedeBuscar != null && PuedeBuscar(this.right))
                {
                    return whereCallExpression;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
