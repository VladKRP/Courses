using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{

		interface IConvertible
		{
				string ConvertToCSharp(string str);
				string ConvertToVB(string str);
		}

		interface ICodeChecker
		{
				bool CodeCheckerSyntax(string stringForCheck, string language);
		}

    public class ProgramHelper:ProgramConverter,IConvertible,ICodeChecker
    {
				public string ConvertToCSharp(string str)
				{
						str = str + "\nCode converted to C#.\nClass ProgramHelper.";
						Console.WriteLine(str);
						return str;
				}

				public string ConvertToVB(string str)
				{
						str = str + "\nCode converted to VB.\nClass ProgramHelper.";
						Console.WriteLine(str);
						return str;
				}

				public bool CodeCheckerSyntax(string stringForCheck,string language)
				{
					return true;
				}
    }
		public class ProgramConverter:IConvertible
		{
				public string ConvertToCSharp(string str)
				{
					str = str + "\nCode converted to C#.\nClass ProgramConverter.";
					Console.WriteLine(str);
					return str;
				}

				public string ConvertToVB(string str)
				{
					str = str + "\nCode converted to VB.\nClass ProgramConverter.";
					Console.WriteLine(str);
					return str;
				}
		}
}
