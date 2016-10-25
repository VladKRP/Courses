using System;

namespace ShapesLib
{
	/// <summary>
	/// Abstract class Shape
	/// </summary>
	public abstract class Shape
    {
		/// <summary>
		/// The perimetr
		/// </summary>
		protected double perimetr;
		/// <summary>
		/// The square
		/// </summary>
		protected double square;
		/// <summary>
		/// Check can shape be exist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is exist; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool isExist();
    }

	/// <summary>
	/// Class Triagle
	/// </summary>
	/// <seealso cref="ShapesLib.Shape" />
	public class Triangle:Shape 
		{

		/// <summary>
		/// The x1, y1 coordinate of shape
		/// </summary>
		public double x1, y1;
		/// <summary>
		/// The x2, y2 coordinate of shape
		/// </summary>
		public double x2, y2;
		/// <summary>
		/// The x3, y3 coordinate of shape
		/// </summary>
		public double x3, y3;


		/// <summary>
		/// The side a
		/// </summary>
		public double sideA;
		/// <summary>
		/// The side b
		/// </summary>
		public double sideB;
		/// <summary>
		/// The side c
		/// </summary>
		public double sideC;

		/// <summary>
		/// Initializes a new instance of the <see cref="Triangle"/> class.
		/// </summary>
		/// <param name="x1">The x1.</param>
		/// <param name="y1">The y1.</param>
		/// <param name="x2">The x2.</param>
		/// <param name="y2">The y2.</param>
		/// <param name="x3">The x3.</param>
		/// <param name="y3">The y3.</param>
		public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
				{
						this.x1 = x1;
						this.y1 = y1;
						this.x2 = x2;
						this.y2 = y2;
						this.x3 = x3;
						this.y3 = y3;
						searchSides();
				}

		/// <summary>
		/// Initializes a new instance of the <see cref="Triangle"/> class.
		/// </summary>
		/// <param name="sideA">The side a.</param>
		/// <param name="sideB">The side b.</param>
		/// <param name="sideC">The side c.</param>
		/// <exception cref="System.Exception">Triangle side's can't be negative.</exception>
		public Triangle(double sideA, double sideB, double sideC)
				{
						try {
								if (sideA > 0 && sideB > 0 && sideC > 0)
								{
									this.sideA = sideA;
									this.sideB = sideB;
									this.sideC = sideC;
								}
								else
								{
									throw new Exception("Triangle side's can't be negative.");
								}
									
						}
						catch(Exception exc)//Out error message when at least one side negative.
						{
								Console.WriteLine("Error:" + exc.Message);
						}
				}

		/// <summary>
		/// Searches the sides by coordinate.
		/// </summary>
		public void searchSides()
				{
						sideA = Math.Sqrt(Math.Pow(Math.Abs(x2) - Math.Abs(x1), 2) + Math.Pow(Math.Abs(y2) - Math.Abs(y1), 2));
						sideB = Math.Sqrt(Math.Pow(Math.Abs(x3) - Math.Abs(x2), 2) + Math.Pow(Math.Abs(y2) - Math.Abs(y1), 2));
						sideC = Math.Abs(x3) - Math.Abs(x1);
				}

		/// <summary>
		/// Calculate triangle perimetr
		/// </summary>
		/// <returns>Triangle perimetr</returns>
		public double Perimetr()
				{
						perimetr = sideA + sideB + sideC;
						return perimetr;
				}

		/// <summary>
		/// Calculate triangle square
		/// </summary>
		/// <returns>Triangle square</returns>
		public double Square()
				{
						double semiperimetr = Perimetr() / 2;
						square = (Math.Sqrt(semiperimetr*(semiperimetr-sideA)*(semiperimetr-sideB)*(semiperimetr-sideC)));
						return square;
				}

		/// <summary>
		/// Determines whether this instance is exist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is exist; otherwise, <c>false</c>.
		/// </returns>
		public override bool  isExist() 
				{
						bool shapeExist = false;
						if (((sideA > 0 && sideB > 0 && sideC > 0)) && ((sideB + sideC > sideA) && (sideA + sideC > sideB) && (sideA + sideB > sideC)))
								shapeExist = true;
						return shapeExist;
				}
		}

}
