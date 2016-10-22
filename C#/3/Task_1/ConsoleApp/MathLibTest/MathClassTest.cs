using MathLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MathLibTest
{
	[TestClass]
	public class MathClassTest
	{
		[TestMethod]
		public void NOD_36and8()
		{
			int number1 = 36;
			int number2 = 8;
			int excepted = 4;

			int actual = MathClass.NOD(number1, number2);
			Assert.AreEqual(excepted, actual);
		}

		[TestMethod]
		public void NOD_8and0()
		{
			int number1 = 8;
			int number2 = 0;
			int excepted = 0;

			int actual = MathClass.NOD(number1, number2);
			Assert.AreEqual(excepted, actual);
		}
	}
}
