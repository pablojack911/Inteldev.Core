
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DTO.Validaciones
{
	/// <summary>
	/// validador estatico al que llaman los dto para validar. Este validador se comunica con 
	/// el validador concreto del vistamodelo.
	/// </summary>
	/// <typeparam name="TEntidad">Tipo de entidad a validar</typeparam>
	public class ValidadorEstatico
	{
		private static IValidador validador;

		private static IValidador GetValidador(object DTO)
		{
            if (DTO != null)
            {
                validador = null;
                var DTOType = DTO.GetType();
                var atri = (ValidadorAtributo)DTOType.GetCustomAttributes(typeof(ValidadorAtributo), true).FirstOrDefault();
                //SACAR ESTO CUANDO TODOS TENGAN VALIDADOR
                if (atri != null)
                {
                    var tipoValidador = atri.TipoValidador;
                    if (validador == null)
                        validador = (IValidador)Activator.CreateInstance(tipoValidador);
                    else
                    {
                        var tipoValidadorActual = validador.GetType();
                        if (tipoValidadorActual != tipoValidador)
                            validador = (IValidador)Activator.CreateInstance(tipoValidador);
                    }
                }
                return validador;
            }
            else
                return null;
		}

		public static string ValidarPropiedad(object dto, string propiedad)
		{
			GetValidador(dto);
            if (validador != null)
                return validador.ValidaPropiedad(dto, propiedad);
            else
                return "";
		}

		public static bool ValidadEntidad(object dto)
		{
			//ACA TAMBIEN SACAR
            GetValidador(dto);
            if (validador != null)
                return validador.ValidaEntidad(dto);                
            else
                return true;
		}
	}
}
