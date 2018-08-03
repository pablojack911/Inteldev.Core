using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO.Validaciones
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class ValidadorAtributo : Attribute
	{
		public Type TipoValidador { get; set; }

		public ValidadorAtributo(Type Validador)
		{
			this.TipoValidador = Validador;
		}
	}
}
