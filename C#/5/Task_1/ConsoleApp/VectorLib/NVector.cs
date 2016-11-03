using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLib
{

	public class NVector
    {
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public NVector(double xCoordiante, double yCoordinate, double zCoordinate)
		{
				X = xCoordiante;
				Y = yCoordinate;
				Z = zCoordinate;	
		}

		public void displayVector()
		{
				Console.WriteLine("({0};{1};{2})", X, Y, Z);
		}

		public static NVector operator +(NVector vector1, NVector vector2)
					{
							double xCoordinate = vector1.X + vector2.X;
							double yCoordinate = vector1.Y + vector2.Y;
							double zCoordinate = vector1.Z + vector2.Z;

							NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		public static NVector operator -(NVector vector1, NVector vector2)
					{
							double xCoordinate = vector1.X - vector2.X;
							double yCoordinate = vector1.Y - vector2.Y;
							double zCoordinate = vector1.Z - vector2.Z;

							NVector obtainedVector = new  NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		public static NVector operator *(NVector vector1, NVector vector2)
					{
						double xCoordinate = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
						double yCoordinate = vector1.Z * vector2.X - vector1.X * vector2.Z;
						double zCoordinate = vector1.X * vector2.Y - vector1.Y * vector2.X;

						NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
						return obtainedVector;
					}

		public static NVector operator *(NVector vector, double number)
					{
							double xCoordinate = vector.X * number;
							double yCoordinate = vector.Y * number;
							double zCoordinate = vector.Z * number;

							NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		}


	public class NVectorArray
	{
		
		public NVector[] vectorArray;
		private int size;

	
		public NVector this[int index]
		{
			get
			{
				return vectorArray[index];
			}
			set
			{
				vectorArray[index] = value;
			}
		}

		public NVectorArray(params NVector[] array)
		{
			size = array.Length;
			vectorArray = new NVector[size];
			for (int i = 0; i < size; i++)
				vectorArray[i] = array[i];
		}

		public void DisplayVectorArray()
		{
			for (int i = 0; i < size; i++)
				vectorArray[i].displayVector();
		}
	}
}
