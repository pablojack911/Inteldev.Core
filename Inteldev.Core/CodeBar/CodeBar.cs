using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.CodeBar
{
    public abstract class CodeBar
    {
        public CodeBar(int digitos)
        {
            this.digitos = digitos;
        }
        public bool EsValido(string codigo)
        {
            bool ok = false;

            if (codigo.Length == this.digitos)
            {
                this.digitoverificador = codigo.Last().ToString();

                codigoOriginal = codigo.Substring(0, codigo.Length - 1);

                this.sumaParesImpares();

                if (this.GeneraDigitoVerificador() == this.digitoverificador)
                    ok = true;
            }
            return ok;
        }


        protected abstract string GeneraDigitoVerificador();
        public string GenerarCodigo(string codigo)
        {
            this.codigoOriginal = codigo.TrimEnd().PadLeft(this.digitos - 1, '0');
            this.sumaParesImpares();
            this.digitoverificador = this.GeneraDigitoVerificador();

            return this.codigoOriginal + this.digitoverificador;
        }

        void sumaParesImpares()
        {
            //this.pares = codigoOriginal.TakeWhile((p, i) => i % 2 == 0).Sum(p => int.Parse(p.ToString()));

            //this.impares = codigoOriginal.TakeWhile((p, i) => i % 2 != 0).Sum(p => int.Parse(p.ToString()));
            for (int i = 1; i < codigoOriginal.Length + 1; i++)
            {
                if (i % 2 == 0)
                {
                    int p = 0;
                    //this.pares += int.Parse(codigoOriginal.Substring(i - 1, 1));
                    if (int.TryParse(codigoOriginal.Substring(i - 1, 1), out p))
                        this.pares += p;
                    

                }
                else
                {
                    //this.impares += int.Parse(codigoOriginal.Substring(i - 1, 1));
                    int p = 0;                    
                    if (int.TryParse(codigoOriginal.Substring(i - 1, 1), out p))
                        this.impares += p;
                }
            }
        }

        protected int digitos;
        protected int pares;
        protected int impares;
        protected string digitoverificador;
        protected string codigoOriginal;


        public static bool Validar<TCodeBar>(string codigo) where TCodeBar : CodeBar, new()
        {
            var code = new TCodeBar();
            return code.EsValido(codigo);
        }

        public static string Generar<TCodeBar>(string codigo) where TCodeBar : CodeBar, new()
        {
            var code = new TCodeBar();
            return code.GenerarCodigo(codigo);
        }
    }
}
