using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Inteldev.Core.DTO
{
    [AttributeUsage(AttributeTargets.Property)]    
    public class IncluirEnBuscadorAttribute : Attribute
    {
       
        public IncluirEnBuscadorAttribute()
        {
            
        }

    }
}
