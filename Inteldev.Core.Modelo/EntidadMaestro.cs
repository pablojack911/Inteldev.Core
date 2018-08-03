using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo
{
	public class EntidadMaestro : EntidadBase
	{
        [Index(IsUnique = false)]
        [MaxLength(30)]
		public string Codigo { get; set; }
	}
}
