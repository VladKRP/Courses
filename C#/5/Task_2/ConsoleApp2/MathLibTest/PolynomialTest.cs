using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;

namespace MathLibTest
{
	[TestClass]
	public class PolynomialTest
	{

		[TestMethod]
		public void EqualOperatorTest1()
		{
			Polynomial firstPolynomial = new Polynomial(5, 0.1, 7);
			Polynomial secondPolynomial = new Polynomial(5, 0.1, 7);

			bool expected = true;
			bool actual = (firstPolynomial == secondPolynomial);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void EqualOperatorTest2()
		{
			Polynomial firstPolynomial = new Polynomial(5, 0.1, 7);
			Polynomial secondPolynomial = new Polynomial(7, 0.1, 7);

			bool expected = false;
			bool actual = (firstPolynomial == secondPolynomial);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotEqualOperatorTest1()
		{
			Polynomial firstPolynomial = new Polynomial(5, 0.1, 7);
			Polynomial secondPolynomial = new Polynomial(5, 0.1, 7);

			bool expected = false;
			bool actual = (firstPolynomial != secondPolynomial);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotEqualOperatorTest2()
		{
			Polynomial firstPolynomial = new Polynomial(5, 0.1, 7);
			Polynomial secondPolynomial = new Polynomial(7, 0.1, 7);

			bool expected = true;
			bool actual = (firstPolynomial != secondPolynomial);

			Assert.AreEqual(expected, actual);
		}
	}
}
