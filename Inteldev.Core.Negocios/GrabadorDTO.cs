using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Metadatos;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios.Mapeador;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios
{
    public class GrabadorDTO<TEntidad, TDto> : Inteldev.Core.Negocios.IGrabadorDTO<TEntidad, TDto>
        where TEntidad : EntidadBase
        where TDto : DTOBase
    {
        IGrabador<TEntidad> GrabadorEntidad;
        protected IMapeadorGenerico<TEntidad, TDto> Mapeador;
        protected IBuscador<TEntidad> Buscador;
        public GrabadorDTO(IGrabador<TEntidad> grabadorEntidad, IMapeadorGenerico<TEntidad, TDto> mapeador, IBuscador<TEntidad> buscador = null)
        {
            this.GrabadorEntidad = grabadorEntidad;
            this.Mapeador = mapeador;
            this.Buscador = buscador;
        }

        public virtual GrabadorCarrier Grabar(TDto dto, Usuario Usuario)
        {
            GrabadorCarrier ok = new GrabadorCarrier();
            var oDto = dto as DTO.DTOBase;
            var mapeadorUsuario = FabricaNegocios.Instancia.Resolver<MapeadorGenerico<Inteldev.Core.Modelo.Usuarios.Usuario, Inteldev.Core.DTO.Usuarios.Usuario>>();
            if (mapeadorUsuario == null)
            {
                ok.setError(true);
                ok.setMensaje("Error al grabar.\nCódigo de error: 0xGDTO_RMU");
            }
            else
            {
                var usuario = mapeadorUsuario.DtoToEntidad(Usuario);
                if (this.Mapeador == null)
                {
                    ok.setError(true);
                    ok.setMensaje("Error al grabar.\nCódigo de error: 0xGDTO_MNull");
                }
                else
                {
                    var entidad = this.Mapeador.DtoToEntidad(dto);
                    if (oDto.Id > 0)
                    {
                        ok = this.GrabadorEntidad.GrabarExistente(entidad, usuario);
                    }
                    else
                    {
                        ok = this.GrabadorEntidad.GrabarNuevo(entidad, usuario);
                        ok.setId(entidad.Id);
                    }
                    if (!ok.getError())
                    {
                        this.GrabadorEntidad.GuardarCambios();
                        ok = this.GrabarEnFox(entidad, usuario.Nombre, ok);
                    }
                }
            }
            return ok;
        }

        public bool Existe(TDto Entidad)
        {
            return (MetaDatos.ObtenerValor<int>(Entidad, "Id") != 0);
        }

        public GrabadorCarrier GrabarEnFox(TEntidad entidad, string usuario, GrabadorCarrier ok)
        {
            var oFox = FabricaNegocios._Resolver<IGrabadorFox<TEntidad>>();
            if (oFox != null)
            {
                oFox.Usuario = usuario;
                this.Buscador.CargarEntidadesRelacionadas = CargarRelaciones.CargarTodo;
                entidad = this.Buscador.BuscarSimple(entidad.Id);
                try
                {
                    oFox.Grabar(entidad);
                }
                catch (Exception ex)
                {
                    ok.setError(true);
                    ok.setMensaje(ok.getMensaje()+"\n¡ATENCIÓN!\nOcurrió un error al grabar de Fox, pero en Fixius se grabó correctamente.\nSi es un ALTA, no intente grabar nuevamente.\nContactese con el departamento de Sistemas.\n\nInformación del error: " + ex.Message);
                }
            }
            return ok;
        }
    }
}
