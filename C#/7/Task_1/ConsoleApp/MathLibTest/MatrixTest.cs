using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;

namespace MathLibTest
{
	[TestClass]
	public class MatrixTest
	{
		[TestMethod]
		public void MatrixAddTest()
		{
			double[,] array1 = { { 1, 3 }, { 2, 1 } };
			double[,] array2 = { { 2, 0 }, { 1, 2 } };
			double[,] expected = { { 3, 3 }, { 3, 3 } };


			Matrix mx1 = new Matrix(array1);
			Matrix mx2 = new Matrix(array2);
			Matrix expectedMatrix = new Matrix(expected);
			Matrix actualMatrix = mx1 + mx2;

			Assert.AreEqual(expectedMatrix, actualMatrix);
		}

		[TestMethod]
		public void MatrixDeducatedTest()
		{

		}
	}
}
