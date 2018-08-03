using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo
{
	/// <summary>
	/// Provee dos metodos que tienen que tener las entidades y provee un inicializador de colleciones.
	/// </summary>
	public class EntidadBase
	{
		public int Id { get; set; }
		public string Nombre { get; set; }

		public EntidadBase()
		{
			//this.InicializarColecciones();			
		}

		/// <summary>
		/// Busca en las propiedades de la entidad collecciones y las inicializa para que no halla problemas.
		/// </summary>
		private void InicializarColecciones()
		{
			var props = this.GetType().GetProperties();
			foreach (var prop in props)
			{
				
				if (prop.PropertyType.GetProperty("Count") != null)
				{                                       
					Type typeList = typeof(List<>);
					Type actualType = typeList.MakeGenericType(prop.PropertyType.GetGenericArguments());                    
					prop.SetValue(this, Activator.CreateInstance(actualType), null);
				}
			}
		}
		
	}

}
