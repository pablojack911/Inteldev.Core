using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inteldev.Core.Modelo.Usuarios
{
    public class Permiso : EntidadMaestro
    {
        public NivelPermiso NivelPermiso { get; set; }

        public virtual ICollection<Permiso> SubModulos { get; set; }

        public Permiso()
        {
            this.SubModulos = new List<Permiso>();
        }

        public override string ToString()
        {
            return this.Nombre + this.NivelPermiso.ToString();
        }
    }
}
