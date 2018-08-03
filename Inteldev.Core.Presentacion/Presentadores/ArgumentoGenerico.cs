using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Presentacion.Presentadores
{
	public class ArgumentoGenerico<TEntidad> : EventArgs
		where TEntidad : DTOBase
	{
		private TEntidad Entidad;

		public ArgumentoGenerico(TEntidad Entidad )
		{
			this.Entidad = Entidad;
		}

		public TEntidad GET( )
		{
			return Entidad;
		}

	}
}
