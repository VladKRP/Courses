using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;

namespace MathLib.test
{
	[TestClass]
	public class MathClassTest
	{
		[TestMethod]
		public void SqrtTest1()
		{
			double number = 9;
			double expected = 3;
			double actual = MathClass.NRoot(number, 2);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SqrtTest2()
		{
			double number = 27;
			double expected = 3;
			double actual = MathClass.NRoot(number, 3);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void isSqrtResultsEqualTest()
		{
			double number = 64;
			bool expected = true;
			bool actual = MathClass.IsNRootResultsEqual(number, 2);
			Assert.AreEqual(expected, actual);
		}
	}
}
