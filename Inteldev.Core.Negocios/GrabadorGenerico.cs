using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Inteldev.Core.Datos;
using Inteldev.Core.Metadatos;
using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Usuarios;
using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.Negocios.Mapeador;
using Microsoft.Practices.Unity;
using System.Data.Entity;


namespace Inteldev.Core.Negocios
{
    /// <summary>
    /// Graba cualquier tipo de entidad
    /// </summary>
    /// <typeparam name="TEntidad">tipo de entidad con la cual trabajar. Solo acepta entidades que implementan
    /// EntidadBase</typeparam>
    /// <seealso cref="EntidadBase"/>
    public class GrabadorGenerico<TEntidad> : LogicaDeNegociosBase<TEntidad>, IGrabador<TEntidad>
        where TEntidad : EntidadMaestro
    {
        #region Campos
        /// <summary>
        /// Se graban los cambios o no?
        /// </summary>
        public bool grabarDatos { get; set; }

        /// <summary>
        /// Usuario que realizó la grabación 
        /// </summary>
        private Usuario usuario;

        /// <summary>
        /// Objeto que devuelvo. Contiene si hubo error o no y el mensaje
        /// </summary>
        protected GrabadorCarrier grabadorHelper;

        private Inteldev.Core.Negocios.Validadores.IValidador<TEntidad> validador;

        /// <summary>
        /// Entidad sobre la cual realizar las operaciones
        /// </summary>
        private TEntidad entidad;

        /// <summary>
        /// Generador del próximo número de ID
        /// </summary>
        private INumerador<TEntidad> numerador;

        private ParameterOverride[] parameter;

        private string empresa;
        #endregion Campos

        #region Constructor
        public GrabadorGenerico(string empresa, string entidad, Inteldev.Core.Negocios.Validadores.IValidador<TEntidad> validador)
            : base(empresa, entidad)
        {
            this.grabadorHelper = new GrabadorCarrier();
            this.parameter = new ParameterOverride[2];
            this.parameter[0] = new ParameterOverride("empresa", empresa);
            this.empresa = empresa;
            this.parameter[1] = new ParameterOverride("entidad", typeof(TEntidad).Name);
            this.numerador = (INumerador<TEntidad>)FabricaNegocios.Instancia.Resolver(typeof(INumerador<TEntidad>), this.parameter);
            this.validador = validador;
            this.grabarDatos = true;
        }
        #endregion Constructor

        #region Grabador para importador

        public GrabadorCarrier GrabarListaImportador(List<TEntidad> listaEntidades, Usuario Usuario = null)
        {
            //this.Contexto.EmpezarTransaccion();
            foreach (var item in listaEntidades)
            {
                var grabadorCarrier = this.GrabarImportador(item, Usuario);
                //var grabadorCarrier = this.Grabar(item, Usuario);

                //if (grabadorCarrier.getError())
                //{
                //    this.Contexto.RollbackTransaccion();
                //    return grabadorCarrier;
                //}
            }

            return this.GuardarCambios();
            //si todo salio bien
            //this.Contexto.CommitTransaccion();
            //this.Contexto.DisposeTransaccion();
            //return new GrabadorCarrier();
        }
        private object GrabarImportador(TEntidad item, Usuario Usuario = null) //usado para el importador solamente. Fixius llama a GrabarNuevo y GrabarExistente directamente
        {
            this.entidad = item;
            this.usuario = Usuario;
            //this.AntesDeGrabar(this.entidad);
            if (Existe(this.entidad))
            {
                this.grabadorHelper = GrabarExistenteParaImportador(this.entidad, Usuario);
            }
            else
            {
                this.grabadorHelper = GrabarNuevoParaImportador(this.entidad, Usuario);
            }
            return this.grabadorHelper;
        }

        private GrabadorCarrier GrabarNuevoParaImportador(TEntidad tEntidad, Usuario Usuario = null)
        {
            this.entidad = tEntidad;
            this.usuario = Usuario;
            if (entidad.Codigo != null)
            {
                entidad.Codigo = entidad.Codigo.Trim().PadLeft(this.numerador.TamañoMaximo, '0');//agregado por Pocho
            }
            string mensajeError;
            if (this.validador.ExisteError(tEntidad, this.empresa, out mensajeError))
            {
                this.grabadorHelper.setMensaje(mensajeError);
                this.grabadorHelper.setError(true);

            }
            else
            {
                if (this.entidad.Codigo == null || this.entidad.Codigo == "0".PadLeft(this.numerador.TamañoMaximo, '0') || this.entidad.Codigo == "")
                {
                    entidad.Codigo = this.numerador.ProximoCodigo(entidad);
                }
                //MODIFICACIONES 2015
                //this.Contexto.Insertar(tEntidad, Usuario); //se agrega al contexto pero no se guarda hasta que GrabarListaImportador invoque el GuardarCambios
                this.Insertar(tEntidad, Usuario, this.Contexto.ObtenerContextos<TEntidad>());
            }
            return this.grabadorHelper;
        }
        /// <summary>
        /// 2015 - Este metodo inserta un elemento por cada contexto que contenga la entidad
        /// </summary>
        /// <param name="tEntidad">Elemento a insertar en el contexto</param>
        /// <param name="Usuario">Usuario responsable de la insercion</param>
        public virtual void Insertar(TEntidad tEntidad, Usuario Usuario, List<IDbContext> listaContextos)
        {
            //var listaContextos = this.Contexto.ObtenerContextos<TEntidad>();
            listaContextos.ForEach(x =>
            {
                x.Insertar<TEntidad>(tEntidad, Usuario); //Insertar DE DBCONTEXT
                x.SaveChanges();
            });
        }

        private GrabadorCarrier GrabarExistenteParaImportador(TEntidad tEntidad, Usuario Usuario = null)
        {
            this.entidad = tEntidad;
            this.usuario = Usuario;
            //MODIFICACIONES 2015
            //this.Contexto.Actualizar(tEntidad, Usuario); //se actualiza el contexto pero no se guarda hasta que GrabarListaImportador invoque el GuardarCambios
            this.Actualizar(tEntidad, Usuario, this.Contexto.ObtenerContextos<TEntidad>());
            return this.grabadorHelper;
        }
        /// <summary>
        /// 2015 - Este método actualiza un elemento en los contextos a los que pertenezca.
        /// </summary>
        /// <param name="tEntidad">Elemento a actualizar.</param>
        /// <param name="Usuario">Usuario que realiza la actualización.</param>
        public virtual void Actualizar(TEntidad tEntidad, Usuario Usuario, List<IDbContext> listaContextos)
        {
            //var listaContextos = this.Contexto.ObtenerContextos<TEntidad>();
            listaContextos.ForEach(x =>
            {
                x.Actualizar<TEntidad>(tEntidad, Usuario);
                x.SaveChanges();
            });
        }

        #endregion Grabador para importador

        #region Grabador Normal (FIXIUS UTILIZA ESTOS)

        public GrabadorCarrier Grabar(TEntidad Entidad, Usuario Usuario = null)
        {
            this.entidad = Entidad;
            this.usuario = Usuario;
            //this.AntesDeGrabar(this.entidad);
            if (Existe(this.entidad))
            {
                this.grabadorHelper = GrabarExistente(this.entidad, Usuario);
            }
            else
            {
                this.grabadorHelper = GrabarNuevo(this.entidad, Usuario);
            }

            if (this.grabarDatos)
            {
                this.GuardarCambios();
            }

            return this.grabadorHelper;
        }

        /// <summary>
        /// Graba la entidad como un nuevo objeto en ves de sobreescribir el actual
        /// </summary>
        /// <param name="Entidad">Entidad a guardar</param>
        /// <param name="Usuario">Usuario que hace la accion</param> 
        /// <returns>True si inserto bien</returns>
        public virtual GrabadorCarrier GrabarNuevo(TEntidad Entidad, Usuario Usuario = null)
        {
            this.entidad = Entidad;
            this.usuario = Usuario;
            if (entidad.Codigo != null)
            {
                entidad.Codigo = entidad.Codigo.Trim().PadLeft(this.numerador.TamañoMaximo, '0');//agregado por Pocho
            }
            string mensajeError;
            if (this.validador.ExisteError(Entidad, this.empresa, out mensajeError))
            {
                this.grabadorHelper.setMensaje(mensajeError);
                this.grabadorHelper.setError(true);
            }
            else
            {
                if (this.entidad.Codigo == null || this.entidad.Codigo == "0".PadLeft(this.numerador.TamañoMaximo, '0') || this.entidad.Codigo == "")
                {
                    entidad.Codigo = this.numerador.ProximoCodigo(entidad);
                    this.grabadorHelper.setMensaje(string.Format("Datos grabados correctamente. El código es: {0}", entidad.Codigo));
                }
                //MODIFICACIONES 2015
                //this.Contexto.Insertar(Entidad, Usuario);
                this.Insertar(Entidad, Usuario, this.Contexto.ObtenerContextos<TEntidad>());
                /*this.GuardarCambios(); //SI DESCOMENTAR -> eliminar GuardarCambios de IGrabador y su invocación en GrabadorDTO
                if (!this.grabadorHelper.getError())
                    this.grabadorHelper.setMensaje(string.Format("Grabado Correctamente. El codigo es: {0}", this.entidad.Codigo));
                 */

            }
            return this.grabadorHelper;
        }

        /// <summary>
        /// Graba los cambios en la entidad actual.
        /// </summary>
        /// <param name="Entidad">Entidad a grabar</param>
        /// <returns>true si grabo bien, false si no lo hizo</returns>
        public virtual GrabadorCarrier GrabarExistente(TEntidad Entidad, Usuario Usuario = null)
        {
            this.entidad = Entidad;
            this.usuario = Usuario;
            //MODIFICACIONES 2015
            //this.Contexto.Actualizar(Entidad, Usuario);//2015 - NO MAS! DE AHORA EN ADELANTE, SE HARA POR especifico
            this.Actualizar(Entidad, Usuario, this.Contexto.ObtenerContextos<TEntidad>());
            //this.GuardarCambios();
            return this.grabadorHelper;
        }

        #endregion Grabador Normal (FIXIUS UTILIZA ESTOS)

        /// <summary>
        /// Existe la entidad o no existe?
        /// </summary>
        /// <param name="Entidad">entidad a buscar</param>
        /// <returns>True si existe. False si no.</returns>
        public bool Existe(TEntidad Entidad)
        {
            return (MetaDatos.ObtenerValor<int>(Entidad, "Id") != 0);
        }

        public GrabadorCarrier GuardarCambios()
        {
            GrabadorCarrier s = new GrabadorCarrier();
            //MODIFICACIONES 2015
            //var concu = this.Contexto.SaveChanges();
            var listaContextos = this.Contexto.ObtenerContextos(typeof(TEntidad));
            List<Inteldev.Core.Datos.EvaluarConcurrencia> concu = new List<Datos.EvaluarConcurrencia>();
            foreach (var contexto in listaContextos)
            {
                concu.Add(contexto.SaveChanges());
            }
            var mapeador = FabricaNegocios._Resolver<IMapeadorGenerico<Inteldev.Core.Datos.EvaluarConcurrencia, Inteldev.Core.DTO.Carriers.EvaluarConcurrencia>>();
            s.setConcurrencia(mapeador.EntidadToDto(concu.FirstOrDefault())); //no podia hacer otra cosa... REVISAR!!
            //DespuesDeGrabar();
            return s;
        }

        #region NO IMPLEMENTADO
        /// <summary>
        /// Acciones a realizar antes de grabar una entidad
        /// </summary>
        /// <param name="Entidad">Entidad a grabar</param>
        public virtual void AntesDeGrabar(TEntidad Entidad)
        {
        }

        public virtual void DespuesDeGrabar()
        {
            //var oFox = FabricaNegocios._Resolver<IGrabadorFox<TEntidad>>();

            //if (oFox != null)
            //    oFox.Grabar(this.entidad);
        }
        #endregion NO IMPLEMENTADO

        #region Metodos nuevos - 2015

        //METODOS NUEVOS - 2015
        /// <summary>
        /// Metodo encargado de hacer el pasaje de valores simples de una entidad a otra.
        /// </summary>
        /// <param name="origen">Entidad de origen</param>
        /// <param name="destino">Entidad de destino</param>
        /// <param name="cntxt">Contexto donde se encuentran las entidades</param>
        public void SetearValores(EntidadBase origen, EntidadBase destino, IDbContext cntxt)
        {
            if (destino != null && origen != null)
                cntxt.Entry(destino).CurrentValues.SetValues(origen);
        }
        /// <summary>
        /// Metodo utilizado para actualizar las propiedades de referencia.
        /// </summary>
        /// <param name="entidad">Entidad a actualizar</param>
        /// <param name="Propiedad">Nombre de la propiedad de referencia</param>
        /// <returns>Id de la FK</returns>
        public int? SetearFk(EntidadBase entidad, string Propiedad)
        {
            var prop = entidad.GetType().GetProperty(Propiedad);
            EntidadBase valorProp = (EntidadBase)prop.GetValue(entidad);

            int? fk = null;
            if (valorProp != null)
            {
                if (valorProp.Id != 0)
                    fk = valorProp.Id;
                prop.SetValue(entidad, null);
            }
            return fk;
        }

        /// <summary>
        /// Actualiza en el contexto entre 2 listas de valores.
        /// </summary>
        /// <typeparam name="TipoEntidad">Tipo de la entidad de las listas.</typeparam>
        /// <param name="actualizar">Lista de entidades tal cual como esta en la base de datos hasta el momento.</param>
        /// <param name="modificado">Lista de entidades actualizadas desde el frente.</param>
        /// <param name="cntxt">Contexto actual de trabajo.</param>
        /// <param name="setsFk">Accion que se ejecuta para setear las FK</param>
        public void ActualizarColeccionUnoAMuchos<TipoEntidad>(ICollection<TipoEntidad> actualizar,
                                                   ICollection<TipoEntidad> modificado,
                                                   IDbContext cntxt,
                                                   Action<TipoEntidad> setsFk = null) where TipoEntidad : EntidadBase
        {
            BorrarEntidades<TipoEntidad>(actualizar, modificado, cntxt, setsFk);

            ModificarEntidades<TipoEntidad>(actualizar, modificado, cntxt, setsFk);

            DarDeAltaEntidades<TipoEntidad>(actualizar, modificado, cntxt, setsFk);
        }

        /// <summary>
        /// Metodo para actualizar los valores entre dos colecciones que mantienen una relacion Muchos a Muchos
        /// </summary>
        /// <typeparam name="TipoEntidad">Tipo de la entidad de las listas</typeparam>
        /// <param name="actualizar">Lista de entidades tal cual como esta en la base de datos hasta el momento</param>
        /// <param name="modificado">Lista de entidades actualizadas desde el frente</param>
        /// <param name="cntxt">Contexto actual de trabajo</param>
        /// <param name="setsFk">Accion que se ejecuta para setear las FK</param>
        public void ActualizarColeccionMuchosAMuchos<TipoEntidad>(ICollection<TipoEntidad> actualizar,
                                                   ICollection<TipoEntidad> modificado,
                                                   IDbContext cntxt,
                                                   Action<TipoEntidad> setsFk = null) where TipoEntidad : EntidadBase
        {
            BorrarRelaciones<TipoEntidad>(actualizar, modificado, cntxt, setsFk);
            AgregarEntidades<TipoEntidad>(actualizar, modificado, cntxt, setsFk);
        }

        private void DarDeAltaEntidades<TipoEntidad>(ICollection<TipoEntidad> actualizar, ICollection<TipoEntidad> modificado, IDbContext cntxt, Action<TipoEntidad> setsFk) where TipoEntidad : EntidadBase
        {
            foreach (var nuevo in modificado.Where(p => p.Id == 0))
            {
                //actualiza las fk de la entidad, si tiene
                if (setsFk != null)
                {
                    setsFk(nuevo);
                }
                actualizar.Add(nuevo);
            }
        }

        private void ModificarEntidades<TipoEntidad>(ICollection<TipoEntidad> actualizar, ICollection<TipoEntidad> modificado, IDbContext cntxt, Action<TipoEntidad> setsFk) where TipoEntidad : EntidadBase
        {
            modificado.Where(p => p.Id > 0).ToList().ForEach(x =>
            {
                var actu = actualizar.FirstOrDefault(o => o.Id > 0 && o.Id == x.Id);
                if (actu != null)
                {
                    if (setsFk != null)
                    {
                        setsFk(x);
                    }
                    cntxt.Entry<TipoEntidad>(actu).CurrentValues.SetValues(x);
                    cntxt.Entry<TipoEntidad>(actu).State = EntityState.Modified;
                }
            });
        }

        /// <summary>
        /// Metodo utilizado para borrar definitivamente elementos en la base de datos. Utilizado para las listas uno a muchos.
        /// </summary>
        /// <typeparam name="TipoEntidad">Tipo de la entidad que se trabaja</typeparam>
        /// <param name="actualizar">Coleccion de entidades de la base</param>
        /// <param name="modificado">Coleccion de entidades modificadas en el frente</param>
        /// <param name="cntxt">Contexto sobre el cual se trabaja</param>
        /// <param name="setsFk">Accion que se utilizaria para setear las Fk</param>
        private void BorrarEntidades<TipoEntidad>(ICollection<TipoEntidad> actualizar, ICollection<TipoEntidad> modificado, IDbContext cntxt, Action<TipoEntidad> setsFk) where TipoEntidad : EntidadBase
        {
            var idsEntidadesParaBorrar = actualizar.Select(gd => gd.Id).Except(modificado.Select(gd => gd.Id));

            var entidadesParaBorrar = new List<TipoEntidad>();

            if (idsEntidadesParaBorrar.Count() > 0)
            {
                foreach (var id in idsEntidadesParaBorrar)
                {
                    var entidad = actualizar.FirstOrDefault(e => e.Id == id);
                    if (entidad != null)
                    {
                        if (!entidadesParaBorrar.Contains(entidad))
                            entidadesParaBorrar.Add(entidad);
                    }
                }
            }

            entidadesParaBorrar.ForEach(ent => cntxt.Entry(ent).State = EntityState.Deleted);
        }

        private void AgregarEntidades<TipoEntidad>(ICollection<TipoEntidad> actualizar, ICollection<TipoEntidad> modificado, IDbContext cntxt, Action<TipoEntidad> setsFk = null) where TipoEntidad : EntidadBase
        {
            var listaIds = actualizar.Select(gd => gd.Id);

            var listaIdsActualizados = modificado.Select(gd => gd.Id);

            var entidadesParaAgregar = new List<TipoEntidad>();

            var idsEntidadesParaAgregar = listaIdsActualizados.Except(listaIds);

            if (idsEntidadesParaAgregar.Count() > 0)
            {
                foreach (var id in idsEntidadesParaAgregar)
                {
                    //var entidad = cntxt.BuscarPorId<TEntidad>(id, CargarRelaciones.NoCargarNada);
                    var entidad = modificado.FirstOrDefault<TipoEntidad>(p => p.Id > 0 && p.Id == id);
                    if (entidad != null)
                    {
                        if (!entidadesParaAgregar.Contains(entidad))
                            entidadesParaAgregar.Add(entidad);
                    }
                }
            }
            entidadesParaAgregar.ForEach(ent =>
            {
                //actualiza las fk de la entidad, si tiene
                if (setsFk != null)
                {
                    setsFk(ent);
                }
                cntxt.Entry<TipoEntidad>(ent).State = EntityState.Modified;
                actualizar.Add(ent);

            });

        }

        private void BorrarRelaciones<TipoEntidad>(ICollection<TipoEntidad> actualizar, ICollection<TipoEntidad> modificado, IDbContext cntxt, Action<TipoEntidad> setsFk) where TipoEntidad : EntidadBase
        {
            var idsEntidadesParaBorrar = actualizar.Select(gd => gd.Id).Except(modificado.Select(gd => gd.Id));

            var entidadesParaBorrar = new List<TipoEntidad>();

            if (idsEntidadesParaBorrar.Count() > 0)
            {
                foreach (var id in idsEntidadesParaBorrar)
                {
                    var entidad = actualizar.FirstOrDefault(e => e.Id == id);
                    if (entidad != null)
                    {
                        if (!entidadesParaBorrar.Contains(entidad))
                            entidadesParaBorrar.Add(entidad);
                    }
                }
            }

            entidadesParaBorrar.ForEach(ent => actualizar.Remove(ent));
        }

        #endregion

    }
}
