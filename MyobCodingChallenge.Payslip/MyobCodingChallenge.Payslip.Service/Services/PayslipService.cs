using MyobCodingChallenge.Payslip.Service.Interface;
using System;

namespace MyobCodingChallenge.Payslip.Service.Services
{
    public class PayslipService: IPayslipService
    {
        private readonly ITaxService _taxService;
        private readonly IIncomeService _incomeService;

        public PayslipService(ITaxService taxService, IIncomeService incomeService)
            => (_taxService, _incomeService) = (taxService, incomeService);

        public string GetPayslip(string name, decimal yearlyIncome)
        {
            try
            {
                if (yearlyIncome < 0)
                    throw new Exception("Invalid input, income cannot be < 0");

                var monthlyTax = _taxService.GetMonthlyIncomeTax(yearlyIncome);
                var grossMonthlyIncome = _incomeService.GetGrossMonthlyIncome(yearlyIncome);
                var netMonthlyIncome = _incomeService.GetNetMonthlyIncome(grossMonthlyIncome, monthlyTax);

                // Round to 2 decimal places
                return $"Monthly Payslip for: {name} \n" +
                       $"Gross Monthly Income: {grossMonthlyIncome:C2} \n" +
                       $"Monthly Income Tax: {monthlyTax:C2} \n" +
                       $"Net Monthly Income: {netMonthlyIncome:C2} \n";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

    }
}
