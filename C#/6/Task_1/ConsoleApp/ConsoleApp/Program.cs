using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ProgramConverter[] testInterf = new ProgramConverter[3];
			testInterf[0] = new ProgramConverter();
			testInterf[1] = new ProgramHelper();

			testInterf[0].ConvertToCSharp("Hello");
			testInterf[0].ConvertToVB("Hello");
			

			testInterf[1].ConvertToCSharp("Hello");
			testInterf[1].ConvertToVB("Hello");

			ProgramHelper p1 = new ProgramHelper();
			p1.CodeCheckerSyntax("hOHO","C++");


			Console.Read();
		}
	}
}
