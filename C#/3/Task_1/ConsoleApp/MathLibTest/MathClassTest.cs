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

	}
}
