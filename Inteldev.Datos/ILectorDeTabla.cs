using System;
namespace Inteldev.Datos
{
    public interface ILectorDeTabla
    {
        System.Data.DataTable Ejecutar();
        string Tabla { get; set; }
    }
}
