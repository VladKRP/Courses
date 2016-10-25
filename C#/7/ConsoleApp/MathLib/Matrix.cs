using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{

	/*Разработать тип для работы с матрицами (не менее двух конструкторов).
	Индексатор, свойства, интерфейс Icomparable, метод для обратной матрицы.
	Реализовать методы, позволяющие выполнять операции сложения, вычитания и умножения матриц,
	предусмотрев возможность их выполнения, в противном случае должно генерироваться исключение.
	Cвои классы ошиок
	*/
	public class Matrix
  {
			public double[,] matrix;
			public int line;
			public int column;
			static int matrixNumber = 0;

			public double this[int i, int j]
			{
					get{	return matrix[i,j];}
					set {	matrix[i, j] = value;}
			}

			public Matrix(int numLine,int numColumn, double initialValues = 0)
			{
				line = numLine;
				column = numColumn;
				matrix = new double[width, height];
				for(int i = 0;i < width; i++)
				{
						for(int j = 0;j < height; j++)
						{
								matrix[i,j] = initialValues;
						}
				}
				matrixNumber++;
			}

			public Matrix(int numLine,int numColumn, double [,] values)
			{
					for(int i =0; i< numLine; i++)
					{
						for(int j =0; j < numColumn; j++)
						{
							matrix[i, j] = values[i, j];
						}
					}
			}

			public inverseMatrix()
			{
				try
				{
						if(matrixDeterminant != 0){

						}
						else
						{
							throw new Exception("When matrix determinant equal zero inverse matrix doesn't exist.")
						}
				}
				catch(Exception exc)
				{
					Console.WriteLine("Error:" + exc.Message);
				}

			}

			public static Matrix operator +(Matrix mx1, Matrix mx2)
			{
					try
					{
							if (mx1.line == mx2.line && mx1.column == mx2.column)
							{
								Matrix mx3 = new Matrix(mx1.line,mx1.column);
								for(int i =0; i < mx1.line; i++)
								{
									for(int j = 0; j < mx1.column; j++)
									{
											mx3[i, j] = mx1[i, j] + mx2[i, j];
									}
								}
								return mx3;
							}
							else
							{
									throw new Exception("Matrix with different size can't be added.");
							}
					}
					catch(Exception exc)
					{
						Console.WriteLine("Error:" + exc.Message);
						Matrix mx3 = new Matrix(1, 1, 0);
						return mx3;
					}
			}

			public static Matrix operator -(Matrix mx1, Matrix mx2)
			{
				try
				{
						if (mx1.line == mx2.line && mx1.column == mx2.column)
						{
							Matrix mx3 = new Matrix(mx1.line,mx1.column);
							for(int i =0; i < mx1.line; i++)
							{
								for(int j = 0; j < mx1.column; j++)
								{
										mx3[i, j] = mx1[i, j] - mx2[i, j];
								}
							}
							return mx3;
						}
						else
						{
								throw new Exception("Matrix with different size can't be deducted.");
						}
				}
				catch(Exception exc)
				{
					Console.WriteLine("Error:" + exc.Message);
					Matrix mx3 = new Matrix(1, 1, 0);
					return mx3;
				}
			}

			public static Matrix operator *(Matrix mx1, Matrix mx2)
			{
				try
				{
						if (mx1.column == mx2.line)
						{
							Matrix mx3 = new Matrix(mx1.line,mx2.column);
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
								throw new Exception("Number columns in first matrix must be equal number strings in matrix two.");
						}
				}
				catch(Exception exc)
				{
					Console.WriteLine("Error:" + exc.Message);
					Matrix mx3 = new Matrix(1, 1, 0);
					return mx3;
				}
			}

  }
}
