using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Estructuras
{
    public class If
    {
        
        public static IIfNull Null(object valor)
        {
            var valornull = new IfNull(valor);
            return valornull;
        }
            
    }

    public class IfNull : IIfNull
    {
        object valor;
        public IfNull(object valor)
        {
            this.valor = valor;
        }


        public object Devolver(object dev)
        {
            object retorno;
            if (this.valor == null)
                retorno = dev;
            else
            {
                try
                {
                    retorno = this.valor;
                }
                catch (Exception exc)
                {
                    retorno = dev;
                }
            }
            return retorno;
        }
    }

    public interface IIfNull
    {
        object Devolver(object dev);
    }
}
