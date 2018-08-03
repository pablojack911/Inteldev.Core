using Inteldev.Core.Datos;
using Inteldev.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Core.DataSwitch
{
    /// <summary>
    /// Interfaz que debe usar la capa superior para poder acceder a los datos.
    /// </summary>
    public interface IDataSwitch<TEntidad>
        where TEntidad : EntidadBase
    {
        System.Linq.IQueryable<TEnt> Consultar<TEnt>(Inteldev.Core.CargarRelaciones cargarEntidadesRelacionadas) where TEnt : Inteldev.Core.Modelo.EntidadBase;
        TMaestro BuscarPorCodigo<TMaestro>(String busqueda, CargarRelaciones cargarEntidadesRelacionadas) where TMaestro : EntidadMaestro;
        TEntidad BuscarPorId(int id, CargarRelaciones cargarEntidades);
        TEntidad Crear();
        List<IDbContext> ObtenerContextos(Type TipoEntidad);
        List<IDbContext> ObtenerContextos<TipoEntidad>() where TipoEntidad : EntidadBase;

        #region ELIMINADO - 2015
        //bool Actualizar(TEntidad Entidad, Inteldev.Core.Modelo.Usuarios.Usuario Usuario);
        //void Borrar(TEntidad entidad, Inteldev.Core.Modelo.Usuarios.Usuario usuario);
        //void CommitTransaccion();
        //void DisposeTransaccion();
        //void EmpezarTransaccion();
        //bool Insertar(TEntidad Entidad, Inteldev.Core.Modelo.Usuarios.Usuario Usuario);
        bool BorrarTodo();
        //void RollbackTransaccion();
        //Inteldev.Core.Datos.EvaluarConcurrencia SaveChanges();
        #endregion
    }
}
