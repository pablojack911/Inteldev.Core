using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
	public interface IPresentadorDomicilio
	{
		List<Calle> Calles { get; set; }
		Inteldev.Core.DTO.Locacion.Domicilio Item { get; set; }
	}
}
