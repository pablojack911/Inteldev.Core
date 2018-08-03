using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO
{
    public class DTOMaestro : DTOBase
    {
        [DataMember]
        private string codigo;
        [IncluirEnBuscador]
        [IncluirEnListado]
        public string Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;
                this.OnPropertyChanged("Codigo");
            }
        }


        public override string ToString()
        {
            var codigoString = this.Codigo == null || this.Codigo.Length == 0 ? "Nuevo" : this.Codigo;
            string result = "(" + codigoString + ") - " + this.Nombre;
            return result;
        }

    }
}
