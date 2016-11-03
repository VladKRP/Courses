using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
	public class Matrix: IComparable
  {
			public double[,] matrix;
			public int Line { get; set; }
			public int Column { get; set; }

			public double this[int i, int j]
			{
					get{	return matrix[i,j];}
					set{	matrix[i, j] = value;}
			}

			public Matrix(int numLine, int numColumn, double initialValues = 0)
			{
				if (numLine > 0 && numColumn > 0)
				{
					Line = numLine;
					Column = numColumn;
					matrix = new double[Line, Column];
					for(int i = 0; i < Line; i++)
					{
							for(int j = 0; j < Column; j++)
							{
									matrix[i,j] = initialValues;
							}
					}
				}
				else
					throw new MatrixException("Number line or column can't be less than 1.");
			}

			public Matrix(double [,] values)
			{
				Line = matrix.GetLength(0);
				Column = matrix.GetLength(1);

				if (Line > 0 && Column > 0)
						{
							for (int i = 0; i < Line; i++)
							{
								for (int j = 0; j < Column; j++)
								{
									matrix[i, j] = values[i, j];
								}
							}
						}
				else
					throw new MatrixException("Number line or column can't be less than 1.");
			}

			public static Matrix operator +(Matrix mx1, Matrix mx2)
			{
							if (mx1.Line == mx2.Line && mx1.Column == mx2.Column)
							{
								Matrix mx3 = new Matrix(mx1.Line,mx1.Column);
								for(int i =0; i < mx1.Line; i++)
								{
									for(int j = 0; j < mx1.Column; j++)
									{
											mx3[i, j] = mx1[i, j] + mx2[i, j];
									}
								}
								return mx3;
							}
							else
								throw new MatrixException("Matrix with different size can't be added.");
			}

			public static Matrix operator -(Matrix mx1, Matrix mx2)
			{
				
						if (mx1.Line == mx2.Line && mx1.Column == mx2.Column)
						{
							Matrix mx3 = new Matrix(mx1.Line, mx1.Column);
							for(int i = 0; i < mx1.Line; i++)
							{
								for(int j = 0; j < mx1.Column; j++)
								{
										mx3[i, j] = mx1[i, j] - mx2[i, j];
								}
							}
							return mx3;
						}
						else
							throw new Exception("Matrix with different size can't be deducted.");
			}

			public static Matrix operator *(Matrix mx1, Matrix mx2)
			{
				
						if (mx1.Column == mx2.Line)
						{
							Matrix mx3 = new Matrix(mx1.Line, mx2.Column);
							/*for(int i =0; i < mx1.line; i++)
							{
								double result = 0.0;
								for(int j = 0; j < mx1.column; j++)
								{
									result += mx1[i,j] * mx2[j,i];
									if(j == mx.column - 1)
											mx3[i,j] = result;
								}

							}*/
							return mx3;
						}
						else
						{
								throw new MatrixException("Number columns in first matrix must be equal number strings in matrix two.");
						}
			}

			public int CompareTo(object obj1)
			{
				Matrix mx = obj1 as Matrix;
				if (Line == mx.Line && Column == mx.Column)
				{
					for (int i = 0; i < Line; i++)
					{
						for (int j = 0; j < Column; j++)
						{
							if (matrix[i, j] != matrix[i, j])
								return -1;
						}
					}
					return 0;
				}
				else if (Line < mx.Line || Column < mx.Column)
					return -1;
				else
					return 1;	
			}
	}
}

public class MatrixException:Exception
{
		public MatrixException(string message)
			:base(message){ }
}