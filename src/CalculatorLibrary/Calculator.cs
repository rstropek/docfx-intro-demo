using System;

namespace CalculatorLibrary
{
    public class Calculator
    {
        /// <summary>
        /// Makes a division.
        /// </summary>
        /// <param name="dividend">First parameter for the calculation</param>
        /// <param name="divisor">Second parameter for the calculation</param>
        /// <returns>The resulting quotient</returns>
        /// <example>
        /// <code source="../CalculatorTest/CalculatorTest.cs" region="FirstTest"></code>
        /// </example>
        /// <exception cref="DivideByZeroException">Thrown if divisor is zero</exception>
        /// <remarks>lorem ipsum</remarks>
        public static int Div(int dividend, int divisor)
        {
            return dividend / divisor;
        }
    }
}
