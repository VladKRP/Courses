using System;
using VectorLib;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			NVector vect1 = new NVector(5, 10, 3);
			NVector vect2 = new NVector(3, 6, 11);
			NVector vect3 = vect1 + vect2;
			vect1.displayVector();
			vect2.displayVector();
			vect3.displayVector();

			Console.ReadLine();
		}
	}
}
