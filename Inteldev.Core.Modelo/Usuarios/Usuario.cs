using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Organizacion;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inteldev.Core.Modelo.Usuarios
{
    public class Usuario : EntidadMaestro
    {
        public string Clave { get; set; }
		public PerfilUsuario PerfilUsuario { get; set; }
		[ForeignKey("PerfilUsuario")]
		public int? PerfilId { get; set; }
		public UnidadeDeNegocio UnidadNegocioPorDefecto { get; set; }
        public Empresa EmpresaPorDefecto { get; set; }
        [ForeignKey("EmpresaPorDefecto")]
        public int? EmpresaPorDefectoId { get; set; }
		//datos no persistibles
		[NotMapped]
		public UnidadeDeNegocio UnidadDeNegocioActual { get; set; }
		[NotMapped]
		public Sucursal SucursalActual { get; set; }
		[NotMapped]
		public Empresa EmpresaActual { get; set; }
    }
}
