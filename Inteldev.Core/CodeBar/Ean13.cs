using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Core.CodeBar
{
    public class Ean13 : CodeBar
    {
        public Ean13()
            : base(13)
        { }
        protected override string GeneraDigitoVerificador()
        {
            this.pares = this.pares * 3;
            int check = this.pares + this.impares;
            int digitoControl = 10 - (check % 10);
            if (digitoControl == 10)
                digitoControl = 0;
            return digitoControl.ToString();
        }
    }
}
