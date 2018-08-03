using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inteldev.Core.Estructuras;

namespace Inteldev.Core.Tests
{
	/// <summary>
	/// Test Inteldev.Core.Estructuras.IfElse
	/// </summary>
	[TestClass]
	public class IfElseStructureTest
	{
		private IfElse ifelse;

		[TestInitialize]
		public void TestInitialize( )
		{
			this.ifelse = new IfElse();
		}

		[TestMethod]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void NullConditionTest( )
		{
			ifelse.Condicion = null;
			ifelse.Ejecutar();
		}

		[TestMethod]
		public void ThenExecuteTest( )
		{
			int v1 = 1;
			int v2 = 1;
			int result=0;
			ifelse.Condicion = (() => v1==v2);
			ifelse.Entonces = (p => result = 2);
			ifelse.Ejecutar();
			int expected = 2;
			Assert.AreEqual(expected,result);
		}

		[TestMethod]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void ThenNullTest( )
		{
			int v1 = 1;
			int v2 = 1;
			ifelse.Condicion = (( ) => v1 == v2);
			ifelse.Entonces = null;
			ifelse.Ejecutar();
		}

		[TestMethod]
		public void ElseTest( )
		{
			int v1 = 1;
			int v2 = 2;
			int result = 0;
			ifelse.Condicion = (( ) => v1 == v2);
			ifelse.Entonces = (p => result = 2);
			ifelse.Sino = (p => result = 5);
			ifelse.Ejecutar();
			int expected = 5;
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void ElseNulltest( )
		{
			int v1 = 1;
			int v2 = 2;
			int result;
			ifelse.Condicion = (( ) => v1 == v2);
			ifelse.Entonces = (p => result = 2);
			ifelse.Sino = null;
			ifelse.Ejecutar();
		}
	}
}
