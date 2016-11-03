using System;
using ShapesLib;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{

			Triangle[] triangle = { new Triangle(10, 10, 6, 19, 10, 20), new Triangle(8, 10, 7) };//Create array of object Triangle
			for(int i = 0; i < 2; i++) 
			{
					if (triangle[i].isExist())//If triangle exist print information about it.
					{
						Console.WriteLine("Triangle is exist");
						Console.WriteLine("Sides: {0} {1} {2} ",triangle[i].sideA, triangle[i].sideB, triangle[i].sideC);
						Console.WriteLine("Perimetr:" + triangle[i].Perimetr());
						Console.WriteLine("Square:" + triangle[i].Square());
					}
					else
						Console.WriteLine("Triangle isn't exist");
					Console.WriteLine("\n");
			}

			try {
				Triangle exceptionTriangle = new Triangle(-10, 8, 7);//When side negative throw exception.
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
			
			Console.ReadLine();
		}
	}
}
