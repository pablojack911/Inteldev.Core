using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Estructuras
{
    public class Iif: IEntonces, ISiNo
    {
        bool condicion;
        object entonces;
        
        private Iif(bool condicion)
        {
            this.condicion = condicion;
        }
        public static IEntonces Condicion(bool condicion)
        {
            return new Iif(condicion);
        }

        public ISiNo Entonces(object entonces)
        {
            this.entonces = entonces;

            return this;
        }


        public object Sino(object sino)
        {
            return condicion ? entonces : sino;
        }
    }

    public interface IEntonces
    {
        ISiNo Entonces(object sino);
    }

    public interface ISiNo
    {
        object Sino(object sino);
    }
}
