using System;
using Converting;

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
			Console.WriteLine(ConvertingClass.toBin(77));
			Console.WriteLine(ConvertingClass.toBin(-1));
			Console.ReadLine();
		}
	}
}
