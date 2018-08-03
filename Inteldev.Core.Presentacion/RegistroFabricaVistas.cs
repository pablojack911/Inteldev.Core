using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Stock;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Patrones;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Presentacion
{
    /// <summary>
    /// Contiene el registro de las vistas a usar para cada unidad de negocio.
    /// Cuando se carga una vista, si no se especifica la unidad, se toma para todos.
    /// </summary>
    public abstract class RegistroFabricaVistas
    {
        public Dictionary<Type, Dictionary<Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio?, Type>> Vistas { get; set; }
        public Dictionary<Type, Type> VistasDefault { get; set; }
        public Dictionary<Tuple<Type, TipoVista>, Dictionary<Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio?, Type>> VistasComplejas { get; set; }
        public Dictionary<Tuple<Type, TipoVista>, Type> VistasComplejasDefault { get; set; }
        private Dictionary<Core.DTO.Organizacion.UnidadeDeNegocio?, Type> dictionary;

        public RegistroFabricaVistas()
        {
            this.dictionary = new Dictionary<UnidadeDeNegocio?, Type>();
            this.Vistas = new Dictionary<Type, Dictionary<Core.DTO.Organizacion.UnidadeDeNegocio?, Type>>();
            this.VistasDefault = new Dictionary<Type, Type>();
            this.VistasComplejas = new Dictionary<Tuple<Type, TipoVista>, Dictionary<UnidadeDeNegocio?, Type>>();
            this.VistasComplejasDefault = new Dictionary<Tuple<Type, TipoVista>, Type>();
            this.Registrar();
        }

        public abstract void Registrar();

        /// <summary>
        /// Solo funciona si queres registrar una vista con una unidad de negocio.
        /// </summary>
        /// <param name="nombre">nombre para poder buscarla</param>
        /// <param name="unidadDeNegocio">enum</param>
        /// <param name="Vista">vista de la unidad de negocio</param>
        public void Registro(Type nombre, UnidadeDeNegocio? unidadDeNegocio, Type Vista)
        {
            dictionary.Clear();
            dictionary.Add(unidadDeNegocio, Vista);
            Vistas.Add(nombre, dictionary);
        }

        /// <summary>
        /// Para vistas por defecto. No estan asociadas a ninguna unidad de negocio.
        /// </summary>
        /// <param name="nombre">nombre para poder buscarla</param>
        /// <param name="Vista">vista asociada al nombre</param>
        public void Registro(Type nombre, Type Vista)
        {
            this.VistasDefault.Add(nombre, Vista);
        }

        public void Registro(Type nombre, TipoVista tipoVista, UnidadeDeNegocio? unidadDeNegocio, Type vista)
        {
            dictionary.Clear();
            dictionary.Add(unidadDeNegocio, vista);
            this.VistasComplejas.Add(new Tuple<Type, TipoVista>(nombre, tipoVista), dictionary);
        }

        public void Registro(Type nombre, TipoVista tipoVista, Type vista)
        {
            this.VistasComplejasDefault.Add(new Tuple<Type, TipoVista>(nombre, tipoVista), vista);
        }

    }
}
