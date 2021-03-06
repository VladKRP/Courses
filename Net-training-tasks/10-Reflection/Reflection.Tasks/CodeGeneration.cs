﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reflection.Tasks
{
    public class CodeGeneration
    {
        /// <summary>
        /// Returns the functions that returns vectors' scalar product:
        /// (a1, a2,...,aN) * (b1, b2, ..., bN) = a1*b1 + a2*b2 + ... + aN*bN
        /// Generally, CLR does not allow to implement such a method via generics to have one function for various number types:
        /// int, long, float. double.
        /// But it is possible to generate the method in the run time! 
        /// See the idea of code generation using Expression Tree at: 
        /// http://blogs.msdn.com/b/csharpfaq/archive/2009/09/14/generating-dynamic-methods-with-expression-trees-in-visual-studio-2010.aspx
        /// </summary>
        /// <typeparam name="T">number type (int, long, float etc)</typeparam>
        /// <returns>
        ///   The function that return scalar product of two vectors
        ///   The generated dynamic method should be equal to static MultuplyVectors (see below).   
        /// </returns>
        public static Func<T[], T[], T> GetVectorMultiplyFunction<T>() where T : struct {

            ParameterExpression firstVector = Expression.Parameter(typeof(T[]), "firstVector");
            ParameterExpression secondVector = Expression.Parameter(typeof(T[]), "secondVector");
            ParameterExpression result = Expression.Parameter(typeof(T), "result");
            ParameterExpression index = Expression.Parameter(typeof(int), "index");
            ParameterExpression vectorLength = Expression.Parameter(typeof(int), "vectorLength");

            Expression firstVectorAccesor = Expression.ArrayAccess(firstVector, index);
            Expression secondVectorAccessor = Expression.ArrayAccess(secondVector, index);

            LabelTarget label = Expression.Label(typeof(T)); 
     
            BlockExpression block = Expression.Block(
                new[] {result, index, vectorLength},
                Expression.Assign(result, Expression.Default(typeof(T))),
                Expression.Assign(index, Expression.Constant(0)),
                Expression.Assign(vectorLength, Expression.ArrayLength(firstVector)),
                Expression.Loop(
                    Expression.Block(
                        Expression.IfThenElse(
                            Expression.GreaterThan(vectorLength, index),
                                Expression.Block(
                                    Expression.AddAssign(result, Expression.Multiply(firstVectorAccesor, secondVectorAccessor)),
                                    Expression.PostIncrementAssign(index)
                                ),
                                Expression.Break(label, result))
                    ),
                    label
                )
            );
            return Expression.Lambda<Func<T[], T[], T>>(block,firstVector, secondVector).Compile();
        } 


        // Static solution to check performance benchmarks
        public static int MultuplyVectors(int[] first, int[] second) {
            int result = 0;
            for (int i = 0; i < first.Length; i++) {
                result += first[i] * second[i];
            }
            return result;
        }

    }
}
