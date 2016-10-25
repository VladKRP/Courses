using System;
using TextEditLib;

namespace ConsoleApp
{
	/// <summary>
	/// Class Program
	/// </summary>
	class Program
	{
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			//Out edited coordinate from fisrt file
			Console.WriteLine("Coordinate from first file:");
			TextEdit.OutCoordinate("Location.txt");
			//Out edited coordinate from second file
			Console.WriteLine("Coordinate from second file:");
			TextEdit.OutCoordinate("Location2.txt");
			//Trying to open not existing file
			TextEdit.OutCoordinate("Loc.txt");
			Console.ReadLine();
		}
	}
}
