using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Inteldev.Core.Estructuras
{
	/// <summary>
	/// Encapsula el uso de Switch-Case
	/// </summary>
	/// <exception cref="NullReferenceExeption">Cuando alguna propiedad es null</exception>
	/// <typeparam name="TValor">Tipo de datos de los casos</typeparam>
    public class SwitchCase<TValor> : IProceso
    {

		/// <summary>
		/// Diccionario con los valores y las acciones correspondientes a cada caso
		/// </summary>
        private Dictionary<TValor, Action<Object>> casos = new Dictionary<TValor, Action<object>>();
        
		/// <summary>
		/// Accion a tomar en caso de no encontrar el caso en la lista de casos
		/// </summary>
		public Action<object> CasoPorDefecto { get; set; }
        
		/// <summary>
		/// Valor a buscar dentro de los casos
		/// </summary>
		public TValor Valor { get; set; }
        
		/// <summary>
		/// Agregar un caso nuevo. Es como poner un case nuevo dentro del switch.
		/// </summary>
		/// <param name="valor">valor del case</param>
		/// <param name="accion">que metodo se tiene que ejecutar. Solo debe tener 1 parametro y devolver void</param>
		public void AgregarCaso(TValor valor, Action<object> accion)
        {
            this.casos.Add(valor, accion);
        }

		/// <summary>
		/// Limpia la lista de casos
		/// </summary>
		public void Limpiar( )
		{
			this.casos.Clear();
		}

		/// <summary>
		/// Busca el Valor dentro de los posibles casos. Si no se encuentra se ejecuta caso por defecto
		/// </summary>
        public void Ejecutar()
        {
			Action<object> caso;
			try
			{
				caso = casos[this.Valor];
				caso(this);
			}
			catch (KeyNotFoundException e)
			{
				CasoPorDefecto(this);
			}
        }
    }
}
