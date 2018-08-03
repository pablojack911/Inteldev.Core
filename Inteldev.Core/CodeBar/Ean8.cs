using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.CodeBar
{
    public class Ean8:CodeBar
    {

        public Ean8():base(8)
        {            
        }
        protected override string GeneraDigitoVerificador()
        {
            this.impares = this.impares * 3;
            int check = this.pares + this.impares;
            int digitoControl = 10 - (check % 10);
            if (digitoControl == 10)
                digitoControl = 0;
            return digitoControl.ToString();
        }
    }
}
