using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.DTO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncluirEnListadoAttribute : Attribute
    {
        public IncluirEnListadoAttribute()
        {

        }
    }
}
