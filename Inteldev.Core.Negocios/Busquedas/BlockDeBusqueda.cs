using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Inteldev.Core.Modelo;

namespace Inteldev.Core.Negocios.Busquedas
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEntidad"></typeparam>
    public abstract class BlockDeBusqueda<TEntidad> : IBlockDeBusqueda<TEntidad> where TEntidad : EntidadBase   
    {
		/// <summary>
		/// Lista que contiene las partes de la busqueda. Es decir, sobre que campos quiero buscar, sus condiciones
		/// y si puede buscar.
		/// </summary>
		/// <seealso cref="ParteBusqueda"/>
		/// <seealso cref="EntidadBase"/>
        public List<ParteBusqueda<TEntidad>> Partes { get; set;}

		private object busqueda;

		/// <summary>
		/// Objeto que contiene la busqueda que se quiere realizar. Normalmente es un strin
		/// </summary>
		public object Busqueda { 
			get 
		{
			if (busqueda == null)
				throw new ArgumentNullException("Se debe inicializar busqueda");
			return busqueda;
		}
			set { busqueda = value; }
		}
		
		/// <summary>
		/// Constructor. Inicializa.
		/// </summary>
        public BlockDeBusqueda()
        {
            this.Partes = new List<ParteBusqueda<TEntidad>>();
        }

		/// <summary>
		/// Agrega una parte a la lista.
		/// </summary>
		/// <param name="partes">lista con las partes. Debe ser implementacion de ParteBusqueda</param>
		/// <seealso cref="ParteBusqueda"/>
		/// <seealso cref="EntidadBase"/>
        public abstract void AgregarPartes();

        public abstract void AgregarPartes(List<object> listaPropiedades, List<Inteldev.Core.Modelo.ParametrosMiniBusca> Parametros);

		///// <summary>
		///// Crea y agrega una parte a la lista.
		///// </summary>
		///// <param name="nombre">Nombre del campo a buscar</param>
		///// <param name="condicion">Condicion de busqueda (where)</param>
		//protected void CrearParte(string nombre)
		//{
		//	this.Partes.Add(new ParteBusqueda<TEntidad>() { Nombre = nombre });
		//}

		/// <summary>
		/// Wrapper publico sobre Partes.
		/// </summary>
		/// <returns>Lista de partes</returns>
        public IList<ParteBusqueda<TEntidad>> ObtenerPartes()
        {
            return this.Partes;
        }
    }

}
