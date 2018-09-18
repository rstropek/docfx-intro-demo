using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorLibrary;
using CalculatorWebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        /// <summary>
        /// Makes a Division
        /// </summary>
        /// <remarks>Lorem ipsum</remarks>
        /// <param name="calculationParams">Two Numbers for a simple division</param>
        /// <returns>int</returns>
        /// <response code="200">Division successful</response>
        /// <response code="500">Division by zero not possible</response>
        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public ActionResult<int> Post([FromBody] CalculationParams calculationParams)
        {
            try
            {
                return Calculator.Div(calculationParams.Dividend, calculationParams.Divisor);
            }
            catch (DivideByZeroException)
            {
                return StatusCode(500);
            }
        }
    }
}
