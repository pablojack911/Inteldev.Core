using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Busquedas
{
	public class BusquedaStringEquals<TEntidad> : ParteBusqueda<TEntidad>
		where TEntidad : EntidadBase
	{
        public override void Cargar(object Busqueda, string name)
		{
			this.Nombre = name;
			this.PuedeBuscar = (p => !string.IsNullOrEmpty(p.ToString()) && p.ToString().Length > 3);
			this.SetearParteIzquierda(name);
			this.SetearParteDerecha(Busqueda.ToString(),typeof(string));
			this.JuntaExpressionIgual();
		}
	}
}
