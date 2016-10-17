using System;
using TextEditLib;

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
			Console.WriteLine("Coordinate from first file:");
			TextEdit.outCoordinate("Location.txt");
			Console.WriteLine("Coordinate from second file:");
			TextEdit.outCoordinate("Location2.txt");
			Console.ReadLine();
		}
	}
}
