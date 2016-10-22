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
			public int width;
			public int height;
			static int matrixNumber = 0;
		
			public double this[int i, int j]
			{
					get
					{
							return matrix[i,j];
					}

					set 
					{
							matrix[i, j] = value;
					}

			} 

			public Matrix(int numLine,int numColumn)
			{
				width = numLine;
				height = numColumn;
				matrix = new double[width, height];
				for(int i = 0;i < width; i++) 
				{
						for(int j = 0;j < height; j++)
						{
								matrix[i,j] = 0;
						}
				}
				matrixNumber++;
			}

			public Matrix generateMatrix()
			{
					for (int i = 0; i < width; i++)
					{
						for (int j = 0; j < height; j++)
						{
							matrix[i, j] = 0;
						}
					}
					return new Matrix();
			}

  }
}
