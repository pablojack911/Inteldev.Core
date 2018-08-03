using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador para DTO de Provincia
    /// </summary>
    class ValidadorProvincia : ValidadorBase<Provincia>
    {
        /// <summary>
        /// Constructor con reglas para validar.
        /// </summary>
        public ValidadorProvincia()
        {
            this.ClearRules(p => p.Codigo);

            this.RuleFor(p => p.Codigo)
                .Length(0, 2)
                    .WithMessage("Código no debe tener más de 2 caracteres.")
                .Must((p, codigo) =>
                {
                    int num = 0;
                    return int.TryParse(codigo, out num);
                })
                    .WithMessage("Código debe contener números solamente.");
        }
    }
}
