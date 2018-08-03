using System;
namespace Inteldev.Datos
{
    interface IImportador
    {
        void AgregarLectordeTabla(ILectorDeTabla lector, TipoInsert tipoInsert);
        bool Ejecutar();
    }
}
