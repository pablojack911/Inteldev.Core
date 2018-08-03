using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Inteldev.Core.DTO.Validaciones;

namespace Inteldev.Core.DTO.Locacion
{
    /// <summary>
    /// Entidad teléfono
    /// </summary>
    [ValidadorAtributo(typeof(ValidadorTelefono))]
    public class Telefono : DTOBase
    {
        /// <summary>
        /// Tipos de telefono: 0-Fijo 1-Celular
        /// </summary>
        [DataMember]
        public TipoTelefono TipoTelefono { get; set; }
        //[DataMember]
        //public string CodigoPais { get; set; }
        //[DataMember]
        //public string CodigoArea { get; set; }

        /// <summary>
        /// Numero de telefono incluyendo codigos
        /// </summary>
        [DataMember]
        private string numero;

        public string Numero
        {
            get { return numero; }
            set
            {
                numero = value;
                this.OnPropertyChanged("Numero");
            }
        }


        /// <summary>
        /// Metodo de impresion
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", Numero);
        }
    }
}
