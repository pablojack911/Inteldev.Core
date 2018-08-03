using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Inteldev.Core.DTO.Locacion;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador para DTO Localidad
    /// </summary>
    class ValidadorLocalidad : ValidadorBase<Localidad>
    {
        /// <summary>
        /// Constructor con reglas
        /// </summary>
        public ValidadorLocalidad()
        {
            this.ClearRules(l => l.Codigo);

            this.RuleFor(l => l.Codigo)
                .NotEmpty()
                    .WithMessage("Código Postal no puede estar vacío.")
                .NotNull()
                    .WithMessage("Código Postal no puede estar vacío.")
                .Length(4)
                    .WithMessage("Código Postal debe tener 4 caracteres.")
                .Must((l, codigo) =>
                {
                    int num = 0;
                    return int.TryParse(codigo, out num);
                })
                    .WithMessage("Código debe contener números solamente.");

            this.RuleFor(l => l.Nombre)
                .NotEmpty()
                    .WithMessage("Nombre no puede estar vacío.")
                .NotNull()
                    .WithMessage("Nombre no puede estar vacío.");

            //Func<Localidad, Provincia, bool> validaProvincia = (localidad, prov) =>
            //    {
            //        if (localidad.Provincia == null)
            //            return false;
            //        return true;
            //    };

            this.RuleFor(l => l.Provincia)
                //.Must(validaProvincia)
                .NotNull()
                    .WithMessage("Debe especificar una provincia.");


        }
    }
}
