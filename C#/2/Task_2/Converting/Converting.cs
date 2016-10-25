using System;

namespace Converting
{
		/// <summary>
		/// Class for converting numbers.
		/// </summary>
		public class ConvertingClass
			{
					/// <summary>
					/// Convert number from hex system to bin.
					/// </summary>
					/// <param name="number">The number in hex system.</param>
					/// <returns>The number in bin system.</returns>
					static public string ToBin(int number)
					{
						string resultString = null; //This string will strore binary representation initial number.
						try
						{
							if (number > 0)
							{
									resultString = System.Convert.ToString(number, 2); //Convert number to bin system.
							}
							else
									throw new Exception("Number is negative.");
						}
						//If number negative run exception.
						catch (Exception exc)
						{
								Console.WriteLine("Error: " + exc.Message);
						}

						return resultString;
					}
			}
}