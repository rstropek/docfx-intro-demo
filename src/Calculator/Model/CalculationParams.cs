using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Model
{
    /// <summary>
    /// Class for Calculation Parameters
    /// </summary>
    /// <value>valasd</value>
    public class CalculationParams
    {
        /// <summary>
        /// The first number of a division
        /// </summary>
        /// <value>The number's value</value>
        [DefaultValue(0)]
        public int Dividend { get; set; } = 0;

        /// <summary>
        /// The second number of a division
        /// </summary>
        /// <value>The number's value</value>
        [DefaultValue(0)]
        public int Divisor { get; set; } = 0;
    }
}
