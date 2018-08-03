using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Servicios.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hola");
			BuscadorInicial buscador = new BuscadorInicial();
			Inteldev.Core.Presentacion.Presentadores.PresentadorBuscador<Localidad> presentador = new Inteldev.Core.Presentacion.Presentadores.PresentadorBuscador<Localidad>();
			buscador.dataContext = presentador;
			buscador.InitializeComponent();
			Console.WriteLine("Y que hacemo que no inicializamo??");
		}
	}
}
