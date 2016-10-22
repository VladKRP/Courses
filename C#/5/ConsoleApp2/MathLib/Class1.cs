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
				public static int polynomialCounter = 0;

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

				public Polynomial(params double[] factor)
				{
						this.factor = factor;
				}


    }
}
