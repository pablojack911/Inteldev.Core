using System.Collections.Generic;

namespace Inteldev.Core.Modelo.Menu
{
    public class OpcionMenu : EntidadBase
    {
       
        public string Modulo { get; set; }
        public string Icono { get; set; }
        public string Atajo { get; set; }
        public object Comando { get; set; }
        public ICollection<OpcionMenu> Opciones { get; set; }
        
    }
}
