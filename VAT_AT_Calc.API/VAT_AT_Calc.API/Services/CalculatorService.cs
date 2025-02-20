using VAT_AT_Calc.API.Contracts.Services;
using VAT_AT_Calc.API.Models;
using VAT_AT_Calc.API.Models.DTOs;

namespace VAT_AT_Calc.API.Services
{
    public class CalculatorService : ICalculatorService
    {
        public CalculatorService()
        {}

        public ValueAddedTaxAmounts CalculateValueAddedTax(CalculatorRequestDto calculatorRequestDto)
        {
            decimal net;
            decimal gross;
            decimal valueAddedTaxAmount;
            decimal VATrate = calculatorRequestDto.ValueAddedTaxRate;

            try
            {
                // there are 3 cases: we have net or gross or vat-amount
                if (calculatorRequestDto.Net == null && calculatorRequestDto.Gross == null) // valueAddedTaxAmount is known
                {
                    CalculateBasedOnValueAddedTaxAmount(calculatorRequestDto.ValueAddedTaxAmount, out net, out gross, out valueAddedTaxAmount, VATrate);
                }
                else if (calculatorRequestDto.Net == null && calculatorRequestDto.ValueAddedTaxAmount == null) // gross is known
                {
                    CalculateBasedOnGross(calculatorRequestDto.Gross, out net, out gross, out valueAddedTaxAmount, VATrate);
                }
                else if (calculatorRequestDto.Gross == null && calculatorRequestDto.ValueAddedTaxAmount == null) // net is known
                {
                    CalculateBasedOnNet(calculatorRequestDto.Net, out net, out gross, out valueAddedTaxAmount, VATrate);
                }
                else
                {
                    throw new Exception("Something went wrong! Inputs are null");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return new ValueAddedTaxAmounts
            {
                Gross = gross,
                Net = net,
                ValueAddedTaxAmount = valueAddedTaxAmount,
                ValueAddedTaxRate = calculatorRequestDto.ValueAddedTaxRate
            };
        }

        private static void CalculateBasedOnNet(decimal? inputNet,
                                                out decimal net,
                                                out decimal gross,
                                                out decimal valueAddedTaxAmount,
                                                decimal VATrate)
        {
            if (!inputNet.HasValue) { throw new Exception("Something went wrong! Inputs are null"); }

            net = inputNet.Value;
            valueAddedTaxAmount = net * (VATrate / 100);
            gross = net + valueAddedTaxAmount;
        }

        private static void CalculateBasedOnGross(decimal? inputGross,
                                                  out decimal net,
                                                  out decimal gross,
                                                  out decimal valueAddedTaxAmount,
                                                  decimal VATrate)
        {
            if (!inputGross.HasValue) { throw new Exception("Something went wrong! Inputs are null"); }

            gross = inputGross.Value;
            net = gross / (1 + (VATrate / 100));
            valueAddedTaxAmount = gross - net;
        }

        private static void CalculateBasedOnValueAddedTaxAmount(decimal? inputValueAddedTaxAmount,
                                                                out decimal net,
                                                                out decimal gross,
                                                                out decimal valueAddedTaxAmount,
                                                                decimal VATrate)

        {
            if (!inputValueAddedTaxAmount.HasValue) { throw new Exception("Something went wrong! Inputs are null"); }

            valueAddedTaxAmount = inputValueAddedTaxAmount.Value;
            net = inputValueAddedTaxAmount.Value / (VATrate / 100);
            gross = net + valueAddedTaxAmount;
        }
    }
}
