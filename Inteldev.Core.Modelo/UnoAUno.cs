using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Modelo
{
	/// <summary>
	/// este atributo sirve para indicar cuando una entidad tiene que ser recorrida para incluir las foreign key
	/// que posee dentro. ("WTF? No, en realidad... esto indica que la relacion de la Entidad - SubEntidad es Uno a Uno, o sea, no una coleccion ni una FK." Pocho) 
	/// </summary>
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
	public sealed class UnoAUno : Attribute
	{
		public UnoAUno( ) {}
	}
}
