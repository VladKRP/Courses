using System;

namespace Converting
{
	
		public class ConvertingClass
			{
					
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