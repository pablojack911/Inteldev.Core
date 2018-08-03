using Inteldev.Core.Modelo;
using System;
namespace Inteldev.Core.Negocios
{
    public interface INumerador<TEntidad> where TEntidad : EntidadMaestro
    {
        string ProximoCodigo(TEntidad entidad = null);
        int TamañoMaximo { get; set; }
        string UltimoCodigo();
        string IncrementaLetra(string codigoLetra);

        string ProximoCodigoDisponibleConPrefijo(string prefijo, string resto, int tamañomax);
        string ProximoCodigoDisponibleSoloNumero(long LongDesde, int tamañoMaximo);
    }
}
