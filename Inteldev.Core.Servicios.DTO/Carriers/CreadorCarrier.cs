using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Carriers
{
    [DataContract]
    public class CreadorCarrier<TEntidad>
        where TEntidad : DTOBase
    {
        [DataMember]
        private TEntidad entidad;
        [DataMember]
        private bool error;
        [DataMember]
        private string mensaje;

        public CreadorCarrier()
        {
            this.error = false;
            this.mensaje = "";
        }

        public void SetError(bool error)
        {
            this.error = error;
        }

        public bool GetError()
        {
            return this.error;
        }

        public void SetEntidad(TEntidad Entidad)
        {
            this.entidad = Entidad;
        }

        public TEntidad GetEntidad()
        {
            return this.entidad;
        }

        public void SetMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }

        public string GetMensaje()
        {
            return this.mensaje;
        }
    }

}
