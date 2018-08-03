using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Inteldev.Core.Estructuras
{
	/// <summary>
	/// Encapsula el uso de Try-Catch.
	/// </summary>
    public class TryCatch : IProceso
    {
		/// <summary>
		/// ¿Queres o no interceptar la exepcion?
		/// </summary>
        public bool InterceptarError { get; set; }
        
		/// <summary>
		/// Metodo a ejecutar. Recordar que solo debe tener un parametro y devolver void
		/// </summary>
        public Action<object> Try { get; set; }
        
		/// <summary>
		/// Metodo a ejecutar en caso de que se salte la exepcion
		/// </summary>
		public Action<Exception> Catch { get; set; }
        
		/// <summary>
		/// Metodo a ejecutar al finalizar el try-catch. Solo se ejecuta si esta en true
		/// <see cref="InterceptarError"/>
		/// </summary>
		public Action<object> Finally { get; set; }
        
		/// <summary>
        /// ¿Ocurrio alguna exepcion?
        /// </summary>
		public bool HuboError { get; set; }
        
		//constructor
		public TryCatch()
        {
            this.InterceptarError = true;
        }

		/// <summary>
		/// Constructor sobrecargado
		/// </summary>
		/// <param name="interceptarError">Si queres o no interceptar el error</param>
        public TryCatch(bool interceptarError)
        {
            this.InterceptarError = interceptarError;
        }

		/// <summary>
		/// Ejecuta el try-catch.
		/// Si hubo exepciones se registran en el log del debug y en atributos.
		/// <see cref="Catch"/>
		/// <see cref="Try"/>
		/// <see cref="Finally"/>
		/// </summary>
        public void Ejecutar()
        {
            this.HuboError = false;
            if (this.InterceptarError)
            {
                try
                {
                    this.Try(this);
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                    this.HuboError = true;
                    if (this.Catch != null) //porque no verifico esto contructor o metodo intentar??
                        this.Catch(exc);    
                }
                finally
                {
                    if(this.Finally != null)
                        this.Finally(this);
                }
            }
            else //no quiero interceptar el error
            {
                this.Try(this); //ejecuto metodo
            }
        }

		/// <summary>
		/// Constructor. Prueba metodo Try, captura la exepcion, pero si no hay metodo catch definido, no hace nada.
		/// </summary>
		/// <param name="Try">metodo a probar si tira o no una exepcion</param>
		/// <returns>resultado de la ejecucion de Try (no captura exepcion)</returns>
        public static bool Intentar(Action<object> Try)
        {
            return TryCatch.Intentar(Try, false);
        }

		/// <summary>
		/// Constructor. Prueba metodo Try. 
		/// </summary>
		/// <param name="Try">Metodo a probar</param>
		/// <param name="NoInterceptarError">False = la captura, pero no hace nada. True = captura la exepcion</param>
		/// <returns>Bool. Si capturo o no capturo la exepcion. (False en caso de captura)</returns>
        public static bool Intentar(Action<object> Try, bool NoInterceptarError)
        {                       
            var tc = new TryCatch( !NoInterceptarError) { Try = Try };        
            tc.Ejecutar();
            return !tc.HuboError;
        }

		/// <summary>
		/// Constructor. Prueba metodo Try. Si tira exepcion ejecuta metodo Catch 
		/// </summary>
		/// <param name="Try">Metodo a probar</param>
		/// <param name="Catch">Metodo a ejecutar en caso de exepcion</param>
		/// <returns>Bool. Si capturo o no capturo la exepcion. (False en caso de captura)</returns>
        public static bool Intentar(Action<object> Try,Action<object> Catch)
        {
            var tc = new TryCatch(false) { Try = Try, Catch= Catch };
            tc.Ejecutar();
            return !tc.HuboError;
        }

		/// <summary>
		/// Constructor. Prueba metodo Try. Si tira exepcion ejecuta metodo Catch. Ejecuta Finally. Ambos solamente si 
		/// parametro NoInterceptarError es False.
		/// </summary>
		/// <param name="Try">Metodo a prbar</param>
		/// <param name="Catch">Metodo a ejecutar en caso de exepcion</param>
		/// <param name="Finally">Metodo que ejecuta siempre (solo si NointerceptarError es False)</param>
		/// <param name="NoInterceptarError">False = la captura, pero no hace nada. True = captura la exepcion</param>
		/// <returns>Bool. Si capturo o no capturo la exepcion. (False en caso de captura)</returns>
        public static bool Intentar(Action<object> Try, Action<object> Catch, Action<object> Finally, bool NoInterceptarError)
        {
            var tc = new TryCatch(!NoInterceptarError) { Try = Try, Catch = Catch, Finally = Finally };
            tc.Ejecutar();
            return !tc.HuboError;
        }

		/// <summary>
		/// Constructor. Prueba metodo Try. Ejecuta siempre Finally solamente si parametro NoInterceptarError es False.
		/// </summary>
		/// <param name="Try">Metodo a probar</param>
		/// <param name="Finally">Metodo que ejecuta siempre (solo si NointerceptarError es False)</param>
		/// <param name="NoInterceptarError">False = la captura, pero no hace nada. True = captura la exepcion</param>
		/// <returns>Bool. Si capturo o no capturo la exepcion. (False en caso de captura)</returns>
        public static bool Intentar(Action<object> Try, Action<object> Finally, bool NoInterceptarError)
        {
            var tc = new TryCatch(!NoInterceptarError) { Try = Try, Finally = Finally };
            tc.Ejecutar();
            return !tc.HuboError;
        }
    }
}
