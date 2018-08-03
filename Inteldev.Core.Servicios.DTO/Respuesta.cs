using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO
{
    [DataContract]
    public class Respuesta<TEntidad> where TEntidad : DTOBase
    {

        private object datos;
        private string mensaje;
        private EstadoRespuesta estado;

        [DataMember]
        public string Mensaje { get { return mensaje; } set { this.mensaje = value; } }
        [DataMember]
        public object Datos { get { return this.datos; } set { this.datos = value; } }
        [DataMember]
        public EstadoRespuesta Estado { get { return this.estado; } set { this.estado = value; } }

    }
}
