using Inteldev.Core.Datos;
using Inteldev.Core.Modelo.Usuarios;
using Inteldev.Core.Negocios.Mapeador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios
{
    public class BuscadorDTOPermiso : BuscadorDTO<Inteldev.Core.Modelo.Usuarios.Permiso, Inteldev.Core.DTO.Usuarios.Permiso>
    {
        public BuscadorDTOPermiso(IBuscador<Permiso> buscadorEntidad, IMapeadorGenerico<Inteldev.Core.Modelo.Usuarios.Permiso, Inteldev.Core.DTO.Usuarios.Permiso> mapeador)
            : base(buscadorEntidad, mapeador)
        {

        }

        public override DTO.Usuarios.Permiso BuscarSimple(object busqueda, CargarRelaciones cargarEntidades)
        {
            int id = 0;
            int.TryParse(busqueda.ToString(), out id);
            //id del permiso que se selecciono en el buscador de la Tabla de Perfiles de usuario.
            //Busqueda sería ItemSeleccionado de BuscadorInicial del Formulario
            var permiso = this.BuscadorEntidad.BuscarSimple(id);
            Inteldev.Core.DTO.Usuarios.Permiso dto = null;
            if (permiso != null)
                dto = Inteldev.Core.Negocios.Mapeador.Mapeador.Instancia.EntidadToDto<Inteldev.Core.Modelo.Usuarios.Permiso, Inteldev.Core.DTO.Usuarios.Permiso>(permiso);
            return dto;
        }
    }
}
