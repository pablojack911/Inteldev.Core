using System.Collections.Generic;
using Inteldev.Core.Modelo.Fiscal;
using System;

namespace Inteldev.Core.Modelo.Organizacion
{
    public partial class Empresa : EntidadMaestro
    {
        public Empresa()
        {
            this.FechaDesde = DateTime.Now;
            this.FechaHasta = DateTime.Now;
        }
        public string RazonSocial { get; set; } 
        public CondicionAnteIva CondicionAnteIVA { get; set; }
        public string CUIT { get; set; }
        //public virtual CondicionAnteIibb CondicionAnteIIBB { get; set; }
        public string IIBB { get; set; }
        //public ICollection<UnidadeDeNegocio> UnidadesDeNegocios { get; set; }        
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
