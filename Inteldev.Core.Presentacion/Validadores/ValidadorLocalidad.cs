using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Inteldev.Core.Presentacion.Validadores
{
	public class ValidadorLocalidad : ValidationRule
	{
		private int i;

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			var ra = new Random();
			i = (int)ra.Next();
			if (i != 6)
			{
				return new ValidationResult(
			false,
			string.Format(
				"Ahhh Puto", i));
			}
			else
				return new ValidationResult(
			false,
			string.Format(
				"Ahhh No funco", i));
		}
	}
}
