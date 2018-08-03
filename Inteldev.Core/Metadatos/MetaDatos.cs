using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Inteldev.Core.Metadatos
{
	/// <summary>
	/// Obtiene ciertos datos extra de los objetos.
	/// </summary>
	public static class MetaDatos
	{
		/// <summary>
		/// Obtiene el valor de una propiedad de un objeto
		/// </summary>
		/// <typeparam name="TResult">Tipo de dato de propiedad</typeparam>
		/// <param name="objeto">objeo a consultar valor de una propiedad</param>
		/// <param name="miembro">nombre de la propiedad</param>
		/// <returns>el valor de la propiedad</returns>
		public static TResult ObtenerValor<TResult>(object objeto, string miembro) 
		{
			object resultado = null;
			var infoProp = objeto.GetType().GetProperty(miembro);
			if (infoProp != null)
			{
				if (infoProp.CanRead)
				   resultado = infoProp.GetValue(objeto, null);
			}

			Type t = typeof(TResult);
			if (resultado != null)
			{
				if (t.Name==typeof(object).Name)                    
					return (TResult)resultado;
				else 
					return (TResult)Convert.ChangeType(resultado, t);
			}

			return default(TResult);

		}

		/// <summary>
		/// Obtiene el valor de una propiedad.
		/// </summary>
		/// <typeparam name="TObject">tipo de objeto a consultar propiedad</typeparam>
		/// <typeparam name="TResult">tipo de dato de la propiedad a consultar</typeparam>
		/// <param name="objeto">objeto a consultar</param>
		/// <param name="propiedad"></param>
		/// <returns>valor de la propiedad</returns>
		public static TResult ObtenerValor<TObject,TResult>(object objeto, Expression<Func<TObject,object>> propiedad)
		{
			return MetaDatos.ObtenerValor<TResult>(objeto,MetaDatos.NombrePropiedad<TObject>(propiedad));
		}

		/// <summary>
		/// Asigna un valor a una propiedad
		/// </summary>
		/// <param name="objeto">objeto a asignar valor</param>
		/// <param name="miembro">nombre de la propiedad</param>
		/// <param name="valor">valor a asignar</param>
		public static void AsignarValor(object objeto, string miembro, object valor)
		{
			var infoProp = objeto.GetType().GetProperty(miembro);
			if (infoProp.CanWrite)
			{
				//if (infoProp.PropertyType.GetProperty("Count") == null)
				infoProp.SetValue(objeto, valor, null);
				//else
				//{
				//    var lista = Activator.CreateInstance(infoProp.PropertyType,valor);
				//    infoProp.SetValue(objeto,lista , null);
				//}
			}
		}

		/// <summary>
		/// Para cada propiedad de un objeto realizar una accion
		/// </summary>
		/// <typeparam name="TObject">tipo de objeto</typeparam>
		/// <param name="objeto">objeto sobre cual aplicar el metodo</param>
		/// <param name="accion">metodo a aplicar sobre las propiedades del objeto</param>
		public static void ForEachPropertys<TObject>(TObject objeto, Action<PropertyInfo> accion)
		{
			var props = objeto.GetType().GetProperties();
			foreach (var prop in props)
			{
				accion(prop);
			}
		}

		/// <summary>
		/// Crear instancia de un tipo determinado
		/// </summary>
		/// <param name="tipo">tipo de objeto a crear instancia</param>
		/// <returns>instancia</returns>
		public static object CrearInstancia(Type tipo)
		{
			if (tipo.GetProperty("Count") != null)
			{
				Type typeList = typeof(List<>);
				Type actualType = typeList.MakeGenericType(tipo.GetGenericArguments());
				return Activator.CreateInstance(actualType);
			}
			else
			{
				return Activator.CreateInstance(tipo);
			}

		}

		//preguntar, porque hace otro metodo si el anterior tambien crea collecciones. 
		public static object CrearInstanciaColleccion(Type tipo)
		{
			object list = null;
			if (tipo.GetProperty("Count") != null)
			{
				Type typeList = typeof(List<>);
				Type actualType = typeList.MakeGenericType(tipo.GetGenericArguments());
				list= Activator.CreateInstance(actualType);
			}
			return list;
		}
		
		/// <summary>
		/// Obtiene el nombre de una propiedad dada la funcion que devuelve la propiedad.
		/// </summary>
		/// <typeparam name="TObject">tipo de objeto</typeparam>
		/// <param name="propiedad">funcion que devuelve el objeto de la propiedad</param>
		/// <returns></returns>
		public static string NombrePropiedad<TObject>(Expression<Func<TObject, object>> propiedad)
		{
			
			PropertyInfo propertyInfo = null;
			if (propiedad.Body is MemberExpression)
			{
				propertyInfo = (propiedad.Body as MemberExpression).Member as PropertyInfo;
			}
			else
			{
				propertyInfo = (((UnaryExpression)propiedad.Body).Operand as MemberExpression).Member as PropertyInfo;
			}
		
			return propertyInfo.Name;            
		}
	}
}
