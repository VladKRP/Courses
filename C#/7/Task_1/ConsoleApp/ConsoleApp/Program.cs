using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix matr = new Matrix(3, 4, 5);
			for(int i = 0;i<3;i++) {
				for (int j = 0; j < 4; j++)
					Console.Write(matr.matrix[i, j] + "\t");
				Console.WriteLine("\n");
			}
			Console.ReadLine();
		}
	}
}
