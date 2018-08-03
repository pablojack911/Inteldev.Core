using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inteldev.Core.Estructuras;

namespace Inteldev.Core.Tests
{
	/// <summary>
	/// Test Inteldev.Core.Estructuras.SwitchCase
	/// </summary>
	[TestClass]
	public class SwitchCaseStructureTest
	{
		private SwitchCase<int> swiCase;
		private int result;

		[TestInitialize]
		public void TestInitialize()
		{
			swiCase = new SwitchCase<int>();
			swiCase.AgregarCaso(1, (p => result = 1));
			swiCase.AgregarCaso(3, (p => result = 3));
			swiCase.AgregarCaso(8, (p => result = 8));
			swiCase.AgregarCaso(10, (p => result = 10));
			swiCase.AgregarCaso(12, (p => result = 12));
			swiCase.AgregarCaso(39, (p => result = 39));
		}

		[TestMethod]
		public void CaseFoundTest( )
		{
			swiCase.Valor=3;
			swiCase.Ejecutar();
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void CaseNotFoundTest( )
		{
			swiCase.Valor = 9;
			swiCase.Ejecutar();
		}

		[TestMethod]
		public void DefaultCase( )
		{
			swiCase.CasoPorDefecto = (p=> result = 77);
			swiCase.Valor = 13;
			swiCase.Ejecutar();
			Assert.AreEqual(77, result);
		}
	}
}
