using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Practices.Unity;

namespace Inteldev.Core.Patrones
{

    /// <summary>
    /// Encargada de mapear objetos abstractos con objetos concretos y resolver dependencia.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto. T solo puede ser si es de tipo FabricaIoC</typeparam>
    public class FabricaIoC<T> : Singleton<T>
        where T : FabricaIoC<T>
    {

        /// <summary>
        /// Contenedor de Unity Container
        /// </summary>
        private IUnityContainer unityContainer;

        /// <summary>
        /// Constructor. Inicializa Unity.
        /// </summary>
        public FabricaIoC()
        {
            unityContainer = new UnityContainer();
            this.Registro();
        }


        public virtual void Registro()
        {


        }

        /// <summary>
        /// Mapea tipo abstracto con un tipo concreto de objeto
        /// </summary>
        /// <param name="abstracto">Tipo abstracto de objeto</param>
        /// <param name="concreto">Tipo concreto del objeto</param>
        public void Registrar(Type abstracto, Type concreto, InjectionMember[] valores = null)
        {
            if (valores == null)
                unityContainer.RegisterType(abstracto, concreto);
            else
                unityContainer.RegisterType(abstracto, concreto, valores);
        }

        public void RegistrarSingleton(Type abstracto, Type concreto)
        {
            unityContainer.RegisterType(abstracto, concreto, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Mapea un registro de fabricas
        /// </summary>
        /// <param name="registro">Lista con las fabricas registradas</param>
        /// <see cref="RegistroFabrica"/>
        public void CargarRegistro(RegistroFabrica registro)
        {
            foreach (var item in registro.Lista)
            {

                this.Registrar(item.Item1, item.Item2, item.Item4);
            }

            registro.ListaSingleton.ForEach(p => this.RegistrarSingleton(p.Item1, p.Item2));

        }

        /// <summary>
        /// Obtiene la instancia de un tipo determinado
        /// </summary>
        /// <typeparam name="TServicio"></typeparam>
        /// <returns>Instancia del objeto</returns>
        public TServicio Resolver<TServicio>() where TServicio : class
        {
            try
            {
                Debug.WriteLine("Intentando resolver.");
                return unityContainer.Resolve<TServicio>();
            }
            catch (Exception exc)
            {
                return null;
                Debug.Write(exc.Message);
            }


        }

        /// <summary>
        /// Obtiene la instancia de un determinado tipo de objeto. Resuelve la instancia.
        /// </summary>
        /// <param name="Tipo">tipo de objeto</param>
        /// <returns>instancia del objeto</returns>
        public object Resolver(Type Tipo)
        {
            try
            {
                Debug.WriteLine(string.Format("Intentando resolver para {0}.", Tipo.ToString()));
                return unityContainer.Resolve(Tipo);
            }
            catch (Exception exc)
            {
                Debug.Write(exc.Message);
                return null;
            }
        }

        public object Resolver(Type Tipo, string parametro, string ovverride)
        {
            try
            {
                Debug.WriteLine(string.Format("Intentando resolver para {0}..", Tipo.ToString()));
                return unityContainer.Resolve(Tipo, new ParameterOverride(parametro, ovverride));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public object Resolver(Type Tipo, ParameterOverride[] parameters)
        {
            try
            {
                Debug.WriteLine(string.Format(string.Format("Intentando resolver para {0}...", Tipo.ToString())));
                return unityContainer.Resolve(Tipo, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Registra una instancia para que cuando se resulva, use esta instancia y no cree una. 
        /// </summary>
        /// <typeparam name="TServicio"></typeparam>
        /// <param name="Instancia"></param>
        public void RegistrarInstancia<TServicio>(TServicio Instancia) where TServicio : class
        {
            unityContainer.RegisterInstance<TServicio>(Instancia, new ExternallyControlledLifetimeManager());

        }



        public void RegistrarInstancia(Type tipo, object instancia)
        {
            unityContainer.RegisterInstance(tipo, instancia, new ExternallyControlledLifetimeManager());

        }


        public static void _RegistraInstancia<TServicio>(TServicio Instancia) where TServicio : class
        {
            FabricaIoC<T>.Instancia.RegistrarInstancia<TServicio>(Instancia);
        }

        public static void _RegistraInstancia(Type tipo, object instancia)
        {
            FabricaIoC<T>.Instancia.RegistrarInstancia(tipo, instancia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServicio"></typeparam>
        /// <returns></returns>
        public static TServicio _Resolver<TServicio>() where TServicio : class
        {
            return FabricaIoC<T>.Instancia.Resolver<TServicio>();
        }

        /// <summary>
        /// Obtiene la instancia de un determinado tipo de objeto
        /// </summary>
        /// <param name="Tipo">tipo de objeto</param>
        /// <returns>instancia del objeto</returns>
        public static object _Resolver(Type Tipo)
        {
            return FabricaIoC<T>.Instancia.Resolver(Tipo);
        }

        public List<Tuple<string, string>> ObtenerRegistro()
        {
            var lista = new List<Tuple<string, string>>();
            foreach (var item in this.unityContainer.Registrations)
            {
                var registeredType = item.RegisteredType.ToString();
                if (registeredType.Contains("IDao") || registeredType.Contains("IGrabadorFox") || registeredType.Contains("MapeadorFox"))
                    lista.Add(new Tuple<string, string>(item.RegisteredType.ToString(), item.MappedToType.ToString()));
            }
            return lista;
        }
    }
}
