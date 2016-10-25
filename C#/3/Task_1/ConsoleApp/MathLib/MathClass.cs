using System;

namespace MathLib
{
	/// <summary>
	/// Math class
	/// </summary>
	public class MathClass
	{
			/// <summary>
			/// Find NOD of 2 numbers.
			/// </summary>
			/// <param name="number1">The number1.</param>
			/// <param name="number2">The number2.</param>
			/// <returns></returns>
			static public int NOD(int number1, int number2)
			{
				try 
				{
					//Search NOD if numbers positiv.Else throw exception.
					if (number1 > 0 || number2 > 0)
					{
						if (number2 == 0)
							return number1;
						else
							return NOD(number2, number1 % number2);
					}
					else
						throw new Exception();
				}
				catch(Exception) when (number1 < 0 || number2 < 0)
				{
						Console.WriteLine("Can't find NOD of negative number");
						return -1;
				}
			}

			//static public int NOD(int number1, int number2, int number3)
			//{
			//	return 0;
			//}

			//static public int NOD(int number1, int number2, int number3, int number4)
			//{
			//	return 0;
			//}

			//static public int NOD(int number1, int number2, int number3, int number4, int number5)
			//{
			//	return 0;
			//}

	}
}
