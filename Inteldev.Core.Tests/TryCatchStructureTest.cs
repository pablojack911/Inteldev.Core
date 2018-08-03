using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inteldev.Core.Estructuras;

namespace Inteldev.Core.Tests
{
	/// <summary>
	/// Test Try/Catch on Inteldev.Core.Estructuras
	/// </summary>
	[TestClass]
	public class TryCatchStructureTest
	{
		private int result;

		private void tryMethod(object o)
		{
			throw new NullReferenceException();
		}

		private void finallyMethod(object o)
		{
			result = 4;
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void ExeptionCathTest( )
		{
			TryCatch.Intentar(p => tryMethod(p), true);
		}

		[TestMethod]
		public void TryFinallyTest( )
		{
			TryCatch.Intentar(p => tryMethod(p), o=>finallyMethod(o),false);
			Assert.AreEqual(4, result);
		}

	}
}
