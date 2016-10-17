using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;

namespace MathLib.test
{
	[TestClass]
	public class MathClassTest
	{
		[TestMethod]
		public void sqrt_9_equal_3()
		{
			int number = 9;
			double expected = 3;
			double actual = MathClass.sqrt(number, 2);
			Assert.AreEqual(expected, actual);
		}
	}
}
