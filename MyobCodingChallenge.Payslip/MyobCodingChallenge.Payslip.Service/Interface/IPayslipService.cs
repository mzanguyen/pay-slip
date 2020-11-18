
namespace MyobCodingChallenge.Payslip.Service.Interface
{
    public interface IPayslipService
    {
        string GetPayslip(string name, decimal yearlyIncome);
    }
}
