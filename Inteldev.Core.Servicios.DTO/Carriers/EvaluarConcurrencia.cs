using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Carriers
{
	[DataContract]
	public class EvaluarConcurrencia
	{
		[DataMember]
		public bool huboConcurrencia { get; set; }
		[DataMember]
		public List<ValoresDeConcurrencia> resultadoDeConcurrencia { get; set; }
	}
}
