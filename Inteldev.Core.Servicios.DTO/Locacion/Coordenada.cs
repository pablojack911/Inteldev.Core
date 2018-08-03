using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Inteldev.Core.DTO.Locacion
{
    public class Coordenada : DTOBase
    {
        [DataMember]
        public double Latitud { get; set; }
        [DataMember]
        public double Longitud { get; set; }

        public override string ToString()
        {
            if (Latitud == 0 && Longitud == 0)
                return "";
            else
                return string.Format("Lat:{0} - Lng:{1}", Latitud, Longitud);
        }
    }
}
