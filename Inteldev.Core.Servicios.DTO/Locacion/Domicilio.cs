using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Inteldev.Core.DTO.Locacion
{
    public class Domicilio : DTOBase
    {
        public Domicilio()
        {
            this.Coordenada = new Coordenada();
        }
        [DataMember]
        //public Calle Calle { get; set; }

        private Calle calle;

        public Calle Calle
        {
            get { return calle; }
            set
            {
                calle = value;
                this.OnPropertyChanged("Calle");
            }
        }

        [DataMember]
        public int? CalleId { get; set; }
        [DataMember]
        //public int Numero { get; set; }
        private int numero;

        public int Numero
        {
            get { return numero; }
            set
            {
                numero = value;
                this.OnPropertyChanged("Numero");
            }
        }

        [DataMember]
        //public int Piso { get; set; }
        private int piso;

        public int Piso
        {
            get { return piso; }
            set
            {
                piso = value;
                this.OnPropertyChanged("Piso");
            }
        }

        [DataMember]
        //public string Departamento { get; set; }
        private string departamento;

        public string Departamento
        {
            get { return departamento; }
            set
            {
                departamento = value;
                this.OnPropertyChanged("Departamento");
            }
        }

        [DataMember]
        public Coordenada Coordenada { get; set; }


        public override string ToString()
        {
            return string.Format("{0} Nº {1} {2} {3}", Calle,
                                                       Numero,
                                                       Piso == 0 ? string.Empty : "Piso:" + Piso.ToString(),
                                                       string.IsNullOrEmpty(Departamento) ? string.Empty : "Dpto:" + Departamento);
        }
    }
}
