using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Patrones
{
	/// <summary>
	/// Encapsula el patron Singleton
	/// </summary>
	/// <typeparam name="T">clase que implementa singleton</typeparam>
    public class Singleton<T> where T: Singleton<T>
    {
		protected Singleton()
        {
            
        }

        private static T _instancia = (T)Activator.CreateInstance(typeof(T), true);
        public static T Instancia
        {
            get { return _instancia; }
        }
                
    }
            
}
