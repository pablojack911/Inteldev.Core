using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Inteldev.Core.Modelo;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Objects;

namespace Inteldev.Core.Datos
{
	/// <summary>
	/// Contexto Base. Comentarios sobre metodos estan en <see cref="IDbContext"/>
	/// </summary>
    public abstract class DbContextBase : DbContext , IDbContext
    {
		/// <summary>
		/// Constructor. Usa base de datos por defecto
		/// </summary>
        public DbContextBase() : base() { }

		private string Estado;

		/// <summary>
		/// Constructor. Usa una base especifica
		/// </summary>
		/// <param name="nombre">Nombre de la base de datos</param>
		public DbContextBase(string nombre) : base(nombre) { this.Estado = ""; }        

        public new IDbSet<TEntidad> Set<TEntidad>() where TEntidad :EntidadBase
        {			
            return base.Set<TEntidad>();
        }                

        public new int SaveChanges()        
        {
            return base.SaveChanges();
        }

        public void MarcarComoBorrado(object objeto)
        {
			if (base.Entry(objeto).State!=System.Data.EntityState.Deleted)
				base.Entry(objeto).State = System.Data.EntityState.Deleted;
        }

        public void MarcarComoModificado(object objeto)
        {
			if (base.Entry(objeto).State != System.Data.EntityState.Modified)
				base.Entry(objeto).State = System.Data.EntityState.Modified;
        }

        public void MarcarComoNoModificado(object objeto)
        {
			if (base.Entry(objeto).State != System.Data.EntityState.Modified)
				base.Entry(objeto).State = System.Data.EntityState.Unchanged;
        }

		public void MarcarComoAgregado(object objeto)
		{
			if (base.Entry(objeto).State != System.Data.EntityState.Added)
				base.Entry(objeto).State = System.Data.EntityState.Added;
		}

        public bool EstaBorrado(object objeto)
        {			
			return base.Entry(objeto).State == System.Data.EntityState.Deleted;
        }

        public bool EstaModificado(object objeto)
        {
            return base.Entry(objeto).State == System.Data.EntityState.Modified;
        }

		//public void Dispose()
		//{
		//	throw new NotImplementedException();
		//}

        public IQueryable<TEntidad> IncluirRelaciones<TEntidad>(IQueryable<TEntidad> objeto) where TEntidad :EntidadBase
        {
            var tipoEntidad =typeof(TEntidad);            
            var propiedad = tipoEntidad.GetProperties();
            foreach (var prop in propiedad)
            {
                if (prop.PropertyType.GetProperty("Id") != null )
                {
                    objeto = objeto.Include(prop.Name);
                }
                else
                {
                    if (prop.PropertyType.GetProperty("Count") != null)
                    {
                        objeto = IncluirColecciones<TEntidad>(objeto, prop, "");
                    }                    
                }
            }
            return objeto;
        }


		
        private IQueryable<TEntidad> IncluirColecciones<TEntidad>(IQueryable<TEntidad> objeto, PropertyInfo prop, string nombre ) where TEntidad : EntidadBase
        {

            objeto = objeto.Include(nombre+prop.Name);
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
                    if (typeof(TEntidad).Name != tipogen.Name)
                    {
                        try
                        {
                            objeto = IncluirColecciones<TEntidad>(objeto, item, nombre);
                        }
                        catch (Exception exc)
                        {
                            break;
                        }
                    }
                }
            }

