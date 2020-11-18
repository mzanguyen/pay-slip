using MyobCodingChallenge.Payslip.Service.Interface;
using System;

namespace MyobCodingChallenge.Payslip.Service.Services
{
    public class IncomeService: IIncomeService
    {
        public decimal GetGrossMonthlyIncome(decimal income)
        {
            if (income < 0)
                throw new Exception("Invalid input, income cannot be < 0");

            return income / 12;
        }

        public decimal GetNetMonthlyIncome(decimal grossIncome, decimal tax)
        {
            if (grossIncome < 0 || tax < 0)
                throw new Exception("Invalid input, income and tax cannot be < 0");

            return grossIncome - tax;
        }
    }
}
