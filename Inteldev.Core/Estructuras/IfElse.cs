using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Estructuras
{
	/// <summary>
	/// Encapsula el uso de If/Else
	/// </summary>
    public class IfElse : IProceso
    {
		/// <summary>
		/// Metodo que evalua una condicion
		/// </summary>
		public Func<bool> Condicion { get; set; }

		/// <summary>
		/// Si la condicion es true, ejecuta este metodo
		/// </summary>
		public Action<Object> Entonces { get; set; }
		
		/// <summary>
		/// Si condicion es false, ejecuta este metodo
		/// </summary>
		public Action<Object> Sino { get; set; }

		
        public IfElse()
        {
        }

		
        public IfElse(Func<bool> condicion)
        {
            this.Condicion = condicion;
        }

		/// <summary>
		/// Constructor. Setea metodo condicion y metodo entonces
		/// </summary>
		/// <param name="condicion">metodo que evalua la condicion. True o False</param>
		/// <param name="entonces">Si condicion es false ejecutar este metodo</param>
        public IfElse(Func<bool> condicion, Action<Object> entonces)
        {
            this.Condicion = condicion;
            this.Entonces = entonces;
        }

		/// <summary>
		/// Ejecuta el if else
		/// </summary>
        public void Ejecutar()
        {
            if (this.Condicion()) 
            {
                Entonces(this);
            }
            else 
            {
                    Sino(this);
            }
        }

    }
}
