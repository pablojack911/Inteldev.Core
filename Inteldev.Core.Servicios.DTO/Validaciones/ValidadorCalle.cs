using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador para DTO Calle
    /// </summary>
    public class ValidadorCalle : ValidadorBase<Calle>
    {
        /// <summary>
        /// Contstructor con reglas de validacion
        /// </summary>
        public ValidadorCalle()
        {
            this.ClearRules(c => c.Codigo);
            this.RuleFor(c => c.Nombre)
                .NotNull()
                    .WithMessage("Campo obligatorio.")
                .NotEmpty()
                    .WithMessage("Campo obligatorio.");

            this.RuleFor(c => c.Localidad)
                .NotNull()
                    .WithMessage("Debe especificar a que ciudad pertenece.");
        }
    }
}
