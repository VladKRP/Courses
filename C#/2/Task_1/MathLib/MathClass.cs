using System;

namespace MathLib
{
	public class MathClass
    {
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
