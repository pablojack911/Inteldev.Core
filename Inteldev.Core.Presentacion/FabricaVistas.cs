using Inteldev.Core.Patrones;
using Inteldev.Core.Presentacion.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Extenciones;


namespace Inteldev.Core.Presentacion
{
    public class FabricaVistas : Singleton<FabricaVistas>
    {
        private RegistroFabricaVistas registro;

        public FabricaVistas()
        {

        }

        public void CargarRegistro(RegistroFabricaVistas registro)
        {
            this.registro = registro;
        }
        public Type BuscaVista(Type nombre)
        {
            Dictionary<Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio?, Type> vistas;
            try
            {
                vistas = registro.Vistas[nombre];
                var vista = vistas[Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual];
                if (vista != null)
                    return vista;
                else
                    return registro.VistasDefault[nombre];
            }
            catch (Exception ex)
            {
                return registro.VistasDefault[nombre];
            }
        }

        public Type BuscaVista(Type nombre, TipoVista tipo)
        {
            Dictionary<Inteldev.Core.DTO.Organizacion.UnidadeDeNegocio?, Type> vistas;
            var key = new Tuple<Type, TipoVista>(nombre, tipo);
            try
            {
                vistas = registro.VistasComplejas[key];
                var vista = vistas[Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual];
                if (vista != null)
                    return vista;
                else
                    return registro.VistasComplejasDefault[key];
            }
            catch (Exception ex)
            {
                return registro.VistasComplejasDefault[key];
            }

        }
    }

    public enum TipoVista
    {
        Edicion,
        Listado
    };
}
