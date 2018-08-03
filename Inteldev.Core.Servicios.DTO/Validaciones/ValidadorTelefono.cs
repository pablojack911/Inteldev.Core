using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    public class ValidadorTelefono : AbstractValidator<Telefono>, IValidador
    {
        public ValidadorTelefono()
        {
            this.RuleFor(c => c.Numero)
                .NotNull()
                    .WithMessage("Debe ingresar número.")
                .NotEmpty()
                    .WithMessage("Debe ingresar número.");
        }

        public bool ValidaEntidad(object Entidad)
        {
            if (Entidad != null)
                return this.Validate((Telefono)Entidad).IsValid;
            else
                return true;
        }

        public string ValidaPropiedad(object Entidad, string Propiedad)
        {
            var result = this.Validate((Telefono)Entidad, Propiedad);
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
