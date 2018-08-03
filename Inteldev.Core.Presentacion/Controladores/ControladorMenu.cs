using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.DTO.Menu;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using System.Windows;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.DTO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Inteldev.Core.Contratos;
using System.Windows.Controls;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.DTO.Organizacion;

namespace Inteldev.Core.Presentacion.Controladores
{
    /// <summary>
    /// Clase encargada de crear, asociar, y lanzar las opciones del menu.
    /// </summary>
    public class ControladorMenu
    {
        //public Dictionary<Type, IPresentadorABM> Presentadores { get; set; }

        /// <summary>
        /// Constructor. Si no hay un menu asociado a un comando, lanza opcion en construccion. 
        /// </summary>
        public ControladorMenu()
        {
            this.Opciones = new SwitchCase<string>();
            this.Opciones.CasoPorDefecto = (c => MessageBox.Show("Opcion en Construccion"));
            //this.Presentadores = new Dictionary<Type, IPresentadorABM>();
        }

        /// <summary>
        /// Lista con las opciones de menu asociadas a un usuario.
        /// </summary>
        public Func<Usuario, UnidadeDeNegocio?, List<OpcionMenu>> ServicioCargarMenu { get; set; }

        /// <summary>
        /// Carga las opciones de menu que tiene asociado un determinado usuario. Util para distintos permisos de usuarios.
        /// </summary>
        /// <param name="usuario">Objecto Usuario</param>
        /// <param name="unidadActual">Unidad de Negocio Actua</param>
        /// <returns>La collecion de opciones de menu que puede usar.</returns>
        public ICollection<OpcionMenu> Cargar(Usuario usuario, UnidadeDeNegocio? unidadActual)
        {
            var menu = ServicioCargarMenu(usuario, unidadActual).Where(p => p.Nombre == "Raiz").ToList()[0].Opciones;
            return menu;
        }

        /// <summary>
        /// Ejecuta una opcion. 
        /// </summary>
        /// <param name="p">Nombre de la opcion del menu a ejecutar</param>
        public void Ejecutar(string p)
        {
            this.Opciones.Valor = p;
            this.Opciones.Ejecutar();
        }

        /// <summary>
        /// Agrega una opcion al menu. 
        /// </summary>
        /// <param name="entradaMenu">Nombre de la opcion</param>
        /// <param name="accion">accion a ejecutar cuando se clickeea </param>
        public void AgregarOpcion(string entradaMenu, Action<object> accion)
        {
            this.Opciones.AgregarCaso(entradaMenu, accion);
        }
        public void AgregarOpcion(string entradaMenu, Type typePresentador)
        {
            //Action<string, Type> accion = delegate(string modulo, Type tp)
            //{
            //    IPresentadorABM presentador = null;
            //    if (!this.Presentadores.ContainsKey(tp) || this.Presentadores.FirstOrDefault(p => p.Key == tp).Value == null)
            //    {
            //        presentador = Activator.CreateInstance(tp) as IPresentadorABM;
            //        this.Presentadores.Add(tp, presentador);
            //    }
            //    else
            //    {
            //        presentador = this.Presentadores.FirstOrDefault(p => p.Key == tp).Value;
            //    }
            //    presentador.Titulo = modulo;
            //    presentador.Ejecutar();
            //};
            Action<string, Type> accion = delegate(string modulo, Type tp)
            {
//                IPresentadorABM presentador = null;
                //if (!this.Presentadores.ContainsKey(tp) || this.Presentadores.FirstOrDefault(p => p.Key == tp).Value == null)
                //{
                IPresentadorABM presentador = Activator.CreateInstance(tp) as IPresentadorABM;
                    //this.Presentadores.Add(tp, presentador);
                //}
                //else
                //{
                //    presentador = this.Presentadores.FirstOrDefault(p => p.Key == tp).Value;
                //}
                
                presentador.Titulo = modulo;
                presentador.Ejecutar();
            };

            this.Opciones.AgregarCaso(entradaMenu, m => accion(((SwitchCase<string>)m).Valor, typePresentador));

        }

        /// <summary>
        /// Limpia la lista de menues 
        /// </summary>
        public void LimpiarOpciones()
        {
            this.Opciones.Limpiar();
        }

        /// <summary>
        /// Opciones que tiene el menu.
        /// </summary>
        SwitchCase<string> Opciones { get; set; }

    }
}
