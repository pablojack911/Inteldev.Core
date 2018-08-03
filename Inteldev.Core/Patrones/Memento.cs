using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Metadatos;
using System.Collections;

namespace Inteldev.Core.Patrones
{
    public class Memento<TObjeto>
    {
        private Dictionary<string, object> imagen;
        private Dictionary<Tuple<int, string>, object> imagenes;
        private TObjeto objeto;

        public Memento(TObjeto objeto)
        {
            this.objeto = objeto;
            this.imagen = new Dictionary<string, object>();
            this.imagenes = new Dictionary<Tuple<int, string>, object>();
            this.CapturarEstado(objeto);
            
        }           

        public TObjeto RecuperarEstado()
        {
            Metadatos.MetaDatos.ForEachPropertys(objeto, p => this.RecuperaPropiedad(p.Name,imagen.First(k => k.Key == p.Name).Value ));
            return objeto;
        }
        private void RecuperaPropiedad(string propiedad,object valor)
        {
            if (valor is IEnumerable)
            {
                foreach (var item in (valor as IEnumerable))
                {
                    MetaDatos.ForEachPropertys(item, p => p.SetValue(item, this.imagenes.First(i => i.Key.Item1 == item.GetHashCode() && i.Key.Item2 == p.Name).Value, null));
                }
            }
            MetaDatos.AsignarValor(objeto, propiedad, valor);
        }

        public void CapturarEstado(TObjeto objeto)
        {
            this.LimpiarEstados();
            Metadatos.MetaDatos.ForEachPropertys(objeto, p => this.CapturaPropiedad(p.Name, p.GetValue(objeto, null)));
        }

        private void CapturaPropiedad(string propiedad, object valor)
        {
            if (propiedad != "Error")
            {
                this.imagen.Add(propiedad, valor);
                if (valor is IEnumerable)
                {
                    foreach (var item in (valor as IEnumerable))
                    {
                        MetaDatos.ForEachPropertys(item, p => this.imagenes.Add(new Tuple<int, string>(item.GetHashCode(), p.Name), p.GetValue(item, null)));
                    }
                }
            }
        }
        
        public void LimpiarEstados()
        {
            this.imagen.Clear();
            this.imagenes.Clear();
        }
    }
}
