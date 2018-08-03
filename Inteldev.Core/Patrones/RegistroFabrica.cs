using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;

namespace Inteldev.Core.Patrones
{
	
	/// <summary>
	/// Clase abstracta que contiene contenedores de mapeos
	/// </summary>
	public abstract class RegistroFabrica
	{
		/// <summary>
		/// Lista que contiene las fabricas registradas
		/// </summary>
		public List<Tuple<Type, Type, string,InjectionMember[]>> Lista { get; set; }
		public List<Tuple<Type,Type>> ListaSingleton { get; set; }

		/// <summary>
		/// Constructor. Inicializa Lista.
		/// </summary>
		public RegistroFabrica()
		{
			this.Lista = new List<Tuple<Type, Type, string, InjectionMember[]>>();
			this.ListaSingleton = new List<Tuple<Type, Type>>();
			this.Configurar();
			
		}

		/// <summary>
		/// Registra una nueva fabrica guardandola en la lista de fabricas.
		/// </summary>
		/// <param name="tipoAbstracto">Tipo abstracto del objeto</param>
		/// <param name="TipoConcreto">Tipo concreto del objeto </param>
		/// <param name="Nombre">Nombre de la fabrica (Opcional)</param>
		public void Registrar(Type tipoAbstracto, Type TipoConcreto, string Nombre="")
		{
			this.Lista.Add(new Tuple<Type, Type, string, InjectionMember[]>(tipoAbstracto, TipoConcreto, Nombre,null));
		}

		public void Registrar(Type tipoAbstracto, Type TipoConcreto, params InyectaValor[] valores)
		{
			this.Lista.Add(new Tuple<Type, Type, string, InjectionMember[]>(tipoAbstracto, TipoConcreto, "" ,valores));
						
		}

		public void Registrar(Type tipoAbstracto, Type TipoConcreto, params InjectionConstructor[] valores)
		{
			this.Lista.Add(new Tuple<Type, Type, string, InjectionMember[]>(tipoAbstracto, TipoConcreto, "", valores));

		}

		public void RegistrarSingleton(Type tipoAbstracto, Type TipoConcreto)
		{
			this.ListaSingleton.Add(new Tuple<Type, Type>(tipoAbstracto, TipoConcreto));
		}

		//public void Registrar<TAbstracto, TConcreto>(Expression<Func<ISetPriperty, TConcreto, object>> valores)
		//    where TAbstracto : class
		//    where TConcreto : class
		//{
		//    var valorespordefecto = new Dictionary<string, object>();
		//    this.Lista.Add(new Tuple<Type, Type, string, Dictionary<string, object>>(typeof(TAbstracto), typeof(TConcreto), "", valorespordefecto));
		//}
		

		public abstract void Configurar();

					   
	}

	public interface ISetPriperty
	{
		ISetPropertyValue Pripiedad(Expression<Func<object,object>> prop);
	}

	public interface ISetPropertyValue
	{
		ISetPriperty Valor(object valor);
	}

	public class InyectaValor : InjectionProperty
	{
		public InyectaValor(string propiedad, object valor):base(propiedad,valor)
		{

		}
		public InyectaValor(string propiedad, Type resolver )
			: base(propiedad,new ResolvedParameter(resolver))
		{
			
			
		}
	}
}
