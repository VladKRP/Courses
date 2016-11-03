using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
	public class Polynomial
    {
			public double[] factor;
			private int degree;
			public string StrPolinomial { get; set; }

			public double this[int index]
					{
							get {
									return factor[index];
							}
							set
							{
									factor[index] = value;
							}
					}

			public Polynomial(params double[] monomialsSequence)
					{
							degree = monomialsSequence.Length;
							factor = new double[degree];
							for(int i = 0; i < degree; i++)
							{
									factor[i] = monomialsSequence[i];
							}
							GeneratePolinomial();
					}

			private void GeneratePolinomial()
			{
				for (int i = 0, j = degree - 1; i < degree; i++, j--)
					if (j > 0)
						StrPolinomial += factor[i].ToString() + "x^" + j + "+";
					else
						StrPolinomial += factor[i].ToString();
			}


			public void display()
			{
				Console.WriteLine(StrPolinomial);
			}



			public static bool operator ==(Polynomial pol1, Polynomial pol2)
			{
				if (pol1.StrPolinomial == pol2.StrPolinomial)
				{
					return true;
				}
				return false;
			}


			public static bool operator !=(Polynomial pol1, Polynomial pol2)
			{
				if (pol1.StrPolinomial != pol2.StrPolinomial)
				{
					return true;
				}
				return false;
			}


	}
}
