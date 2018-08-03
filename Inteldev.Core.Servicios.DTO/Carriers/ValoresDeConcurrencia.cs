using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Carriers
{
	[DataContract]
	public class ValoresDeConcurrencia
	{
		[DataMember]
		public object valorOriginal { get; set; }
		[DataMember]
		public object nuevoValor { get; set; }
		[DataMember]
		public object valorPersistido { get; set; }
		[DataMember]
		public string nombrePropiedad { get; set; }
	}
}
