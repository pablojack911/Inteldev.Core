using Inteldev.Core.DTO.Carriers;
using System;

namespace Inteldev.Core.Presentacion.Comandos
{

    public class ComandoGrabar : RelayCommand
    {

        public ComandoGrabar(Func<object, object> _execute, Predicate<object> _canExecute)
            :base(_execute,_canExecute)
        {
            
        }

        public override void EvaluarResultado(object resultado)
        {            
            if (resultado is GrabadorCarrier)
            {
				var grabador = resultado as GrabadorCarrier;
				Mensajes.Aviso(grabador.getMensaje());
            }
        }
    }

}
