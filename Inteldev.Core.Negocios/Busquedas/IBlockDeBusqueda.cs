using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo;

namespace Inteldev.Core.Negocios.Busquedas
{

    public interface IBlockDeBusqueda<TEntidad> where TEntidad : EntidadBase   
    {

        IList<ParteBusqueda<TEntidad>> ObtenerPartes();
		void AgregarPartes( );
        void AgregarPartes(List<object> listaPropiedades, List<Inteldev.Core.Modelo.ParametrosMiniBusca> Parametros);
		object Busqueda { get; set; }
		List<ParteBusqueda<TEntidad>> Partes { get; set; }
    }

}
