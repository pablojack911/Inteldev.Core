using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inteldev.Core.Extenciones;
using Inteldev.Core.Modelo;
using System.Collections.Generic;

namespace Inteldev.Core.Tests
{
	/// <summary>
	/// Object Extension test on Inteldev.Core.Estructuras
	/// </summary>
	[TestClass]
	public class ObjectExtensionTest
	{
		private Inteldev.Core.Modelo.Organizacion.Empresa organization;

		[TestInitialize]
		public void TestInitialize()
		{
			organization = new Modelo.Organizacion.Empresa();
			organization.CUIT = "233855669";
			organization.Nombre = "Tessto";
			organization.RazonSocial = "que se yo";
			organization.Id = 1;
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void NullObjectTest( )
		{
			organization = null;
			ObjectExtencion.EsEntidad(organization);
		}

		[TestMethod]
		public void CloningTest( )
		{
			Modelo.Organizacion.Empresa cloned;
			cloned = organization.Clonar<Modelo.Organizacion.Empresa>();
			Assert.ReferenceEquals(organization,cloned);
		}

		[TestMethod]
		public void EntityCheckTest( )
		{
			Assert.IsTrue(ObjectExtencion.EsEntidad(organization));
		}

		[TestMethod]
		public void CollectionCheckTest( )
		{
			List<int> collection = new List<int>();
			Assert.IsTrue(ObjectExtencion.EsColeccion(collection));
		}


	}
}
