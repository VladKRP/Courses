using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converting;

namespace Converting.Test
{
	[TestClass]
	public class ConvertingTest
	{
		[TestMethod]
		public void ToBinTest1()
		{
			int hexNumber = 10;
			string expectedResult = "1010";
			string actualResult = ConvertingClass.ToBin(hexNumber);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		public void ToBinNegativeNumTest()
		{
			int hexNumber = -1;
			string expectedResult = null;
			string actualResult = ConvertingClass.ToBin(hexNumber);

			Assert.AreEqual(expectedResult, actualResult);
		}

	}
}
