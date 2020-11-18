
namespace MyobCodingChallenge.Payslip.Service.Interface
{
    public interface IIncomeService
    {
        decimal GetGrossMonthlyIncome(decimal income);
        decimal GetNetMonthlyIncome(decimal grossIncome, decimal tax);
    }
}
