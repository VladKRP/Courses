using MathLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MathLibTest
{
	[TestClass]
	public class MathClassTest
	{
		[TestMethod]
		public void NODTest()
		{
			int number1 = 36;
			int number2 = 8;
			int excepted = 4;

			int actual = MathClass.NOD(number1, number2);
			Assert.AreEqual(excepted, actual);
		}

		[TestMethod]
		public void NODWithNegativeTest()
		{
			int number1 = 24;
			int number2 = -4;
			int excepted = -1;

			int actual = MathClass.NOD(number1, number2);
			Assert.AreEqual(excepted, actual);
		}
	}
}
