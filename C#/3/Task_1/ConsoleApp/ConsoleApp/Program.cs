using System;
using MathLib;

namespace ConsoleApp
{
	class Program
	{
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			
			int number1 = 436;
			int number2 = 120;
			try
			{
				
				Console.WriteLine(MathClass.NOD(number1, number2));
				number1 = 100;
				number2 = -10;
				Console.WriteLine(MathClass.NOD(number1, number2));
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
				Console.ReadLine();
		}
	}
}
