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

		/// <summary>
		/// Gets or sets the x coordinate of vector.
		/// </summary>
		/// <value>
		/// The x.
		/// </value>
		public double X { get; set; }
		/// <summary>
		/// Gets or sets the y coordinate of vector.
		/// </summary>
		/// <value>
		/// The y.
		/// </value>
		public double Y { get; set; }
		/// <summary>
		/// Gets or sets the z coordinate of vector.
		/// </summary>
		/// <value>
		/// The z.
		/// </value>
		public double Z { get; set; }

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
		/// Implements the operator * for vector and number.
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <param name="number">The number.</param>
		/// <returns>
		/// Result of multiplying vector and number.
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



	/// <summary>
	/// NVectorArray implement array of NVector
	/// </summary>
	public class NVectorArray
	{
		/// <summary>
		/// The NVector array
		/// </summary>
		public NVector[] vectorArray;
		private int size;

		/// <summary>
		/// Gets or sets the <see cref="NVector"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="NVector"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="NVectorArray"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		public NVectorArray(params NVector[] array)
		{
			size = array.Length;
			vectorArray = new NVector[size];
			for (int i = 0; i < size; i++)
				vectorArray[i] = array[i];
		}

		/// <summary>
		/// Displays the vector array.
		/// </summary>
		public void DisplayVectorArray()
		{
			for (int i = 0; i < size; i++)
				vectorArray[i].displayVector();
		}
	}
}
