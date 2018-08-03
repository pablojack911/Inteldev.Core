using Inteldev.Core.Datos;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Auditoria;
using Inteldev.Core.Modelo.Usuarios;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Inteldev.Core.Negocios
{

    public class BorradorGenerico<TEntidad> : LogicaDeNegociosBase<TEntidad>, IBorrador<TEntidad>
        where TEntidad : EntidadBase
    {
        public bool GrabarCambios { get; set; }

        protected List<EntidadBase> listaEntidadesParaBorrar;

        public BorradorGenerico(string empresa, string entidad)
            : base(empresa, entidad)
        {
            this.GrabarCambios = true;
            //this.listaEntidadesParaBorrar = new List<EntidadBase>(); //aunque solo se usa en BorradorCliente
        }

        public ErrorCarrier Borrar(TEntidad entidad, Usuario Usuario)
        {
            //MODIFICACIONES 2015
            var listaContextos = this.Contexto.ObtenerContextos(typeof(TEntidad));

            var errorCarrier = new ErrorCarrier();

            listaContextos.ForEach(cntxt =>
            {

                this.Eliminar(entidad, Usuario, cntxt);

                if (this.GrabarCambios)
                    try
                    {
                        cntxt.SaveChanges();
                        //if (Usuario != null)
                        //    cntxt.insertaAuditoria<TEntidad>(entidad, Accion.Elimina, Usuario); //ESTO LO HACE DBCONTEXTBASE, Implementar en GrabadprEspecifico
                        errorCarrier.setMensaje("Datos borrados correctamente.");
                    }
                    catch (DbUpdateException ex)
                    {
                        errorCarrier.setError(false);
                        errorCarrier.setTipoError(error.ForeignKey);
                        errorCarrier.setMensaje("Error. No se puede borrar un dato que está relacionado.");
                    }
                else
                {
                    errorCarrier.setError(false);
                    errorCarrier.setMensaje("No esta listo para grabar");
                }

            });
            return errorCarrier;
        }

        public ErrorCarrier Borrar(int id, Usuario Usuario)
        {
            var entidad = this.Contexto.BuscarPorId(id, CargarRelaciones.CargarCollecciones);
            if (entidad == null)
                return new ErrorCarrier() { borroOk = false, mensaje = string.Format("Error al borrar el id = {0}", id) };
            return this.Borrar(entidad, Usuario);
        }

        public ErrorCarrier BorrarLista(List<TEntidad> listaEntidades, Usuario Usuario) //METODO NO UTILIZADO???? 
        {
            //this.Contexto.EmpezarTransaccion(); //MODIFICACIONES 2015 - ELIMINADO
            foreach (var item in listaEntidades)
            {
                var errorCarrier = this.Borrar(item, Usuario);
                if (!errorCarrier.borroOk)
                {
                    //this.Contexto.RollbackTransaccion(); //MODIFICACIONES 2015 - ELIMINADO
                    return errorCarrier;
                }
            }
            //si todo salio bien
            //this.Contexto.CommitTransaccion();//MODIFICACIONES 2015 - ELIMINADO
            //this.Contexto.DisposeTransaccion();//MODIFICACIONES 2015 - ELIMINADO
            return new ErrorCarrier();
        }

        public ErrorCarrier Borrar(System.Collections.Generic.IList<TEntidad> listaEntidad, Usuario Usuario)
        {
            bool GrabarCambiosAnterior = this.GrabarCambios;
            if (this.GrabarCambios)
            {
                this.GrabarCambios = false;
            }
            ErrorCarrier result = new ErrorCarrier();
            foreach (var entidad in listaEntidad)
            {
                result = this.Borrar(entidad, Usuario);
            }

            //this.GrabarCambios = this.GrabarCambios;

            //this.Contexto.SaveChanges();

            return result;
        }

        public virtual void Eliminar(TEntidad entidad, Usuario usuario, IDbContext cntxt)
        {
            cntxt.Borrar<TEntidad>(entidad, usuario);
        }

        //METODOS UTILIZADOS EN LOS BORRADORES ESPECIFICOS - MANEJO DE ENTIDADES PARA BORRAR
        /// <summary>
        /// Metodo que agrega una entidad perteneciente al grafo de una entidad que se va a borrar a la lista de entidades para eliminar de listaEntidadesParaBorrar
        /// </summary>
        /// <param name="entidad">Entidad a agregar a listaEntidadesParaBorrar</param>
        public void AgregarAListaParaBorrar(EntidadBase entidad)
        {
            if (entidad != null)
                this.listaEntidadesParaBorrar.Add(entidad);

        }

        /// <summary>
        /// Metodo que elimina todas las entidades en listaEntidadesParaBorrar de un contexto pasado por parametro
        /// </summary>
        /// <param name="cntxt">Contexto que se utiliza para elimiar las entidades en la listaEntidadesParaBorrar</param>
        public void EliminarGrafo(IDbContext cntxt)
        {
            this.listaEntidadesParaBorrar.ForEach(b => cntxt.Entry(b).State = EntityState.Deleted);
        }

        /// <summary>
        /// Metodo utilizado por ImportadorDatos para la tabla de PadronIIBB
        /// </summary>
        /// <returns>Error carrier con el error si surgió</returns>
        public ErrorCarrier BorrarTodo()
        {
            return new ErrorCarrier() { borroOk = this.Contexto.BorrarTodo() };
        }

    }
}
