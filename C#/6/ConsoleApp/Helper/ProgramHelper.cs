using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Namespace Helper
/// </summary>
namespace Helper
{

	/// <summary>
	/// IConvertible interface
	/// </summary>
	interface IConvertible
		{
				string ConvertToCSharp(string str);
				string ConvertToVB(string str);
		}

	/// <summary>
	/// interface ICodeChecker
	/// </summary>
	interface ICodeChecker
		{
				bool CodeCheckerSyntax(string stringForCheck, string language);
		}

	public class ProgramHelper:ProgramConverter,ICodeChecker
    {
				public bool CodeCheckerSyntax(string stringForCheck,string language)
				{
					Console.WriteLine("Code checker");
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