            return objeto;
        }

		private void ActualizarForeanKeys(object Entidad)
		{
			var typeEntidad = Entidad.GetType();
			var props = typeEntidad.GetProperties();
			foreach (var prop in props)
			{
				var atributo = (ForeignKeyAttribute)prop.GetCustomAttributes(typeof(ForeignKeyAttribute), false).FirstOrDefault();
				//var atributo = prop.GetCustomAttributesData().Where(p=>p.AttributeType.Equals(typeof(ForeignKeyAttribute))).FirstOrDefault();
				//esto es suficiente para saber si tiene el atributo de foreign key o no?
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
		}


        public void Insertar(object objeto)
        {
			this.ActualizarForeanKeys(objeto);
			this.AdjuntarCollecciones(objeto);
			this.AdjuntarRelaciones(objeto);
            base.Set(objeto.GetType()).Add(objeto);
        }
        
        public void Actualizar(object objeto)
        {
			this.ActualizarForeanKeys(objeto);

			this.Adjuntar(objeto);
			//fijarse lo de las relaciones con entidades.
			//this.AdjuntarRelaciones(objeto);
			this.AdjuntarCollecciones(objeto);
			this.MarcarComoModificado(objeto);
        }

        public void Borrar(object objeto)
        {
			this.Estado = "Eliminado";
			objeto.GetType().GetProperty("Estado").SetValue(objeto,EstadoEntidad.Eliminado);
            //this.Adjuntar(objeto);
			AdjuntarCollecciones(objeto);
            this.MarcarComoBorrado(objeto);
        }

        public void Vaciar<TEntidad>() where TEntidad : EntidadBase
        {
            base.Set<TEntidad>().ToList().ForEach(t => base.Set<TEntidad>().Remove(t));
            base.SaveChanges();
        }

        public void Adjuntar(object objeto)
        {
			if (base.Entry(objeto).State == System.Data.EntityState.Detached)
			{
				//var objetoType = objeto.GetType();
				//var set = base.Set(objetoType);
				//var attachedEntity = set.Find((int)objetoType.GetProperty("Id").GetValue(objeto));
				//if (attachedEntity != null)
				//{
				//	base.Entry(attachedEntity).CurrentValues.SetValues(objeto);
				//}
				//else
				//{
				//	base.Set(objetoType).Attach(objeto);
				//	this.MarcarComoModificado(objeto);
				//}
				base.Set(objeto.GetType()).Attach(objeto);
			}
        }

		private void AdjuntarCollecciones(object objeto)
		{
			var propiedad = objeto.GetType().GetProperties();
			foreach (var prop in propiedad)
			{
				if (prop.PropertyType.GetProperty("Count") != null)
				{
					dynamic objetoRelacion = Metadatos.MetaDatos.ObtenerValor<Object>(objeto, prop.Name);
					if (objetoRelacion != null)
					{

						var lista = new ArrayList(objetoRelacion);
						
						foreach(var item in lista)
						{
							this.CambiarState((EntidadBase)item);
							this.AdjuntarCollecciones(item);
							this.AdjuntarRelaciones(item);
						}

					}
				}
			}
		}

		private void CambiarState(EntidadBase item)
		{			
			switch (item.Estado)
			{
				case EstadoEntidad.NoModificado:
					this.MarcarComoNoModificado(item);
					break;
				case EstadoEntidad.Modificado:
					this.Adjuntar(item);
					this.MarcarComoModificado(item);
					break;
				case EstadoEntidad.Eliminado:
					this.Adjuntar(item);
					this.MarcarComoBorrado(item);
					break;
				case EstadoEntidad.Nuevo:
					//this.Adjuntar(item);
					//this.MarcarComoAgregado(item);
					break;
			}			
		}

		private void AdjuntarRelaciones(object objeto)
        {
            var propiedad = objeto.GetType().GetProperties();
            foreach (var prop in propiedad)
            {
				if (prop.PropertyType.GetProperty("Id") != null)
				{
					var objetoRelacion = Metadatos.MetaDatos.ObtenerValor<object>(objeto, prop.Name);
					if (objetoRelacion != null)
					{
						this.ActualizarForeanKeys(objetoRelacion);
						this.CambiarState((EntidadBase)objetoRelacion);
					}
				}
            }                 
        }
        
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

        public TEntidad BuscarPorId<TEntidad>(int Id, bool cargarEntidadesRelacionadas) where TEntidad : EntidadBase
        {            
            var q = this.Set<TEntidad>().Where(e => e.Id == Id);

            if (cargarEntidadesRelacionadas)
                return this.IncluirRelaciones<TEntidad>(q).FirstOrDefault();
            else
                return q.FirstOrDefault();
        }

        public IQueryable<TEntidad> Consultar<TEntidad>(bool cargarEntidadesRelacionadas) where TEntidad : EntidadBase
        {
            if (cargarEntidadesRelacionadas)
                return this.IncluirRelaciones<TEntidad>(base.Set<TEntidad>());
            else
                return base.Set<TEntidad>();

        }

        public TEntidad Crear<TEntidad>() where TEntidad : EntidadBase
        {            
            var entidad = base.Set<TEntidad>().Create();            
            return entidad;
        }
    }
}
