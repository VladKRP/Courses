using System;

namespace MathLib
{
    public class MathClass
    {
			/// <summary>
			/// Newton method to find square root.
			/// </summary>
			/// <param name="number">The number.</param>
			/// <param name="power">The power.</param>
			/// <param name="eps">The eps.</param>
			/// <returns></returns>
			static public double sqrt(double number,int power,double eps = 0.001) 
				{
						double x = number / power;
						double xNext = (1 / power) * ((power - 1) * x + number / Math.Pow(x, power - 1));	

						while (Math.Abs(xNext - x) > eps)
						{
								x = xNext;
								xNext = (1 / power) * ((power - 1) * x + (number / Math.Pow(x, power - 1)));
						}
						return xNext;
				}
    }
}
