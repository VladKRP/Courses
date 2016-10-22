using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLib;

namespace ShapesLibTest
{
	[TestClass]
	public class TriangleTest
	{


		[TestMethod]
		public void PerimetrTest()
		{
				Triangle testTriangle = new Triangle(5, 10, 7);
				double expectedPerimetr = 22.0;
				double actualPerimetr = testTriangle.Perimetr();

				Assert.AreEqual(expectedPerimetr, actualPerimetr);
		}

		[TestMethod]
		public void SquareTest()
		{
				Triangle testTriangle = new Triangle(5, 10, 7);
				double expectedSquare = Math.Sqrt(264.0);
				double actualSquare = testTriangle.Square();

				Assert.AreEqual(expectedSquare, actualSquare);
		}

		[TestMethod]
		public void isExistTest()
		{
			Triangle testTriangle = new Triangle(1, 10, 7);


			bool expectedValue = false;
			bool actualValue = testTriangle.isExist();
			Assert.AreEqual(expectedValue, actualValue);
		}

	}
}
