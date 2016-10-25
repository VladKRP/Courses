using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
	/// <summary>
	/// Polynomial class
	/// </summary>
	public class Polynomial
    {
		/// <summary>
		/// The factor
		/// </summary>
		public double[] factor;
		private int degree;
		/// <summary>
		/// Gets or sets the string polinomial.
		/// </summary>
		/// <value>
		/// The string polinomial.
		/// </value>
		public string StrPolinomial { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="System.Double"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="System.Double"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Polynomial"/> class.
		/// </summary>
		/// <param name="monomialsSequence">The input factor.</param>
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

		/// <summary>
		/// Displays this instance.
		/// </summary>
		public void display()
		{
			Console.WriteLine(StrPolinomial);
		}


		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="pol1">The pol1.</param>
		/// <param name="pol2">The pol2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(Polynomial pol1, Polynomial pol2)
		{
			if (pol1.StrPolinomial == pol2.StrPolinomial)
			{
				return true;
			}
			return false;
		}


		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="pol1">The pol1.</param>
		/// <param name="pol2">The pol2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
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
