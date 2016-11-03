using System;

namespace ShapesLib
{
	public abstract class Shape
    {
			protected double perimetr;
			protected double square;
			public abstract bool isExist();
		}


		public class Triangle:Shape 
			{

			public double x1, y1;
			public double x2, y2;
			public double x3, y3;

			public double sideA;
			public double sideB;
			public double sideC;

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

			public Triangle(double sideA, double sideB, double sideC)
					{
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

			public void searchSides()
					{
							sideA = Math.Sqrt(Math.Pow(Math.Abs(x2) - Math.Abs(x1), 2) + Math.Pow(Math.Abs(y2) - Math.Abs(y1), 2));
							sideB = Math.Sqrt(Math.Pow(Math.Abs(x3) - Math.Abs(x2), 2) + Math.Pow(Math.Abs(y2) - Math.Abs(y1), 2));
							sideC = Math.Abs(x3) - Math.Abs(x1);
					}

			public double Perimetr()
					{
							perimetr = sideA + sideB + sideC;
							return perimetr;
					}

			public double Square()
					{
							double semiperimetr = Perimetr() / 2;
							square = (Math.Sqrt(semiperimetr*(semiperimetr-sideA)*(semiperimetr-sideB)*(semiperimetr-sideC)));
							return square;
					}

			public override bool  isExist() 
					{
							bool shapeExist = false;
							if (((sideA > 0 && sideB > 0 && sideC > 0)) && ((sideB + sideC > sideA) && (sideA + sideC > sideB) && (sideA + sideB > sideC)))
									shapeExist = true;
							return shapeExist;
					}
			}

}
