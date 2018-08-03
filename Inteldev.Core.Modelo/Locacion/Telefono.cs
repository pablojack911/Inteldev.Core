using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Inteldev.Core.Modelo.Locacion
{
    /// <summary>
    /// Entidad telefono
    /// </summary>
    [Table("Telefonos")]
    public class Telefono : EntidadBase
    {
        /// <summary>
        /// Tipo de telefono
        /// </summary>
        public TipoTelefono TipoTelefono { get; set; }
        //public string CodigoPais { get; set; }
        //public string CodigoArea { get; set; }
        /// <summary>
        /// Numero
        /// </summary>
        public string Numero { get; set; }

    }
}
