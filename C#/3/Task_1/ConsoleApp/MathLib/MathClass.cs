using System;

namespace MathLib
{
	public class MathClass
	{

			static public int NOD(int number1, int number2)
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
						throw new Exception("Can't find NOD of negative number");
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
