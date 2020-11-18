
namespace MyobCodingChallenge.Payslip.Service.Models
{
    public class TaxBand
    {
        public int UpperLimit { get; set; }
        public int LowerLimit { get; set; }
        public decimal Rate { get; set; }
    }
}
