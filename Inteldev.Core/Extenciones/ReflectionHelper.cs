using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Extenciones
{
    public class ReflectionHelper
    {
        Type typeObjeto;
        Object objeto;
        public ReflectionHelper(object paramobjeto)
        {
            this.objeto = paramobjeto;
            this.typeObjeto = this.objeto.GetType();
        }
        public void SetValue(string propiedad, object valor)
        {
            typeObjeto.GetProperty(propiedad).SetValue(objeto, valor, null);
        }

        public TObjetoReturn GetValue<TObjetoReturn>(string propiedad) where TObjetoReturn : class
        {
            return GetValue(propiedad) as TObjetoReturn;
        }

        public object GetValue(string propiedad)
        {
            var prop = typeObjeto.GetProperty(propiedad);
            
            return prop==null?null:prop.GetValue(objeto, null);
        }

        public bool EsColeccion()
        {
            return objeto is IEnumerable<Object>;
        }

        public TAtributo ObtenerAtributo<TAtributo>() where TAtributo: Attribute
        {
            return typeObjeto.GetCustomAttributes(typeof(TAtributo),false).FirstOrDefault() as TAtributo;
        }

        public TAtributo ObtenerAtributo<TAtributo>(string propiedad) where TAtributo : Attribute
        {
            return typeObjeto.GetProperty(propiedad).GetCustomAttributes(typeof(TAtributo), false).FirstOrDefault() as TAtributo;
        }


    }
}
