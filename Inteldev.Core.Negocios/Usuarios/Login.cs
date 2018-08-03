using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Datos;
using Inteldev.Core.Negocios.Mapeador;
using Microsoft.Practices.Unity;

namespace Inteldev.Core.Negocios.Usuarios
{
    /// <summary>
    /// Brinda metodos para manejar el login
    /// </summary>
    public class Login : LogicaDeNegociosBase<Modelo.Usuarios.Usuario>, ILogin
    {

        IMapeadorGenerico<Modelo.Usuarios.Usuario, DTO.Usuarios.Usuario> Mapeador;
        public Login(IMapeadorGenerico<Modelo.Usuarios.Usuario, DTO.Usuarios.Usuario> mapeador, string empresa, string entidad)
            : base(empresa, entidad)
        {
            this.Mapeador = mapeador;
        }

        public DTO.Usuarios.Usuario Autenticar(string usuario, string clave)
        {
            var resultado = this.Contexto.Consultar<Modelo.Usuarios.Usuario>(CargarRelaciones.CargarTodo).Where(u => u.Nombre == usuario && u.Clave == clave);
            if (resultado.Any())
            {
                var resutl = resultado.FirstOrDefault();
                resutl.PerfilUsuario = this.Contexto.Consultar<Modelo.Usuarios.PerfilUsuario>(CargarRelaciones.CargarTodo).Where(p => p.Id == resutl.PerfilId).FirstOrDefault();
                ParameterOverride[] para = 
                { 
                    new ParameterOverride("empresa", resutl.EmpresaPorDefecto.Codigo),
                    new ParameterOverride("entidad", typeof(Modelo.Usuarios.Usuario).Name.ToLower()) 
                };
                var buscadorPermisos = (IBuscadorDTO<Modelo.Usuarios.Permiso, DTO.Usuarios.Permiso>)FabricaNegocios.Instancia.Resolver(typeof(IBuscadorDTO<Modelo.Usuarios.Permiso, DTO.Usuarios.Permiso>), para);
                var permisos = buscadorPermisos.BuscarSimple(resutl.PerfilUsuario.Permiso.Id, CargarRelaciones.CargarTodo);
                var entidad = this.Mapeador.EntidadToDto(resutl);
                entidad.PerfilUsuario.Permiso = permisos;
                return entidad;
            }
            else
                return null;
        }
    }

}
