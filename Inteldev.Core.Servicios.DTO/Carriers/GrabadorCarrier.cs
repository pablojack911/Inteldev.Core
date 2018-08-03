using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Carriers
{
    [DataContract]
	public class GrabadorCarrier
	{
		[DataMember]
		private string mensaje;
		[DataMember]
		private bool error;
		[DataMember]
		private EvaluarConcurrencia concurrencia;
        [DataMember]
        private int id;
		public GrabadorCarrier()
		{
            this.error = false;
            this.mensaje = "Datos Grabados correctamente";
		}

		public string getMensaje()
		{
			return this.mensaje;
		}
		public void setMensaje(string mensaje)
		{
			this.mensaje = mensaje;
		}

		public bool getError()
		{
			return this.error;
		}

		public void setError(bool error)
		{
			this.error = error;
		}

		public void setConcurrencia(EvaluarConcurrencia evaluarConcurrencia)
		{
			this.concurrencia = evaluarConcurrencia;
		}

		public EvaluarConcurrencia getConcurrencia()
		{
			return this.concurrencia;
		}

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

	}
}
