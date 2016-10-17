using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converting;

namespace Converting.Test
{
	[TestClass]
	public class ConvertingTest
	{
		[TestMethod]
		public void toBinTest_10_equal_1010()
		{
			int hexNumber = 10;
			string expectedResult = "1010";
			string actualResult = ConvertingClass.toBin(hexNumber);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		public void toBinTest_negativeNumber_equal_exception()
		{
			int hexNumber = -1;
			string expectedResult = null;
			string actualResult = ConvertingClass.toBin(hexNumber);

			Assert.AreEqual(expectedResult, actualResult);
		}

	}
}
