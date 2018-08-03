using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Inteldev.Core.DTO.Validaciones
{
    public class ValidadorFecha : ValidadorBase<RangoFecha>
    {
        public ValidadorFecha()
        {
            this.RuleFor(p=>p.FechaDesde).LessThan(p=>p.FechaHasta);
        }
    }
}
