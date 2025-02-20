using VAT_AT_Calc.API.Models.DTOs;
using VAT_AT_Calc.API.Models;

namespace VAT_AT_Calc.API.Contracts.Services
{
    public interface ICalculatorService
    {
        ValueAddedTaxAmounts CalculateValueAddedTax(CalculatorRequestDto calculatorRequestDto);
    }
}