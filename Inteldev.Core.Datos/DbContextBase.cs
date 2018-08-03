using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Auditoria;
using Inteldev.Core.Modelo.Usuarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Inteldev.Core.Datos
{
    /// <summary>
    /// Contexto Base. Comentarios sobre metodos estan en <see cref="IDbContext"/>
    /// </summary>
    public abstract class DbContextBase : DbContext, IDbContext
    {
        /// <summary>
        /// Constructor. Usa base de datos por defecto
        /// </summary>
        public DbContextBase()
            : base()
        {
            //this.Configuration.LazyLoadingEnabled = true;
            this.cacheCollecciones = new Dictionary<object, ArrayList>();
            this.auditor = new Auditor();
            this.itemsEliminados = new HashSet<object>();
            this.itemsModificados = new HashSet<object>();
        }


        private Auditor auditor;
        private EvaluarConcurrencia concurrencia;
        private Dictionary<object, ArrayList> cacheCollecciones;

        /// <summary>
        /// Constructor. Usa una base especifica
        /// </summary>
        /// <param name="nombre">Nombre de la base de datos</param>
        public DbContextBase(string nombre)
            : base(nombre)
        {
            this.cacheCollecciones = new Dictionary<object, ArrayList>();
            this.auditor = new Auditor();
            this.itemsEliminados = new HashSet<object>();
            this.itemsModificados = new HashSet<object>();
        }

        public DbContextTransaction EmpezarTransaccion()
        {
            return base.Database.BeginTransaction();
        }

        public new IDbSet<TEntidad> Set<TEntidad>() where TEntidad : EntidadBase
        {
            return base.Set<TEntidad>();
        }

        public new DbSet Set(Type tipo)
        {
            return base.Set(tipo);
        }

        public new DbEntityEntry<TEntidad> Entry<TEntidad>(TEntidad entidad) where TEntidad : EntidadBase
        {
            return base.Entry<TEntidad>(entidad);
        }

        public new EvaluarConcurrencia SaveChanges()
        {
            try
            {
                this.ChangeTracker.DetectChanges();
                base.SaveChanges();
                //this.Configuration.AutoDetectChangesEnabled = true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.concurrencia = new EvaluarConcurrencia(ex);
                this.concurrencia.ObtenerResultado(new List<Inteldev.Core.Datos.EvaluarConcurrencia.ValoresDeConcurrencia>());
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (InvalidOperationException e)
            {
                return concurrencia;
            }
            return concurrencia;
        }

        public void MarcarComoBorrado(object objeto)
        {
            //base.Entry(objeto).State = System.Data.EntityState.Deleted;
            if (base.Entry(objeto).State == EntityState.Detached)
                base.Entry(objeto).State = EntityState.Deleted;
            else
            {
                base.Entry(objeto).State = EntityState.Detached;
                base.Entry(objeto).State = EntityState.Deleted;
            }
        }

        public void MarcarComoModificado(object objeto)
        {
            //base.Entry(objeto).State = System.Data.EntityState.Modified;
            base.Entry(objeto).State = EntityState.Modified;
        }

        public void MarcarComoNoModificado(object objeto)
        {
            //base.Entry(objeto).State = System.Data.EntityState.Unchanged;
            base.Entry(objeto).State = EntityState.Unchanged;
        }

        public void MarcarComoAgregado(object objeto)
        {
            //base.Entry(objeto).State = System.Data.EntityState.Added;
            base.Entry(objeto).State = EntityState.Added;
        }

        public bool EstaBorrado(object objeto)
        {
            //return base.Entry(objeto).State == System.Data.EntityState.Deleted;
            return base.Entry(objeto).State == EntityState.Deleted;
        }

        public bool EstaDetached(object objeto)
        {
            return base.Entry(objeto).State == EntityState.Detached;
            //var obj = objeto as EntidadBase;
            //var local = this.Set(objeto.GetType()).Local.Cast<EntidadBase>();
            //return !local.Any(p => p.Id == obj.Id);

        }

        public bool EstaAgregado(object objeto)
        {
            return base.Entry(objeto).State == EntityState.Added;
        }
        public bool EstaModificado(object objeto)
        {
            //return base.Entry(objeto).State == System.Data.EntityState.Modified;
            return base.Entry(objeto).State == EntityState.Modified;
        }

        List<Type> antirecursivo;
        public IQueryable<TEntidad> IncluirRelaciones<TEntidad>(IQueryable<TEntidad> objeto, CargarRelaciones cargarRelaciones) where TEntidad : EntidadBase
        {
            this.antirecursivo = new List<Type>();
            var tipoEntidad = typeof(TEntidad);
            var propiedad = tipoEntidad.GetProperties();
            foreach (var prop in propiedad)
            {
                this.antirecursivo.Clear();
                this.antirecursivo.Add(tipoEntidad);

                //es una entidad
                if (prop.PropertyType.GetProperty("Id") != null
                    && (cargarRelaciones == CargarRelaciones.CargarTodo || cargarRelaciones == CargarRelaciones.CargarEntidades)
                    && prop.GetCustomAttribute<NotMappedAttribute>() == null)
                {
                    objeto = objeto.Include(prop.Name);
                    if (prop.GetCustomAttribute<UnoAUno>() != null)
                    {
                        var propiedadesHijo = prop.PropertyType.GetProperties();
                        foreach (var item in propiedadesHijo)
                        {
                            if (item.PropertyType.GetProperty("Id") != null)
                            {
                                var incluir = prop.Name + "." + item.Name;
                                objeto = objeto.Include(incluir);
                            }
                        }
                    }

                }
                else
                {
                    //es una coleccion
                    if (prop.PropertyType.GetProperty("Count") != null
                        && (cargarRelaciones == CargarRelaciones.CargarCollecciones ||
                            cargarRelaciones == CargarRelaciones.CargarTodo)
                        && (prop.GetCustomAttributes(typeof(NotMappedAttribute), true).FirstOrDefault() == null))
                    {
                        //es muchos a muchos
                        if (prop.GetCustomAttributes(typeMuchosAMuchos, true).FirstOrDefault() != null || prop.GetCustomAttributes(typeNoIncluirColecciones, true).FirstOrDefault() != null)
                        {
                            objeto = objeto.Include(prop.Name);
                        }
                        else
                        {
                            objeto = IncluirColecciones<TEntidad>(objeto, prop, "");
                        }
                    }
                }
            }
            return objeto;
        }

        private IQueryable<TEntidad> IncluirColecciones<TEntidad>(IQueryable<TEntidad> objeto, PropertyInfo prop, string nombre) where TEntidad : EntidadBase
        {

            objeto = objeto.Include(nombre + prop.Name);
            Debug.WriteLine("IncluirColecciones");
            Debug.WriteLine(prop.Name);
            if (nombre.Length == 0)
            {
                nombre = prop.Name + ".";
            }
            else
            {
                nombre = nombre + prop.Name + ".";
            }
            var tipogen = prop.PropertyType.GetGenericArguments()[0];

            foreach (var item in tipogen.GetProperties())
            {
                if (item.PropertyType.GetProperty("Count") != null)
                {
                    if (typeof(TEntidad).Name != tipogen.Name && !this.antirecursivo.Any<Type>(p => p.Name == tipogen.Name))
                    {
                        try
                        {
                            this.antirecursivo.Add(tipogen);
                            if (item.GetCustomAttributes(typeMuchosAMuchos, true).FirstOrDefault() == null)
                                objeto = IncluirColecciones<TEntidad>(objeto, item, nombre);
                        }
                        catch (Exception exc)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!this.antirecursivo.Any<Type>(p => p.Name == item.Name))
                    {
                        if (item.GetCustomAttributes(typeUnoAUno, true).FirstOrDefault() != null ||
                            item.PropertyType.GetProperty("Id") != null)
                        {
                            //no entra nunca aca
                            objeto = objeto.Include(nombre + item.Name);
                        }
                    }
                }
            }

            return objeto;
        }

        private void ActualizarForeanKeys(object Entidad)
        {
            // Tipo de la entidad
            var typeEntidad = Entidad.GetType();
            // Toma todas las properties
            var props = typeEntidad.GetProperties();
            // Recorre todas la propiedades
            foreach (var prop in props)
            {
                // si no es collecion
                if (prop.PropertyType.GetProperty("Count") == null)
                {
                    // saber si tiene foreingkey o no
                    var atributo = (ForeignKeyAttribute)prop.GetCustomAttributes(typeof(ForeignKeyAttribute), false).FirstOrDefault();
                    //var atributo = prop.GetCustomAttributesData().Where(p=>p.AttributeType.Equals(typeof(ForeignKeyAttribute))).FirstOrDefault();
                    //esto es suficiente para saber si tiene el atributo de foreign key o no?

                    // leo el valor de la propiedad
                    var objetoProp = prop.GetValue(Entidad);
                    // saber si tiene el atributo muchosamuchos
                    var muchosaMuchos = (MuchosAMuchos)prop.GetCustomAttributes(typeof(MuchosAMuchos), false).FirstOrDefault();
                    var unoAMuchos = (UnoAMuchos)prop.GetCustomAttributes(typeof(UnoAMuchos), false).FirstOrDefault();
                    var unoAuno = (UnoAUno)prop.GetCustomAttributes(typeof(UnoAUno), false).FirstOrDefault();
                    //si el valor de la propiedad es "entidad base" y no es muchos a muchos
                    if (objetoProp is EntidadBase && unoAuno != null)
                        ActualizarForeanKeys(objetoProp);

                    if (atributo != null)
                    {
                        string nombrePropiedadForegnKey = atributo.Name;
                        var propiedad = typeEntidad.GetProperty(nombrePropiedadForegnKey);
                        var propiedadObject = propiedad.GetValue(Entidad);
                        if (propiedadObject != null)
                        {
                            var Id = propiedad.PropertyType.GetProperty("Id").GetValue(propiedadObject);
                            if (Id != null && Id.ToString() != "0")
                            {
                                prop.SetValue(Entidad, Id);
                                propiedad.SetValue(Entidad, null);
                            }
                            else
                            {
                                propiedad.SetValue(Entidad, null);
                                prop.SetValue(Entidad, null);
                            }
                        }
                        else
                        {
                            propiedad.SetValue(Entidad, null);
                            prop.SetValue(Entidad, null);
                        }
                    }
                }
                else
                {
                    if (prop.GetCustomAttribute<MuchosAMuchos>() != null || prop.GetCustomAttribute<UnoAMuchos>() != null)
                    {
                        dynamic coleccion = prop.GetValue(Entidad);
                        foreach (var item in coleccion)
                        {
                            if (item != null)
                                ActualizarForeanKeys(item);
                        }
                    }
                }
            }
        }

        public void insertaAuditoria<TEntidad>(TEntidad objeto, Accion accion, Usuario Usuario) where TEntidad : EntidadBase
        {
            //sucursal y empresa TODO.

            var entries = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
            foreach (var ent in entries)
            {
                var auditoria = this.auditor.CrearRegistro<TEntidad>(objeto, "", "", "", Usuario, ent);
                var set = base.Set(typeof(Auditoria));
                set.AsNoTracking();
                set.Add(auditoria);
            }
        }

        public void Insertar<TEntidad>(TEntidad objeto, Usuario Usuario) where TEntidad : EntidadBase
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.ActualizarForeanKeys(objeto);
            //domicilio es una foreign key que tiene que ser actualizada
            this.AdjuntarColecciones(objeto);
            var set = base.Set(typeof(TEntidad));
            set.Add(objeto);
            if (Usuario != null && Usuario.Id != 0)
                this.insertaAuditoria<TEntidad>(objeto, Accion.Agrega, Usuario);
        }



        private void AdjuntarColecciones(object objeto)
        {
            var propiedades = objeto.GetType().GetProperties();

            foreach (var propiedad in propiedades)
            {
                if (propiedad.PropertyType.GetProperty("Count") != null)
                {
                    dynamic coleccion = propiedad.GetValue(objeto);
                    if (coleccion != null)
                    {
                        foreach (var item in coleccion)
                        {
                            if (item.Id != 0)
                                this.Adjuntar(item);
                        }
                    }
                }
            }
        }

        public void Actualizar<TEntidad>(TEntidad objeto, Usuario Usuario) where TEntidad : EntidadBase
        {
            this.ActualizarForeanKeys(objeto);
            this.ActualizarEntidad<TEntidad>(objeto);
            if (Usuario != null && Usuario.Id != 0)
                this.insertaAuditoria<TEntidad>(objeto, Accion.Modifica, Usuario);
        }

        HashSet<object> itemsModificados;
        HashSet<object> itemsEliminados;

        private void ActualizarEntidad<TEntidad>(object objeto) where TEntidad : EntidadBase
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.itemsEliminados = new HashSet<object>();
            this.itemsModificados = new HashSet<object>();
            var objetoType = objeto.GetType();
            var id = (int)objetoType.GetProperty("Id").GetValue(objeto);
            var objetoOriginal = this.BuscarPorId<TEntidad>(id, CargarRelaciones.CargarTodo);
            this.Adjuntar(objetoOriginal);
            this.ComparaCampos(objetoOriginal, objeto, null);

            foreach (var item in itemsEliminados)
            {
                base.Entry(item).State = EntityState.Deleted;
            }

            this.Entry(objetoOriginal).CurrentValues.SetValues(objeto);
        }

        Type typeUnoAMuchos = typeof(UnoAMuchos);
        Type typeMuchosAMuchos = typeof(MuchosAMuchos);
        Type typeUnoAUno = typeof(UnoAUno);
        Type typeNoIncluirColecciones = typeof(NoIncluirColecciones);

        private bool ComparaCampos(object original, object modificado, PropertyInfo[] propiedades)
        {
            if (propiedades == null)
                propiedades = original.GetType().GetProperties();

            bool hayCambios = false;
            foreach (var propiedad in propiedades)
            {
                dynamic valorModificado = propiedad.GetValue(modificado);

                dynamic valorOriginal = propiedad.GetValue(original);

                if (propiedad.PropertyType.GetProperty("Count") != null)
                {
                    if (propiedad.GetCustomAttributes().Where(a => a is UnoAMuchos).Any())
                    {
                        var collecionMod = (IEnumerable<EntidadBase>)valorModificado;

                        Type typeItem = null;

                        dynamic propertisItem = null;
                        foreach (EntidadBase item in valorOriginal)
                        {
                            if (typeItem == null)
                            {
                                typeItem = item.GetType();
                                propertisItem = typeItem.GetProperties();
                            }

                            var buscado = collecionMod.FirstOrDefault(p => p.Id == item.Id);

                            if (buscado == null)
                            {
                                this.itemsEliminados.Add(item);
                                if (!hayCambios)
                                    hayCambios = true;
                            }
                            else
                            {
                                //aca deberia entrar en telefonos de contacto
                                var hay = false;

                                if (item is EntidadBase)
                                    hay = this.ComparaCampos(item, buscado, propertisItem);

                                if (hay)
                                {
                                    this.itemsModificados.Add(valorModificado);
                                    this.Entry(item).CurrentValues.SetValues(buscado);
                                }
                                if (!hayCambios)
                                {
                                    hayCambios = hay;
                                }

                            }

                        }

                        foreach (dynamic item in collecionMod)
                        {
                            if (item.Id == 0)
                            {
                                valorOriginal.Add(item);
                            }
                        }
                    }
                    else//es muchos a muchos no editable
                    {
                        var collecionMod = (IEnumerable<EntidadBase>)valorModificado;

                        List<dynamic> eliminar = new List<dynamic>();
                        foreach (EntidadBase item in valorOriginal)
                        {
                            var buscado = collecionMod.FirstOrDefault(p => p.Id == item.Id);

                            if (buscado == null)
                            {
                                eliminar.Add(item);
                                if (!hayCambios)
                                    hayCambios = true;
                            }

                        }

                        eliminar.ForEach(e => valorOriginal.Remove(e));

                        var collecionorig = (IEnumerable<EntidadBase>)valorOriginal;

                        foreach (dynamic item in valorModificado)
                        {

                            var buscado = collecionorig.FirstOrDefault(p => p.Id == item.Id);
                            if (buscado == null)
                            {
                                if (this.EstaDetached(item))
                                    this.Set(item.GetType()).Attach(item);
                                valorOriginal.Add(item);
                            }
                        }
                    }
                }
                else //no es colleccion
                {
                    if ((propiedad.GetCustomAttributes(typeUnoAUno, true).FirstOrDefault() != null))
                    {
                        //esto es por si no anda domicilio
                        if (valorOriginal != null)
                        {
                            this.Set(valorOriginal.GetType()).Attach(valorOriginal);
                            this.Entry(valorOriginal).CurrentValues.SetValues(valorModificado);
                            this.itemsModificados.Add(valorModificado);
                        }
                    }

                    if (!hayCambios)
                    {
                        if (valorModificado != valorOriginal)
                        {
                            hayCambios = true;
                        }
                    }
                }

            }

            return hayCambios;
        }

        private Stack<object> pilaBorrado = new Stack<object>();

        public void Borrar<TEntidad>(TEntidad objeto, Usuario Usuario) where TEntidad : EntidadBase
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.MarcarComoBorrado(objeto);
            this.insertaAuditoria<TEntidad>(objeto, Accion.Elimina, Usuario);
        }

        public void Vaciar<TEntidad>() where TEntidad : EntidadBase
        {
            base.Set<TEntidad>().ToList().ForEach(t => base.Set<TEntidad>().Remove(t));
            base.SaveChanges();
        }

        public void Adjuntar(object objeto)
        {
            if (base.Entry(objeto).State == EntityState.Detached)
            {
                base.Set(objeto.GetType()).Attach(objeto);
            }
        }


        List<EntidadBase> Eliminados = new List<EntidadBase>();
        public void MarcarRelacionesComoNoModificado(object objeto)
        {
            var propiedad = objeto.GetType().GetProperties();
            foreach (var prop in propiedad)
            {
                if (prop.PropertyType.GetProperty("Id") != null)
                {
                    var objetoRelacion = Metadatos.MetaDatos.ObtenerValor<object>(objeto, prop.Name);
                    if (objetoRelacion != null)
                        this.MarcarComoNoModificado(objetoRelacion);
                }
            }
        }

        public TEntidad BuscarPorId<TEntidad>(int Id, CargarRelaciones cargarEntidadesRelacionadas) where TEntidad : EntidadBase
        {
            //var q = this.Set<TEntidad>().AsNoTracking<TEntidad>().Where(e => e.Id == Id);
            var q = this.Set<TEntidad>().Where(e => e.Id == Id);
            if (cargarEntidadesRelacionadas == CargarRelaciones.NoCargarNada)
            {
                return q.FirstOrDefault();
            }
            else
            {
                return this.IncluirRelaciones<TEntidad>(q, cargarEntidadesRelacionadas).FirstOrDefault();
            }
        }

        public TEntidad BuscarPorCodigo<TEntidad>(string codigo, CargarRelaciones cargarEntidadesRelacionadas)
            where TEntidad : EntidadMaestro
        {
            //var q = this.Set<TEntidad>().AsNoTracking<TEntidad>().Where(p => p.Codigo == codigo);
            var q = this.Set<TEntidad>().Where(p => p.Codigo == codigo);
            if (cargarEntidadesRelacionadas == CargarRelaciones.NoCargarNada)
            {
                return q.FirstOrDefault();
            }
            else
            {

                return this.IncluirRelaciones<TEntidad>(q, cargarEntidadesRelacionadas).FirstOrDefault();
            }
        }

        public IQueryable<TEntidad> Consultar<TEntidad>(CargarRelaciones cargarEntidadesRelacionadas) where TEntidad : EntidadBase
        {
            //var q = this.Set<TEntidad>().AsNoTracking<TEntidad>();
            var q = this.Set<TEntidad>();
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 600;

            if (cargarEntidadesRelacionadas == CargarRelaciones.NoCargarNada)
            {
                return q;
            }
            else
            {
                IQueryable<TEntidad> query = this.IncluirRelaciones<TEntidad>(q, cargarEntidadesRelacionadas);
                return query;
            }

        }

        public TEntidad Crear<TEntidad>() where TEntidad : EntidadBase
        {
            var entidad = base.Set<TEntidad>().Create();
            return entidad;
        }
    }
}
