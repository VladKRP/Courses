using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLib
{
	/// <summary>
	/// Class NVector
	/// </summary>
	public class NVector
    {
		//Vector coordinates
					public double X { get; set; }
					public double Y { get; set; }
					public double Z { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NVector"/> class.
		/// </summary>
		public NVector()
					{
						 X = 0.0;
						 Y = 0.0;
						 Z = 0.0;
					}


		/// <summary>
		/// Initializes a new instance of the <see cref="NVector"/> class.
		/// </summary>
		/// <param name="xCoordiante">The x coordiante.</param>
		/// <param name="yCoordinate">The y coordinate.</param>
		/// <param name="zCoordinate">The z coordinate.</param>
		public NVector(double xCoordiante, double yCoordinate, double zCoordinate)
		{
				X = xCoordiante;
				Y = yCoordinate;
				Z = zCoordinate;	
		}

		/// <summary>
		/// Displays the vector.
		/// </summary>
		public void displayVector()
		{
				Console.WriteLine("({0};{1};{2})", X, Y, Z);
		}

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="vector1">The vector1.</param>
		/// <param name="vector2">The vector2.</param>
		/// <returns>
		/// Sum of two vector.
		/// </returns>
		public static NVector operator +(NVector vector1, NVector vector2)
					{
							double xCoordinate = vector1.X + vector2.X;
							double yCoordinate = vector1.Y + vector2.Y;
							double zCoordinate = vector1.Z + vector2.Z;

							NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="vector1">The vector1.</param>
		/// <param name="vector2">The vector2.</param>
		/// <returns>
		/// Two vector residual.
		/// </returns>
		public static NVector operator -(NVector vector1, NVector vector2)
					{
							double xCoordinate = vector1.X - vector2.X;
							double yCoordinate = vector1.Y - vector2.Y;
							double zCoordinate = vector1.Z - vector2.Z;

							NVector obtainedVector = new  NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="vector1">The vector1.</param>
		/// <param name="vector2">The vector2.</param>
		/// <returns>
		/// Multiply two vector.
		/// </returns>
		public static NVector operator *(NVector vector1, NVector vector2)
					{
						double xCoordinate = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
						double yCoordinate = vector1.Z * vector2.X - vector1.X * vector2.Z;
						double zCoordinate = vector1.X * vector2.Y - vector1.Y * vector2.X;

						NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
						return obtainedVector;
					}

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="number">The number.</param>
		/// <returns>
		/// Multiply vector and number.
		/// </returns>
		public static NVector operator *(NVector vector, double number)
					{
							double xCoordinate = vector.X * number;
							double yCoordinate = vector.Y * number;
							double zCoordinate = vector.Z * number;

							NVector obtainedVector = new NVector(xCoordinate, yCoordinate, zCoordinate);
							return obtainedVector;
					}

		}

		//public class NVectorArray
		//{
		//		public NVector[] vectorArray = null;

		//		public NVector this[int index]
		//		{
		//				get 
		//				{
		//						return vectorArray[index];
		//				}
		//				set
		//				{
		//						vectorArray[index] = value;
		//				}
		//		}

		//		public NVectorArray(int size)
		//		{
		//				vectorArray = new NVector[size];
		//		}
		//}
}
