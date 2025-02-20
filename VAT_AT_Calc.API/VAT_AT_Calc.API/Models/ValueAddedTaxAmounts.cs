namespace VAT_AT_Calc.API.Models
{
    public class ValueAddedTaxAmounts
    {
        public decimal Net { get; set; }

        public decimal Gross { get; set; }

        public decimal ValueAddedTaxAmount { get; set; }

        public uint ValueAddedTaxRate { get; set; }
    }
}
