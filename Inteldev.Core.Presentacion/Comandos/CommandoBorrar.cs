using Inteldev.Core.DTO.Carriers;
using System;

namespace Inteldev.Core.Presentacion.Comandos
{

    public class CommandoBorrar : RelayCommand
    {

        public CommandoBorrar(Func<object, object> _execute, Predicate<object> _canExecute)
            : base(_execute, _canExecute, m => Dialogos.PreguntaSiNo("¿Borra estos datos?"))
        {

        }

        public override void EvaluarResultado(object resultado)
        {
            var result = resultado as ErrorCarrier;
            if (result != null)
                Mensajes.Aviso(result.getErrorMensaje());
        }
    }

}
