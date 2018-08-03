using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Modelo;
using Inteldev.Core.Negocios;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Inteldev.Core.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServicioObtenerCodigoDisponible<TEntidad> : IServicioObtenerCodigoDisponible
        where TEntidad : EntidadMaestro
    {
        public string CodigoDisponible(string desde)
        {
            var paramers = new ParameterOverride[2];
            paramers[0] = new ParameterOverride("empresa", "01");
            paramers[1] = new ParameterOverride("entidad", typeof(TEntidad).Name);

            //var buscador = (BuscadorGenerico<TEntidad>)FabricaNegocios.Instancia.Resolver(typeof(BuscadorGenerico<TEntidad>), paramers); //buscador que utilizo para obtener las listas de codigos
            var numerador = (Numerador<TEntidad>)FabricaNegocios.Instancia.Resolver(typeof(INumerador<TEntidad>), paramers); //numerador de donde obtengo el tamaño maximo de digitos de la entidad

            //long elElegido = 0; //el codigo elegido en long. se retorna transformado a string al fin del metodo en el caso de que no se busque un 

            if (desde == null || desde == "" || desde == "0") //evaluamos el dato del parametro 
                desde = "1".PadLeft(numerador.TamañoMaximo, '0'); //buscamos desde el codigo 001
            else
                desde = desde.Trim();

            long LongDesde = 0; //variable utilizada para transformar el paramtro 'desde' en long
            var soloNumero = long.TryParse(desde, out LongDesde);

            if (soloNumero)
            {
                return numerador.ProximoCodigoDisponibleSoloNumero(LongDesde, numerador.TamañoMaximo);
            }
            else //desde CONTIENE LETRA
            {
                var tamañomax = numerador.TamañoMaximo - 1;
                var prefijo = desde.Substring(0, 1).ToUpperInvariant();
                var resto = desde.Remove(0, 1);
                if (resto == string.Empty)
                    resto = "1".PadLeft(tamañomax, '0');
                else
                    resto = resto.PadRight(tamañomax, '0'); //!!!!! ver continuación cuando esto ocurra
                return numerador.ProximoCodigoDisponibleConPrefijo(prefijo, resto, tamañomax);
            }
            //return elElegido.ToString().PadLeft(numerador.TamañoMaximo, '0');
        }

        //public string ProximoCodigoDisponibleConPrefijo(string prefijo, string resto, IBuscador<TEntidad> buscador, int tamañomax, INumerador<TEntidad> numerador)
        //{
        //    long i = 0;
        //    var listaCodigos = new List<string>();
        //    long max = long.Parse("9".PadLeft(tamañomax, '9'));
        //    long elElegido = 0; //el codigo elegido en long. se retorna transformado a string al fin del metodo en el caso de que no se busque un 

        //    if (!Regex.IsMatch(resto.ToString(), @"[a-zA-Z]")) //significa que no contiene letras. si contiene numeros al entrar acá debería hacer el padright en el buscador? ej: desde = A1; deberia traer los codigos que empiezan con A1, A1001,A1002,A1004, etc
        //    {
        //        var LongDesde = long.Parse(resto);
        //        do
        //        {
        //            listaCodigos.Clear();
        //            listaCodigos = buscador.ObtenerListaCodigos<TEntidad>(prefijo, LongDesde, tamañomax);
        //            if (listaCodigos.Count > 0)
        //            {
        //                i = LongDesde;
        //                foreach (var codigo in listaCodigos)
        //                {
        //                    var codigoLong = long.Parse(codigo.Remove(0, 1));
        //                    if (codigoLong > i)
        //                    {
        //                        elElegido = i;
        //                        break;
        //                    }
        //                    else
        //                        if (i == max)
        //                        {
        //                            prefijo = numerador.IncrementaLetra(prefijo);
        //                            LongDesde = 1;
        //                            i = 1;
        //                            break;
        //                        }
        //                        else
        //                            i++; //VERIFICAR QUE NO SUPERE EL tamañomax, ejemplo: busco codigo disponible a partir de A99. traigo los codigos superiores a A99, 1 solo (A99), pasa por el bucle y suma 1. queda A100 no sirve, 3 digitos maximo.
        //                }
        //                LongDesde = i;
        //            }
        //            else
        //            {
        //                //if (!pasamosDeLetra)
        //                //{
        //                //prefijo = numerador.IncrementaLetra(prefijo);
        //                //LongDesde = 0;
        //                //    pasamosDeLetra = true;
        //                //}
        //                //else
        //                //{
        //                return prefijo.ToUpperInvariant() + (LongDesde).ToString().PadLeft(tamañomax, '0');
        //                //}
        //            }
        //        }
        //        while (elElegido == 0);
        //    }
        //    return prefijo.ToUpperInvariant() + elElegido.ToString().PadLeft(tamañomax, '0');
        //}

        //public string ProximoCodigoDisponibleSoloNumero(long LongDesde, IBuscador<TEntidad> buscador, int tamañoMaximo, INumerador<TEntidad> numerador)
        //{
        //    long i = 0;
        //    var listaCodigos = new List<string>();
        //    long elElegido = 0; //el codigo elegido en long. se retorna transformado a string al fin del metodo en el caso de que no se busque un 
        //    do
        //    {
        //        listaCodigos.Clear();
        //        listaCodigos = buscador.ObtenerListaCodigos<TEntidad>(LongDesde, tamañoMaximo);
        //        if (listaCodigos.Count > 0)
        //        {
        //            i = LongDesde;
        //            foreach (var codigo in listaCodigos)
        //            {
        //                var codigoLong = long.Parse(codigo);
        //                if (codigoLong > i)
        //                {
        //                    elElegido = i;
        //                    break;
        //                }
        //                else
        //                    i++;
        //            }
        //            LongDesde = i;
        //        }
        //        else
        //        {
        //            var prefijo = "A";
        //            var tamañomax = tamañoMaximo - 1;
        //            var resto = "0".PadLeft(tamañomax);
        //            //llamar al proximocodigoconprefijo (que hace el return)
        //            return this.ProximoCodigoDisponibleConPrefijo(prefijo, resto, buscador, tamañomax, numerador);
        //        }
        //    }
        //    while (elElegido == 0);

        //    return elElegido.ToString().PadLeft(tamañoMaximo, '0');
        //}
    }
}
