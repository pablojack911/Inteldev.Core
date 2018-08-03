using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.DTO;
using System.Runtime.Serialization;
using Inteldev.Core.DTO.Validaciones;

namespace Inteldev.Core.DTO.Locacion
{
    /// <summary>
    /// DTO para Calle
    /// </summary>
    [ValidadorAtributo(typeof(ValidadorCalle))]
    public class Calle : DTOMaestro
    {
        /// <summary>
        /// Localidad a la que pertenece
        /// </summary>
        [DataMember]
        //public Localidad Localidad { get; set; }
        private Localidad localidad;

        public Localidad Localidad
        {
            get { return localidad; }
            set
            {
                localidad = value;
                this.OnPropertyChanged("Localidad");
            }
        }

        /// <summary>
        /// Id de Localidad
        /// </summary>
        [DataMember]
        public int? LocalidadId { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", this.Nombre);
        }
    }
}
