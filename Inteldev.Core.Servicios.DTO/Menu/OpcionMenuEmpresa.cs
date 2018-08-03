using System.Collections.Generic;
using System.Runtime.Serialization;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Usuarios;

namespace Inteldev.Core.DTO.Menu
{
    public class OpcionMenuEmpresa:DTOBase
    {
        [DataMember]
        public Empresa Empresa { get; set; }
        [DataMember]
		public OpcionMenu Menu { get { return Menu; } 
			set 
			{
				Menu = value;
				MenuId = value.Id;
			} 
		}
		[DataMember]
		public int MenuId { get; set; }
        [DataMember]
        public List<PerfilUsuario> Perfiles { get; set; }

    }
}
