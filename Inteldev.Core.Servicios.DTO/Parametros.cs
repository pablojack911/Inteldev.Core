using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    [DataContract]
    public class Parametros<TEntidad> where TEntidad : DTOBase
    {
        private TEntidad entidad;
        private RangoFecha rangoFecha;

        [DataMember]
        public TEntidad Entidad { get { return this.entidad; } set { this.entidad = value; } }
        [DataMember]
        public RangoFecha RangoFecha { get { return rangoFecha; } set { this.rangoFecha = value; } }
    }
}
