using Inteldev.Core.DTO;
using System;
using System.Collections.Generic;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{

    public interface IPresentadorBuscador<TEntidad> : IPresentadorBuscador
     where TEntidad : Inteldev.Core.DTO.DTOMaestro, new()
    {
        event PresentadorBuscador<TEntidad>.CambioItemEventHandler CambioItem;
        System.Windows.Input.ICommand CmdBuscar { get; }
        System.Windows.Input.ICommand CmdSeleccionarItem { get; set; }
        System.Windows.Input.ICommand CmdSeleccionarResultado { get; set; }
        System.Collections.ObjectModel.ObservableCollection<TEntidad> Items { get; set; }
        DTOMaestro ItemSeleccionado { get; set; }
        bool ObtenerResultados(string param);
        TEntidad Entidad { get; set; }
        System.Collections.ObjectModel.ObservableCollection<Inteldev.Core.DTO.ResultadoBusqueda<TEntidad>> Resultados { get; set; }
        Inteldev.Core.DTO.ResultadoBusqueda<TEntidad> ResultadoSeleccionado { get; set; }
        Inteldev.Core.Contratos.IServicioABM<TEntidad> Servicio { get; set; }
        Func<string, ListaParametrosDeBusqueda, System.Collections.Generic.List<Inteldev.Core.DTO.ResultadoBusqueda<TEntidad>>> ServicioBuscar { get; set; }
        string textoBusqueda { get; set; }
        Action<TEntidad> ActualizarDto { get; set; }
        List<string> ListaOmitidos { get; set; }
    }

}
