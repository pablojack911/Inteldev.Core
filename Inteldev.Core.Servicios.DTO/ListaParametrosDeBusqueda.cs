using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    [DataContract]
    public class ListaParametrosDeBusqueda
    {
        public ListaParametrosDeBusqueda()
        {
            this.Parametros = new List<ParametrosMiniBusca>();
        }
        [DataMember]
        public List<ParametrosMiniBusca> Parametros { get; set; }
    }
}
