using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo.Organizacion;
using Inteldev.Core.Modelo.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inteldev.Core.Modelo.Menu
{
    public class OpcionMenuEmpresa
    {
        public int id { get; set; }

        public Empresa Empresa { get; set; }
		[ForeignKey("Empresa")]
		public int? EmpresaId { get; set; }
		
		public OpcionMenu Menu { get; set; }
		[ForeignKey("OpcionMenu")]
		public int? MenuId { get; set; }
        
		public ICollection<PerfilUsuario> Perfiles { get; set; }

    }
}
