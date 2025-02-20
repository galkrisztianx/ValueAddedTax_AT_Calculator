using Microsoft.AspNetCore.Mvc;
using VAT_AT_Calc.API.Contracts.Services;
using VAT_AT_Calc.API.Models;
using VAT_AT_Calc.API.Models.DTOs;

namespace VAT_AT_Calc.API.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost("CalculateValueAddedTax")]
        public ActionResult<ValueAddedTaxAmounts> CalculateValueAddedTax([FromBody] CalculatorRequestDto calculatorRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var valueAddedTaxAmounts = _calculatorService.CalculateValueAddedTax(calculatorRequestDto);

            return valueAddedTaxAmounts;
        }
    }
}