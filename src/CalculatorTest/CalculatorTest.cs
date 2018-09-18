using CalculatorLibrary;
using System;
using Xunit;

namespace CalculatorTest
{
    public class CalculatorTest
    {
        #region FirstTest
        [Fact]
        public void SimpleDivision()
        {
            Assert.Equal(2, Calculator.Div(10, 5));
        }
        #endregion
        
        [Fact(Skip ="Only if failing test is needed")]
        public void FailingDivision()
        {
            Assert.Equal(3, Calculator.Div(10, 5));
        }
        
    }
}
