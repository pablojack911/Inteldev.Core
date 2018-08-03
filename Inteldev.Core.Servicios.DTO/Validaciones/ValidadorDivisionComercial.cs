using Inteldev.Core.DTO.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador para el DTO de Division Comercial
    /// </summary>
    public class ValidadorDivisionComercial : ValidadorBase<DivisionComercial>
    {
        /// <summary>
        /// Constructor con reglas
        /// </summary>
        public ValidadorDivisionComercial()
        {
            this.ClearRules(dc => dc.Codigo);
            this.RuleFor(dc => dc.Codigo)
                .NotNull()
                    .WithMessage("El código no puede estar vacío.")
                .NotEmpty()
                    .WithMessage("El código no puede estar vacío.")
                .Length(5)
                    .WithMessage("El código no puede exceder los 5 dígitos.");
        }
    }
}
