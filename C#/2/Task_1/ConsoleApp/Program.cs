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
			Console.WriteLine(MathClass.NRoot(27, 3));
			Console.ReadLine();
		}
	}
}
