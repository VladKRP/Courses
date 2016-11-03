using Microsoft.VisualStudio.TestTools.UnitTesting;
using VectorLib;

namespace NVectorTest
{
	[TestClass]
	public class NVectorClassTest
	{
		[TestMethod]
		public void VectorSumTest()
		{
			NVector vector1 = new NVector(5, 10, 5);
			NVector vector2 = new NVector(7, 1, 1);
			NVector expectedVector = new NVector(12, 11, 6);
			NVector actualVector = vector1 + vector2;

			Assert.AreEqual(expectedVector.X, actualVector.X);
			Assert.AreEqual(expectedVector.Y, actualVector.Y);
			Assert.AreEqual(expectedVector.Z, actualVector.Z);
		}

		[TestMethod]
		public void VectorDifferenceTest()
		{
			NVector vector1 = new NVector(5, 10, 5);
			NVector vector2 = new NVector(7, 1, 1);
			NVector expectedVector = new NVector(-2, 9, 4);
			NVector actualVector = vector1 - vector2;

			Assert.AreEqual(expectedVector.X, actualVector.X);
			Assert.AreEqual(expectedVector.Y, actualVector.Y);
			Assert.AreEqual(expectedVector.Z, actualVector.Z);
		}

		[TestMethod]
		public void VectorAndNumberMultiplyTest()
		{
			NVector vector = new NVector(5, 10, 5);
			double number = 5;
			NVector expectedVector = new NVector(25, 50, 25);
			NVector actualVector = vector * number;

			Assert.AreEqual(expectedVector.X, actualVector.X);
			Assert.AreEqual(expectedVector.Y, actualVector.Y);
			Assert.AreEqual(expectedVector.Z, actualVector.Z);
		}

		[TestMethod]
		public void VectorAndVectorMultiplyTest()
		{
			NVector vector1 = new NVector(2, 5, 1);
			NVector vector2 = new NVector(3, 1, 4);
			NVector expectedVector = new NVector(19, -5, -13);
			NVector actualVector = vector1 * vector2;

			Assert.AreEqual(expectedVector.X, actualVector.X);
			Assert.AreEqual(expectedVector.Y, actualVector.Y);
			Assert.AreEqual(expectedVector.Z, actualVector.Z);
		}

	}
}
