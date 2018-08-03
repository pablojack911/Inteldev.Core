using Inteldev.Core.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador para DTO Usuario
    /// </summary>
    public class ValidadorUsuario : ValidadorBase<Usuario>
    {
        /// <summary>
        /// Constructor con reglas de validacion
        /// </summary>
        public ValidadorUsuario()
        {
            this.ClearRules(u => u.Codigo);

            this.RuleFor(u => u.Clave)
                .NotEmpty()
                    .WithMessage("Requerido.")
                .NotNull()
                    .WithMessage("Requerido.");

            this.RuleFor(u => u.PerfilUsuario)
                .NotEmpty()
                    .WithMessage("Requerido.")
                .NotNull()
                    .WithMessage("Requerido.");

            this.RuleFor(u => u.EmpresaPorDefecto)
                .NotEmpty()
                    .WithMessage("Requerido.")
                .NotNull()
                    .WithMessage("Requerido.");

            //this.RuleFor(u => u.UnidadDeNegocioActual)
            //    .NotNull()
            //        .WithMessage("Requerido.")
            //    .NotEmpty()
            //        .WithMessage("Requerido.");
        }

        //Func<Usuario, string, bool> validaClave = (u, clave) =>
        //        {
        //            var v = clave;
        //            if (clave != null && (clave.Length < 4 || clave.Length > 11))
        //                return false;
        //            return true;
        //        };
    }
}
