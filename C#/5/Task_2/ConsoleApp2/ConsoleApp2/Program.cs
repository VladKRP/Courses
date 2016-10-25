using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib;

namespace ConsoleApp2
{
	class Program
	{
		static void Main(string[] args)
		{
			Polynomial polinomial = new Polynomial(2, 0.5, 7);
			polinomial.display();
			Console.ReadLine();
		}
	}
}
