using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Validaciones;

namespace Inteldev.Core.DTO.Usuarios
{
    [ValidadorAtributo(typeof(ValidadorUsuario))]
    public class Usuario : DTOMaestro
    {
        [DataMember]
        //public string Clave { get; set; }
        private string clave;

        public string Clave
        {
            get { return clave; }
            set
            {
                clave = value;
                this.OnPropertyChanged("Clave");
            }
        }

        [DataMember]
        //        public PerfilUsuario PerfilUsuario { get; set; }
        private PerfilUsuario perfilUsuario;

        public PerfilUsuario PerfilUsuario
        {
            get { return perfilUsuario; }
            set
            {
                perfilUsuario = value;
                this.OnPropertyChanged("PerfilUsuario");
            }
        }

        [DataMember]
        public int PerfilId { get; set; }
        [DataMember]
        public UnidadeDeNegocio? UnidadNegocioPorDefecto { get; set; }
        [DataMember]
        //public Empresa EmpresaPorDefecto { get; set; }
        private Empresa empresaPorDefecto;

        public Empresa EmpresaPorDefecto
        {
            get { return empresaPorDefecto; }
            set
            {
                empresaPorDefecto = value;
                this.OnPropertyChanged("EmpresaPorDefecto");
            }
        }

        [DataMember]
        public int? EmpresaPorDefectoId { get; set; }
        [DataMember]
        //public UnidadeDeNegocio UnidadDeNegocioActual { get; set; }
        private UnidadeDeNegocio unidadDeNegocioActual;

        public UnidadeDeNegocio UnidadDeNegocioActual
        {
            get { return unidadDeNegocioActual; }
            set
            {
                unidadDeNegocioActual = value;
                this.OnPropertyChanged("UnidadDeNegocioActual");
            }
        }

        [DataMember]
        public Sucursal SucursalActual { get; set; }
        [DataMember]
        public Empresa EmpresaActual { get; set; }
    }
}
