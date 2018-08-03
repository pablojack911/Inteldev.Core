using System;
namespace Inteldev.Core.Presentacion.Presentadores
{
    public interface IPresentadorABM
    {
        void Ejecutar();
        string Titulo { get; set; }
    }
}
