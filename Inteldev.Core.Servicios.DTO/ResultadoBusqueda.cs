using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.DTO
{

    public class ResultadoBusqueda<TDto> where TDto : class
    {
        
		public string Nombre { get; set; }
        
		public List<TDto> Lista { get; set; }
        
		public int CantidadDeItems
        {
            get
            {
                return Lista.Count;
            }
        }
    }

}
