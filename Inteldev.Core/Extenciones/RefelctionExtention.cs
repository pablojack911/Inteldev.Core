using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Extenciones
{
    public static class RefelctionExtention
    {
       
        public static ReflectionHelper Reflection(this Object objeto)
        {
            return new ReflectionHelper(objeto);
        }

    }
}
