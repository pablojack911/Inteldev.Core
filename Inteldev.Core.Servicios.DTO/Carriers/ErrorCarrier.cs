using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Carriers
{
	[DataContract]
	public class ErrorCarrier
	{
		[DataMember]
		public bool borroOk { get; set; }
		[DataMember]
		public error tipoError { get; set;}
		[DataMember]
		public string mensaje { get; set; }

		public ErrorCarrier()
        {
            borroOk = true;
        }

		
        public string getErrorMensaje()
        {
            return mensaje;
        }
		
        public void setError(bool error)
        {
            this.borroOk = error;
        }
		
        public void setTipoError(error error)
        {
            this.tipoError = error;
        }
		
        public void setMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }
    }
	
    public enum error
    {
		[EnumMember]
        Validacion,
		[EnumMember]
        ForeignKey
    }

	
}
