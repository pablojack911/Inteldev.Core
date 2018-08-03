using Inteldev.Core.Datos;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.Negocios.Grabadores
{
    public class GrabadorPerfil : GrabadorGenerico<PerfilUsuario>
    {
        public GrabadorPerfil(string empresa, string entidad, Inteldev.Core.Negocios.Validadores.IValidador<PerfilUsuario> validador)
            : base(empresa, entidad, validador)
        {
        }

        private void marcaRecursivo(Permiso permiso)
        {
            if (permiso != null)
            {
                foreach (var item in permiso.SubModulos)
                {
                    //this.Contexto.MarcarComoModificado(item);
                    marcaRecursivo(item);
                }
            }
        }

        public override GrabadorCarrier GrabarExistente(PerfilUsuario Entidad, Usuario Usuario)
        {
            //this.Contexto.MarcarComoModificado(Entidad);
            var grabador = new GrabadorCarrier();
            this.marcaRecursivo(Entidad.Permiso);
            //MODIFICACIONES 2015
            //this.Contexto.SaveChanges();
            var listaContextos = this.Contexto.ObtenerContextos(typeof(PerfilUsuario));
            listaContextos.ForEach(x => x.SaveChanges());
            return grabador;
        }
    }
}
