using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Datos
{
	/// <summary>
	/// Clase encargada de evaluar y guardar las propiedades y valores que ocasionaron un error de concurrencia en la 
	/// base de datos.
	/// </summary>
	public class EvaluarConcurrencia
	{
		#region private atributes
		private bool huboConcurrencia;
		private List<ValoresDeConcurrencia> resultadoDeConcurrencia;
		/// <summary>
		/// Clase que contiene el valor original, el persistido, el que quiere grabar el cliente y el nombre de 
		/// la propiedad que causo la exepcion.
		/// </summary>
		public class ValoresDeConcurrencia 
		{
			private object valorOriginal;
			private object nuevoValor;
			private object valorPersistido;
			private string nombrePropiedad;

			#region public subclass wrappers
			public object ValorOriginal
			{
				set { valorOriginal = value; }
				get { return valorOriginal; }
			}

			public object NuevoValor
			{
				set { nuevoValor = value; }
				get { return nuevoValor; }
			}

			public object ValorPersistido
			{
				set { valorPersistido = value; }
				get { return valorPersistido; }
			}

			public string NombrePropiedad
			{
				set { nombrePropiedad = value; }
				get { return nombrePropiedad; }
			}
			#endregion

		}
		private IEnumerable<DbEntityEntry> entries;
		#endregion
		
		#region public wrappers

		public bool HuboConcurrencia
		{
			set { huboConcurrencia = value; }
			get { return huboConcurrencia; }
		}
		/// <summary>
		/// Contiene una lista de Valores de Concurrencia.
		/// <see cref="ValoresDeConcurrencia"/>
		/// </summary>
		public List<ValoresDeConcurrencia> ResultadoDeConcurrencia
		{
			set { resultadoDeConcurrencia = value; }
			get { return resultadoDeConcurrencia; }
		}
		#endregion

		public EvaluarConcurrencia()
		{

		}

		//constructor
		/// <summary>
		/// <exeption cref="System.ArgumentException">If ex is null</exeption>
		/// </summary>
		/// <param name="ex"></param>
		public EvaluarConcurrencia(DbUpdateConcurrencyException ex)
		{
			if (ex == null)
				throw new System.ArgumentException("Parameter cannot be null","ex");
			entries = ex.Entries;
			huboConcurrencia = true;
			if (entries.Count() == 0)
				huboConcurrencia = false;
		}
		/// <summary>
		/// Carga la lista con los valores de las entidades que ocasionaron la excepcion
		/// </summary>
		/// <param name="Resultado">Lista generica vacia instanciada.</param>
		public void ObtenerResultado(List<ValoresDeConcurrencia> Resultado)
		{
			if (Resultado == null)
			{
				throw new System.ArgumentException("Parameter cannot be null","resultado");
			}
			//recorro las entidades que no se pudieron guardar.
			foreach (var entity in entries)
			{
				ValoresDeConcurrencia valores = new ValoresDeConcurrencia();
				//comparo 2 nada mas. El error es cuando no coincide el persistido con el que lei
				foreach (var originalProperty in entity.GetDatabaseValues().PropertyNames)
				{
					//valor que tenia la base de datos. 
					valores.ValorPersistido = entity.GetDatabaseValues().GetValue<object>(originalProperty);
					valores.ValorOriginal = entity.OriginalValues.GetValue<object>(originalProperty);
					valores.NuevoValor = entity.CurrentValues.GetValue<object>(originalProperty);
					valores.NombrePropiedad = originalProperty;
					if (valores.ValorOriginal != valores.ValorPersistido)
					{
						Resultado.Add(valores);
					}
				}
			}
		}
	}
}
