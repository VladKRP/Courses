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
		static public void OutCoordinate(string fileInDebugDirectory)
		{

				//File must be stored in debug folder in project ConsoleApp.
				string path = Environment.CurrentDirectory + "\\" + fileInDebugDirectory;
				
			
						try 
						{
								if (File.Exists(path))//If file exist we start reading from it.
								{
									using (StreamReader file = new StreamReader(path, System.Text.Encoding.Default))
									{
										string line;
										while ((line = file.ReadLine()) != null)//Read file from line to line by the end.
										{
											string[] coordinate = line.Split(new char[] { ',' });//Divide each line of text on two part.
											if (coordinate.Length == 2)
												Console.WriteLine("X:" + coordinate[0] + " " + "Y:" + coordinate[1]);//Out edit file info on console
										}
									}
								}
								else
									throw new Exception("File not found");//Throw exseption when file not found in folder. 
						}
						catch(Exception exc)
						{
								Console.WriteLine("Error:" + exc.Message);//Out message that file not found.
						}
							
				}
		}
	}

