using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using VAT_AT_Calc.API.Models.Options;

namespace VAT_AT_Calc.API.Models.DTOs
{
    public class CalculatorRequestDto : IValidatableObject
    {
        public decimal? Net { get; set; }

        public decimal? Gross { get; set; }

        public decimal? ValueAddedTaxAmount { get; set; }

        [Required(ErrorMessage = "Value-added tax rate is missing.")]
        public uint ValueAddedTaxRate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var validValueAddedTaxRateValues = validationContext.GetService<IOptions<ValidValueAddedTaxRatesOptions>>()?.Value?.ValidValues;
            if (validValueAddedTaxRateValues == null || validValueAddedTaxRateValues.Count == 0)
            {
                throw new Exception("Configuration problem! VAT Rates are missing!");
            }

            string reason = string.Empty;

            if (!IsValidInputAmounts(Net, Gross, ValueAddedTaxAmount, out reason))
            {
                results.Add(new ValidationResult(reason));
            }

            if (!IsValidTaxRate(ValueAddedTaxRate, validValueAddedTaxRateValues, out reason))
            {
                results.Add(new ValidationResult(reason));
            }

            return results;
        }

        private bool IsValidInputAmounts(decimal? net, decimal? gross, decimal? valueAddedTaxAmount, out string reason)
        {
            int validInputs = 0;
            reason = string.Empty;

            if (net != null) { validInputs += 1; }
            if (gross != null) { validInputs += 1; }
            if (valueAddedTaxAmount != null) { validInputs += 1; }

            if (validInputs < 1)
            {
                reason = "Missing amount input";
                return false;
            }
            else if (validInputs > 1)
            {
                reason = "More than one input";
                return false;
            }

            return true;
        }

        private bool IsValidTaxRate(uint ValueAddedTaxRate, List<uint> validValueAddedTaxRateValues, out string reason)
        {
            reason = string.Empty;

            if (!validValueAddedTaxRateValues.Contains(ValueAddedTaxRate))
            {
                reason = "Invalid VAT rate!";
                return false;
            }
            return true;
        }
    }
}
