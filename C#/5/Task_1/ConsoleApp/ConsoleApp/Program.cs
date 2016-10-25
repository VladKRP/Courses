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
			Console.WriteLine("Sum of two previous vector");
			vect3.displayVector();

			NVector vect4 = new NVector(2, 5, 1);
			NVector vect5 = new NVector(3, 1, 4);
			vect4.displayVector();
			vect5.displayVector();
			NVector vect6 = vect4 * vect5;
			Console.WriteLine("Multiply of two previous vector");
			vect6.displayVector();

			NVectorArray vecArr = new NVectorArray(vect1, vect2, vect3, vect4);
			Console.WriteLine("Vector array");
			vecArr.DisplayVectorArray();
		

			Console.ReadLine();
		}
	}
}
