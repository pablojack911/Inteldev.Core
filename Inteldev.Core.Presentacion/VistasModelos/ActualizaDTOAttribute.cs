using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Presentacion.VistasModelos
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ActualizaDTOAttribute : Attribute
    {
        public string Propiedad { get; set; }
        public ActualizaDTOAttribute(string propiedad)
        {
            this.Propiedad = propiedad;
        }
      
    }
}
