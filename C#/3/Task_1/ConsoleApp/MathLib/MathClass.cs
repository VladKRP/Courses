
namespace MathLib
{
	/// <summary>
	/// Math class
	/// </summary>
	public class MathClass
	{
			/// <summary>
			/// Nods the specified number1.
			/// </summary>
			/// <param name="number1">The number1.</param>
			/// <param name="number2">The number2.</param>
			/// <returns></returns>
			static public int NOD(int number1, int number2)
			{
					if (number2 == 0)
						return number1;
					else
						return NOD(number2, number1 % number2);
			}

			static public int NOD(int number1, int number2, int number3)
			{
				return 0;
			}

			static public int NOD(int number1, int number2, int number3, int number4)
			{
				return 0;
			}

			static public int NOD(int number1, int number2, int number3, int number4, int number5)
			{
				return 0;
			}

	}
}
