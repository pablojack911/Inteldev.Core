using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios.Mapeador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios
{
    public class BorradorDTO<TEntidad, TDto> : Inteldev.Core.Negocios.IBorradorDTO<TEntidad, TDto>
        where TEntidad : EntidadBase
        where TDto : DTOBase
    {
        IBorrador<TEntidad> BorradorEntidad;

        private IMapeadorGenerico<Inteldev.Core.Modelo.Usuarios.Usuario, Inteldev.Core.DTO.Usuarios.Usuario> mapeadorUsuario;

        public BorradorDTO(IBorrador<TEntidad> borradorEntidad)
        {
            this.BorradorEntidad = borradorEntidad;
            this.mapeadorUsuario = FabricaNegocios.Instancia.Resolver<IMapeadorGenerico<Inteldev.Core.Modelo.Usuarios.Usuario, Inteldev.Core.DTO.Usuarios.Usuario>>();
        }
        public ErrorCarrier Borrar(TDto dto, Usuario Usuario)
        {
            var usuario = mapeadorUsuario.DtoToEntidad(Usuario);
            var mapeador = FabricaNegocios._Resolver<IMapeadorGenerico<TEntidad, TDto>>();
            var entidad = mapeador.DtoToEntidad(dto);
            var ec = BorradorEntidad.Borrar(entidad, usuario);
            if (ec.borroOk)
            {
                if (!BorradorFox(entidad))
                {
                    ec.borroOk = false;
                    ec.mensaje = "Error al borrar en Base de datos Fox Pro";
                }
            }

            return ec;
        }
        public ErrorCarrier Borrar(int id, Usuario Usuario)
        {
            var usuario = this.mapeadorUsuario.DtoToEntidad(Usuario);
            return BorradorEntidad.Borrar(id, usuario);
        }


        bool BorradorFox(TEntidad entidad)
        {
            bool ok = true;
            var borradorFox = FabricaNegocios._Resolver<IGrabadorFox<TEntidad>>();
            if (borradorFox != null)
                ok = borradorFox.Borrar(entidad);
            return ok;

        }
    }
}
