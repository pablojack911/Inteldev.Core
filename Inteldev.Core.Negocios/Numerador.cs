using Inteldev.Core.Modelo;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inteldev.Core.Negocios
{
    public class Numerador<TEntidad> : LogicaDeNegociosBase<TEntidad>, Inteldev.Core.Negocios.INumerador<TEntidad> where TEntidad : EntidadMaestro
    {
        protected TEntidad entidad;
        public int TamañoMaximo { get; set; }
        public IBuscador<TEntidad> buscador { get; set; }

        public Numerador(string empresa, string entidad)
            : base(empresa, entidad)
        {
            var paramers = new ParameterOverride[2];
            paramers[0] = new ParameterOverride("empresa", empresa);
            paramers[1] = new ParameterOverride("entidad", entidad);
            buscador = (BuscadorGenerico<TEntidad>)FabricaNegocios.Instancia.Resolver(typeof(BuscadorGenerico<TEntidad>), paramers); //buscador que utilizo para obtener las listas de codigos
        }
        public virtual string UltimoCodigo()
        {
            string codigomaximo = "9".PadLeft(this.TamañoMaximo, '9');
            //ultimo codigo de la tabla          
            return this.Contexto.Consultar<TEntidad>(CargarRelaciones.NoCargarNada).Select(p => p.Codigo).Max();

        }

        public virtual string ProximoCodigo(TEntidad entidad = null)
        {
            //this.entidad = entidad;

            //string ultimoCodigoGrabado = this.UltimoCodigo();
            //string proximoCodigo = string.Empty;

            //if (ultimoCodigoGrabado == null)
            //    return "1".PadLeft(this.TamañoMaximo, '0');

            //long numero = 0;
            //if (long.TryParse(ultimoCodigoGrabado, out numero))
            //    proximoCodigo = numeraSinLetra(ultimoCodigoGrabado, numero);
            //else
            //    proximoCodigo = numeraConLetra(ultimoCodigoGrabado);

            //return proximoCodigo;
            return this.ProximoCodigoDisponibleSoloNumero(1, this.TamañoMaximo);
        }



        string numeraSinLetra(string codigoAnterior, long numero)
        {
            if (codigoAnterior == "9".PadLeft(this.TamañoMaximo, '9'))
            {
                return "A".PadRight(this.TamañoMaximo - 1, '0') + '1';
            }
            else
            {
                numero = numero + 1;
                return (numero.ToString().PadLeft(this.TamañoMaximo, '0'));
            }
        }

        string numeraConLetra(string codigo)
        {
            var letras = codigo.ToArray();
            string parteLetras = string.Empty;
            string parteNumero = string.Empty;
            foreach (var c in letras)
            {
                long numero = 0;
                if (long.TryParse(c.ToString(), out numero))
                    parteNumero = parteNumero + c;
                else
                    parteLetras = parteLetras + c;

            }
            string nuevaParteNumero = string.Empty;
            string nuevaParteLetras = string.Empty;
            if (parteNumero == "9".PadRight(this.TamañoMaximo - parteLetras.Length, '9'))
            {
                if (parteLetras == "Z".PadRight(parteLetras.Length, 'Z'))
                    nuevaParteLetras = "A".PadRight(parteLetras.Length + 1, 'A');
                else
                {
                    nuevaParteLetras = IncrementaLetra(parteLetras);
                }
                nuevaParteNumero = "1".PadLeft(this.TamañoMaximo - nuevaParteLetras.Length, '0');
            }
            else
            {
                nuevaParteLetras = parteLetras;
                nuevaParteNumero = (long.Parse(parteNumero) + 1).ToString().PadLeft(this.TamañoMaximo - nuevaParteLetras.Length, '0');
            }
            return nuevaParteLetras + nuevaParteNumero;
        }

        public string IncrementaLetra(string codigoLetra)
        {
            byte[] letraNum = Encoding.ASCII.GetBytes(codigoLetra.Substring(0, 1));
            letraNum[0]++;
            return Encoding.ASCII.GetChars(letraNum).FirstOrDefault().ToString();
        }

        ///00000
        ///00001
        ///.....
        ///99999
        ///A0001
        ///A0002
        ///.....
        ///A9999
        ///B0001
        ///B0002
        ///.....
        ///B9999
        ///C0001
        ///C0002
        ///.....
        ///Z9999
        ///AA001
        ///AA999
        ///BA001
        ///BA999
        ///CA001



        //public string ProximoCodigo()
        //{
        //    string codigo = this.UltimoCodigo();
        //    if (codigo == null)
        //        codigo = "0";
        //    else
        //        codigo = codigo.ToUpper();
        //    var letra = Regex.Match(codigo, @"[A-Z]{1}").Groups[0].ToString();
        //    int numero = 0;
        //    try
        //    {
        //        numero = int.Parse(Regex.Match(codigo, @"\d+").Groups[0].ToString());
        //    }
        //    catch (FormatException e)
        //    {
        //        numero = 9;
        //    }
        //    int tamaño = 0;
        //    if (letra == "")
        //        tamaño++;
        //    else
        //        tamaño = this.TamañoMaximo - 1;
        //    if (Regex.IsMatch(numero.ToString(), @"^[9]{" + tamaño.ToString() + "}"))
        //    {
        //        /* Son todos 9
        //         * Aumentas en 1 la letra. Pones los numeros en 0
        //         */
        //        byte[] letraNum = Encoding.ASCII.GetBytes(letra);
        //        if (letraNum.Count() == 0)
        //        {
        //            letraNum = new byte[1];
        //            letraNum[0] = 65;
        //        }
        //        else
        //        {
        //            if (letraNum[0] == 90 || letraNum[0] == 122)
        //            {
        //                throw new IndexOutOfRangeException("Final de la capacidad de numeracion");
        //            }
        //            letraNum[0]++;
        //        }
        //        if (this.TamañoMaximo == 1)
        //        {
        //            codigo = Encoding.ASCII.GetChars(letraNum)[0].ToString().PadRight(this.TamañoMaximo - 1, '0');
        //        }
        //        else
        //            codigo = Encoding.ASCII.GetChars(letraNum)[0].ToString().PadRight(this.TamañoMaximo - 1, '0') + "1";
        //    }
        //    else
        //    {
        //        //no son todos 9
        //        numero++;
        //        if (letra != null && letra != "")
        //            codigo = letra + numero.ToString().Trim().PadLeft(this.TamañoMaximo - 1, '0');
        //        else
        //            codigo = numero.ToString().Trim().PadLeft(this.TamañoMaximo, '0');
        //    }
        //    return codigo;
        //}

        public string ProximoCodigoDisponibleConPrefijo(string prefijo, string resto, int tamañomax)
        {
            long i = 0;
            var listaCodigos = new List<string>();
            long max = long.Parse("9".PadLeft(tamañomax, '9'));
            long elElegido = 0; //el codigo elegido en long. se retorna transformado a string al fin del metodo en el caso de que no se busque un 
            long LongDesde = 0;
            long codigoLong = 0;

            if (!Regex.IsMatch(resto.ToString(), @"[a-zA-Z]")) //significa que no contiene letras. si contiene numeros al entrar acá debería hacer el padright en el buscador? ej: desde = A1; deberia traer los codigos que empiezan con A1, A1001,A1002,A1004, etc
            {
                if (long.TryParse(resto, out LongDesde))
                    do
                    {
                        listaCodigos.Clear();
                        listaCodigos = buscador.ObtenerListaCodigos<TEntidad>(prefijo, LongDesde, tamañomax);
                        if (listaCodigos.Count > 0)
                        {
                            i = LongDesde;
                            foreach (var codigo in listaCodigos)
                            {
                                if (long.TryParse(codigo.Remove(0, 1), out codigoLong))
                                    if (codigoLong > i)
                                    {
                                        elElegido = i;
                                        break;
                                    }
                                    else
                                        if (i == max)
                                        {
                                            prefijo = this.IncrementaLetra(prefijo);
                                            LongDesde = 1;
                                            i = 1;
                                            break;
                                        }
                                        else
                                            i++; //VERIFICAR QUE NO SUPERE EL tamañomax, ejemplo: busco codigo disponible a partir de A99. traigo los codigos superiores a A99, 1 solo (A99), pasa por el bucle y suma 1. queda A100 no sirve, 3 digitos maximo.
                            }
                            LongDesde = i;
                        }
                        else
                        {
                            return prefijo.ToUpperInvariant() + (LongDesde).ToString().PadLeft(tamañomax, '0');
                        }
                    }
                    while (elElegido == 0);
            }
            return prefijo.ToUpperInvariant() + elElegido.ToString().PadLeft(tamañomax, '0');
        }

        public string ProximoCodigoDisponibleSoloNumero(long LongDesde, int tamañoMaximo)
        {
            long i = 0;
            var listaCodigos = new List<string>();
            long elElegido = 0; //el codigo elegido en long. se retorna transformado a string al fin del metodo en el caso de que no se busque un 
            long max = long.Parse("9".PadLeft(tamañoMaximo, '9'));
            long codigoLong = 0;
            do
            {
                listaCodigos.Clear();
                listaCodigos = buscador.ObtenerListaCodigos<TEntidad>(LongDesde, tamañoMaximo);
                if (listaCodigos.Count > 0)
                {
                    i = LongDesde;
                    foreach (var codigo in listaCodigos)
                    {
                        if (long.TryParse(codigo, out codigoLong))
                            if (codigoLong > i)
                            {
                                elElegido = i;
                                break;
                            }
                            else
                                if (i == max)
                                {
                                    var prefijo = "A";
                                    var tamañomax = tamañoMaximo - 1;
                                    var resto = "1".PadLeft(tamañomax, '0');
                                    return this.ProximoCodigoDisponibleConPrefijo(prefijo, resto, tamañomax);
                                }
                                else
                                    i++;
                    }
                    LongDesde = i;
                }
                else
                {
                    if (LongDesde < max)
                        elElegido = LongDesde;
                    else
                    {
                        var prefijo = "A";
                        var tamañomax = tamañoMaximo - 1;
                        var resto = "1".PadLeft(tamañomax, '0');
                        return this.ProximoCodigoDisponibleConPrefijo(prefijo, resto, tamañomax);
                    }
                }
            }
            while (elElegido == 0);

            return elElegido.ToString().PadLeft(tamañoMaximo, '0');
        }

    }
}
