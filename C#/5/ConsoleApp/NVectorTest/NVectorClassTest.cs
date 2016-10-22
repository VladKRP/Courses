using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VectorLib;

namespace NVectorTest
{
	[TestClass]
	public class NVectorClassTest
	{
		[TestMethod]
		public void VectorSum()
		{
			NVector vector1 = new NVector(5, 10, 5);
			NVector vector2 = new NVector(7, 1, 1);
			NVector expected = new NVector(12, 11, 6);
			NVector actual = vector1 + vector2;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VectorDifference()
		{
			NVector vector1 = new NVector(5, 10, 5);
			NVector vector2 = new NVector(7, 1, 1);
			NVector expected = new NVector(-2, 9, 4);
			NVector actual = vector1 - vector2;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VectorAndNumberMultiply()
		{
			NVector vector = new NVector(5, 10, 5);
			double number = 5.0;
			NVector expected = new NVector(25, 50, 25);
			NVector actual = vector * number;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VectorAndVectorMultiply()
		{
			NVector vector = new NVector(5, 10, 5);
			double number = 5.0;
			NVector expected = new NVector(25, 50, 25);
			NVector actual = vector * number;

			Assert.AreEqual(expected, actual);
		}

	}
}
