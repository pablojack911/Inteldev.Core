using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using Inteldev.Core.DTO.Validaciones;


namespace Inteldev.Core.DTO.Locacion
{
    /// <summary>
    /// DTO para Localidad
    /// </summary>
    [ValidadorAtributo(typeof(ValidadorLocalidad))]
    public class Localidad : DTOMaestro
    {
        /// <summary>
        /// Provincia
        /// </summary>
        [DataMember]
        //public Provincia Provincia { get; set; }
        private Provincia provincia;

        public Provincia Provincia
        {
            get { return provincia; }
            set
            {
                provincia = value;
                this.OnPropertyChanged("Provincia");
            }
        }

        /// <summary>
        /// Id de Provincia
        /// </summary>
        [DataMember]
        public int? ProvinciaId { get; set; }


    }
}
