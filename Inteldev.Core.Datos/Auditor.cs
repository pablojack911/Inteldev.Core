using Inteldev.Core.Modelo;
using Inteldev.Core.Modelo.Auditoria;
using Inteldev.Core.Modelo.Locacion;
using Inteldev.Core.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Inteldev.Core.Datos
{
	public class Auditor
	{

		public Auditoria CrearRegistro<TEntidad>(TEntidad entidad,
			string sucursal, string empresa, string unidadDeNegocio, Usuario Usuario, DbEntityEntry dbEntry) 
			where TEntidad : EntidadBase
		{
			List<DetalleAuditoria> result = new List<DetalleAuditoria>();

			TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
			string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
			//string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;
			var auditoria = new Auditoria();
			auditoria.NombrePC = Environment.MachineName;
		 	auditoria.UsuarioWindows = Environment.UserName;
			auditoria.Windows = Environment.OSVersion.ToString();
			auditoria.IP = this.LocalIPAddress();
			auditoria.Sucursal = sucursal;
			auditoria.Empresa = empresa;
			auditoria.UnidadDeNegocio = unidadDeNegocio;
			auditoria.Entidad = entidad.ToString();
			//faltara mas info sobre el usuario?
			auditoria.UsuarioSistema = Usuario.Nombre;
			auditoria.UnidadDeNegocio = Usuario.UnidadDeNegocioActual.ToString();
			if (Usuario.EmpresaActual != null && Usuario.SucursalActual != null)
				{
					auditoria.Empresa = Usuario.EmpresaActual.ToString();
					auditoria.Sucursal = Usuario.SucursalActual.ToString();
				}

			if (dbEntry.State == System.Data.Entity.EntityState.Added)
			{	
				var detalle = new DetalleAuditoria();
				detalle.Accion = Accion.Agrega;
				detalle.NombreTabla = tableName;
				detalle.NombreColumna = "*ALL";
				var json = new JavaScriptSerializer().Serialize(dbEntry.CurrentValues.ToObject());
				detalle.ValorNuevo = json.ToString();
				// For Inserts, just add the whole record
				// If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
				result.Add(detalle);
			}
			else if (dbEntry.State == System.Data.Entity.EntityState.Deleted)
			{
				// Same with deletes, do the whole record, and use either the description from Describe() or ToString()
				var detalle = new DetalleAuditoria();
				detalle.NombreColumna = "*ALL";
				detalle.NombreTabla = tableName;
				detalle.Accion = Accion.Elimina;
				var json = new JavaScriptSerializer().Serialize(dbEntry.OriginalValues.ToObject());
				detalle.ValorNuevo =json.ToString();
				result.Add(detalle);
			}
			else if (dbEntry.State == System.Data.Entity.EntityState.Modified)
			{
				foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
				{
					// For updates, we only want to capture the columns that actually changed
					if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
					{
						var detalle = new DetalleAuditoria();
						detalle.Accion = Accion.Modifica;
						detalle.NombreTabla = tableName;
						detalle.NombreColumna = propertyName;
						detalle.ValorAnterior = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
						detalle.ValorNuevo = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
						result.Add(detalle);
					}
				}
			}
			auditoria.Detalle = result;
			return auditoria;
		}

		private string LocalIPAddress( )
		{
			IPHostEntry host;
			string localIP = "";
			host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					localIP = ip.ToString();
					break;
				}
			}
			return localIP;
		}
	}
}
