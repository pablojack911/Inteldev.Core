using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO.Validaciones
{
    /// <summary>
    /// Validador Base para todos los DTOs, heredable para su redefinicion de reglas
    /// </summary>
    /// <typeparam name="TEntidad">Tipo Entidad a validar</typeparam>
    public class ValidadorBase<TEntidad> : AbstractValidator<TEntidad>, IValidador
        where TEntidad : DTOMaestro
    {
        /// <summary>
        /// Constructor con reglas
        /// </summary>
        public ValidadorBase()
        {
            //RuleFor(p => p.Nombre).NotNull().WithMessage("El Nombre no puede estar vacio")
            //                      .NotEmpty().WithMessage("El Nombre no puede estar vacio");
            this.RuleFor(p => p.Codigo)
                .Must((p, Codigo) =>
            {
                int numero = 0;
                if (p.Codigo == null || p.Codigo == string.Empty)
                    return true;
                else
                    return int.TryParse(p.Codigo, out numero);
            }).WithMessage("Código no acepta letras.")
            .Length(0, 3).WithMessage("Código no debe superar los 3 dígitos.");

            this.RuleFor(p => p.Nombre)
                .NotEmpty()
                    .WithMessage("Debe ingresar un nombre.")
                .NotNull()
                    .WithMessage("Debe ingresar un nombre.");
        }

        public bool ValidaEntidad(object Entidad)
        {
            if (Entidad != null)
                return this.Validate((TEntidad)Entidad).IsValid;
            else
                return true;
        }

        public string ValidaPropiedad(object Entidad, string Propiedad)
        {
            var result = this.Validate((TEntidad)Entidad, Propiedad);
            if (!result.IsValid)
            {
                //unicamente muestra un mensaje de error. Si la propiedad tiene mas de uno
                //entonces va que tener que modificar esto para que devuelva otra cosa.
                return result.Errors.FirstOrDefault().ErrorMessage;
            }
            else
                return string.Empty;
        }

    }
}
