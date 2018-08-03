using System;
using System.Collections.Generic;
using System.Windows;

namespace Inteldev.Core.Presentacion.Presentadores.Interfaces
{
    public interface IPresentadorABM<TEntidad, TVistaModelo> : IPresentadorBase<TEntidad>
        where TEntidad : Inteldev.Core.DTO.DTOMaestro, new()
        where TVistaModelo : VistasModelos.VistaModeloBase<TEntidad>, new()
    {
        void AbrirEntidad(TEntidad entidad);
        Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorBuscador<TEntidad> Buscador { get; set; }
        void Cancelar();
        System.Windows.Input.ICommand CmdBorrar { get; set; }
        System.Windows.Input.ICommand CmdCerrarPestaña { get; set; }
        System.Windows.Input.ICommand CmdClonar { get; set; }
        System.Windows.Input.ICommand CmdEditar { get; set; }
        System.Windows.Input.ICommand CmdGrabar { get; set; }
        System.Windows.Input.ICommand CmdNuevo { get; set; }
        System.Windows.Input.ICommand CmdVer { get; set; }
        System.Windows.Input.ICommand CmdListar { get; set; }
        void Editar(TEntidad entidad);
        void Ejecutar();
        System.Collections.ObjectModel.ObservableCollection<TVistaModelo> EntidadesAbiertas { get; set; }
        string EtiquetaBotonNuevo { get; set; }
        bool PuedeBorrar();
        bool PuedeCancelar();
        bool PuedeClonar();
        bool PuedeCrearNuevo();
        bool PuedeEditar();
        bool PuedeGrabar();
        bool PuedeVer();
        bool PuedeListar();
        bool Listar();
        void Ver(TEntidad entidad);
        Type Vista { get; set; }
        Inteldev.Core.Presentacion.Controles.IBaseABM VistaABM { get; set; }
    }
}
