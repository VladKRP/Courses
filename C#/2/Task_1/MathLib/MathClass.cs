using System;

namespace MathLib
{
	/// <summary>
	/// MathClass implementation
	/// </summary>
	public class MathClass
    {
			/// <summary>
			/// Newton method to find n-root.
			/// </summary>
			/// <param name="number">The number.</param>
			/// <param name="power">The power.</param>
			/// <param name="eps">The eps.</param>
			/// <returns></returns>
			static public double NRoot(double number, int power = 2, double eps = 0.00000001) 
			{
				try {
				if (number > 0)
				{
					double x = 0.0;
					double xNext = number;

					do
					{
						x = xNext;
						xNext = xNext - ((Math.Pow(xNext, power) - number) / (power * Math.Pow(xNext, power - 1)));
					} while (Math.Abs(xNext - x) > eps);

					return xNext;
				}
				else
					throw new Exception("Can't find n-root from negative or null number");
					
				}
				catch(Exception exc)
				{
						Console.WriteLine("Error:" + exc.Message);
						return -1;
				}
						
			}

		/// <summary>
		/// Compare own method NRoot and existing method Pow.
		/// </summary>
		/// <param name="number">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>Return true when 2 methods have same results.Return false if methods result not equal.</returns>
		static public bool IsNRootResultsEqual(double number,int power = 2)
			{
					double ownNRootMethodResult = NRoot(number,power);
					double existNRootMethodResult = Math.Pow(number, 1.0/power);
					if (ownNRootMethodResult == existNRootMethodResult)
						return true;
					else
						return false;
			}
		}
}
