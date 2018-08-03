using System;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
	public interface IPresentadorMinibuscaList<TMaestro, TDetalle>
        where TMaestro : class,new()
		where TDetalle : Inteldev.Core.DTO.DTOBase, new()
	{
		string ID { get; set; }
        void PMB_CambioEntidad(object sender, ArgumentoGenerico<TDetalle> e);
		IPresentadorMiniBusca<TDetalle> PMB { get; set; }		
		IPresentadorMaestroDetalle<TMaestro, TDetalle> PMD { get; set; }
	}
}
