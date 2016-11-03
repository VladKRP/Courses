using System;
using Converting;

namespace ConsoleApp
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine(ConvertingClass.ToBin(77));
			Console.WriteLine(ConvertingClass.ToBin(-1));
			Console.ReadLine();
		}
	}
}
