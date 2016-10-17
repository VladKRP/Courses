using System;
using System.IO;

namespace TextEditLib
{
	/// <summary>
	/// TextEdit class
	/// </summary>
	public class TextEdit
	{
		/// <summary>
		/// Outs the coordinate.
		/// </summary>
		/// <param name="fileInDebugDirectory">The file in debug directory.</param>
		static public void outCoordinate(string fileInDebugDirectory)
		{

				//File must be stored in debug folder in project ConsoleApp.
				string path = Environment.CurrentDirectory + "\\" + fileInDebugDirectory;

				using (StreamReader file = new StreamReader(path, System.Text.Encoding.Default))
				{
							string line;
							while ((line = file.ReadLine()) != null)//Read file from line to line by the end.
							{
								string[] coordinate = line.Split(new char[] { ',' });//Divide each line of text on two part.
								if(coordinate.Length  == 2)
										Console.WriteLine("X:" + coordinate[0] + " " + "Y:" + coordinate[1]);//Out edit file info on console
							}
				}
		}
	}
}
