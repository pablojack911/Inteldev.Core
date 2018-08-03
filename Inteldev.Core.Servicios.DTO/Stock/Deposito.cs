using Inteldev.Core.DTO.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO.Stock
{
    /// <summary>
    /// DTO para Deposito
    /// </summary>
    [ValidadorAtributo(typeof(ValidadorBase<Deposito>))]
    public class Deposito : DTOMaestro
    {
    }
}
