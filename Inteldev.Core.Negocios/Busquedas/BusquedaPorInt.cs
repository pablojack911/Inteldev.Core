using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Busquedas
{
	public class BusquedaPorInt<TEntidad> : ParteBusqueda<TEntidad> 
		where TEntidad : EntidadBase 
	{

		public override void Cargar(object busqueda, string name)
		{
            //this.Nombre = name;
            //this.PuedeBuscar = (p => int.TryParse(p.ToString(), out this.Busqueda));
            ////le dice que queremos buscar por el id
            //this.AgregaParteIzquierdaBuscarPor(name, typeof(TEntidad));
            //int id = 0;
            //int.TryParse(busqueda.ToString(), out id);
            //this.tipoBusqueda = typeof(int);
            //this.busqueda = id;
            ////le dice que queremos que el texto de la busqueda sea igual que el id.
            //this.CondicionWhereEqual();
            this.Nombre = name;
            int Busqueda;
            this.PuedeBuscar = (p => int.TryParse(p.ToString(), out Busqueda));
            //le dice que queremos buscar por el id
            this.SetearParteIzquierda(name);
            int id = 0;
            int.TryParse(busqueda.ToString(), out id);
            Busqueda = id;
            //le dice que queremos que el texto de la busqueda sea igual que el id.
            this.SetearParteDerecha(id,typeof(int));
            this.JuntaExpressionIgual();
		}
        
	}
}
