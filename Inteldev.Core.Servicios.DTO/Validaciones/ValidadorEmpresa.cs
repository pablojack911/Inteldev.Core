using Inteldev.Core.DTO.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;


namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador de la Empresa
    /// </summary>
    public class ValidadorEmpresa : ValidadorBase<Empresa>
    {

        /// <summary>
        /// Constructor con sus reglas
        /// </summary>
        public ValidadorEmpresa()
        {
            this.ClearRules(e => e.Codigo);
            this.RuleFor(e => e.Codigo)
                .Length(0, 2)
                    .WithMessage("Código no debe tener más de 2 caracteres.");

            this.RuleFor(e => e.RazonSocial)
                .NotEmpty()
                    .WithMessage("Razón Social no puede estar vacío.")
                .NotNull()
                    .WithMessage("Razón Social no puede estar vacío.");

            this.RuleFor(e => e.CUIT)
                .NotEmpty()
                    .WithMessage("CUIT no puede estar vacío.")
                    .When(e => e.CondicionAnteIVA != Fiscal.CondicionAnteIva.ConsumidorFinal)
                .NotNull()
                    .WithMessage("CUIT no puede estar vacío.")
                    .When(e => e.CondicionAnteIVA != Fiscal.CondicionAnteIva.ConsumidorFinal)
                .Must((e, cuit) =>
                {
                    var validadorCuit = new ValidadorCUIT();
                    return validadorCuit.ValidaCuit(cuit);
                })
                    .WithMessage("El CUIT ingresado es incorrecto.")
                    .When(e => e.CondicionAnteIVA != Fiscal.CondicionAnteIva.ConsumidorFinal);

            this.RuleFor(e => e.CondicionAnteIVA)
                .NotEqual(Fiscal.CondicionAnteIva.ConsumidorFinal)
                    .WithMessage("No puede elejir Consumidor Final.");

            this.RuleFor(e => e.FechaDesde)
                .LessThan(emp => emp.FechaHasta)
                    .WithMessage("Debe ser inferior a fecha fin de período.");

            this.RuleFor(e => e.FechaHasta)
                .GreaterThan(emp => emp.FechaDesde)
                    .WithMessage("Debe ser superior a fecha inicio de período.");
        }
    }
}
