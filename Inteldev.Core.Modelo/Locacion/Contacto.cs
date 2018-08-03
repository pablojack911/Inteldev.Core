using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Locacion
{
    public class Contacto : EntidadBase
    {
		public Contacto( ):base()
		{
			Telefonos = new List<Telefono>();
            Domicilio = new Domicilio();
		}
        [UnoAMuchos]
        public ICollection<Telefono> Telefonos { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }

        [UnoAUno]
		public Domicilio Domicilio { get; set; }
    }
}
