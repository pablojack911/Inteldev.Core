using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo.Usuarios;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Negocios;
using Inteldev.Core.Negocios.Usuarios;
using Microsoft.Practices.Unity;

namespace Inteldev.Core.Servicios
{
    public class ServicioUsuario : ServicioABM<DTO.Usuarios.Usuario, Modelo.Usuarios.Usuario>, IServicioUsuario
    {
        public DTO.Usuarios.Usuario Autenticar(string nombre, string clave)
        {
            ParameterOverride[] para = 
            { 
                new ParameterOverride("empresa", ""), 
                new ParameterOverride("entidad", "empresa") 
            };

            var logueador = (ILogin)FabricaNegocios.Instancia.Resolver(typeof(ILogin), para);
            return logueador.Autenticar(nombre, clave);
        }



    }
}
