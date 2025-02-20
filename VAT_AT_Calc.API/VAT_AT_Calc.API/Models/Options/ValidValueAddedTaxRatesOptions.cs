namespace VAT_AT_Calc.API.Models.Options
{
    public class ValidValueAddedTaxRatesOptions
    {
        public const string ValidValueAddedTaxRates = "ValidValueAddedTaxRates";

        public List<uint> ValidValues { get; set; }
    }
}
