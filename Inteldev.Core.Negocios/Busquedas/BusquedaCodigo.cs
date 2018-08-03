using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Busquedas
{
    public class BusquedaCodigo<TEntidad> : ParteBusqueda<TEntidad>
        where TEntidad : EntidadBase
    {
        public override void Cargar(object Busqueda, string name)
        {
            this.Nombre = name;
            this.PuedeBuscar = (p => !string.IsNullOrEmpty(p.ToString()));
            this.SetearParteIzquierda(name);
            this.SetearParteDerecha(Busqueda,typeof(string));
            //this.JuntaExpressionContains();
            //this.JuntaExpressionIgual();
            this.JuntaExpressionEndsWith();
        }
        
    }
}
