using System.Collections.Generic;
using Inteldev.Core.Modelo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inteldev.Core.Modelo.Usuarios
{
    public class PerfilUsuario : EntidadMaestro
    {
        public Permiso Permiso { get; set; }
    }
}
