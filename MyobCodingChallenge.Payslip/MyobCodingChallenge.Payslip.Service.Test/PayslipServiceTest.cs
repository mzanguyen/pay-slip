using MyobCodingChallenge.Payslip.Service.Interface;
using MyobCodingChallenge.Payslip.Service.Services;
using NSubstitute;
using Xunit;

namespace MyobCodingChallenge.Payslip.Service.Test
{
    public class PayslipServiceTest
    {
        private readonly ITaxService _taxService;
        private readonly IIncomeService _incomeService;

        public PayslipServiceTest()
        {
            _taxService = Substitute.For<ITaxService>();
            _incomeService = Substitute.For<IIncomeService>();
        }

        [Theory]
        [InlineData("Alicia", 130000.00, 500.0, 10500.00, 10000.00)]
        [InlineData("Ben", 60000.00, 500.0, 5000.00, 4500.00)]

        public void GetPayslip_Success(string name, decimal salary, decimal tax, decimal grossIncome, decimal netIncome)
        {
            _taxService.GetMonthlyIncomeTax(salary).Returns(tax);
            _incomeService.GetGrossMonthlyIncome(salary).Returns(grossIncome);
            _incomeService.GetNetMonthlyIncome(grossIncome, tax).Returns(netIncome);

            var response = (new PayslipService(_taxService, _incomeService)).GetPayslip(name, salary);

            Assert.Equal(GetExpectedString(name, tax, grossIncome, netIncome), response);

        }

        [Fact]
        public void GetPayslip_Fail()
        {
            var response = (new PayslipService(_taxService, _incomeService)).GetPayslip("Ben", -45000);

            Assert.Equal("Invalid input, income cannot be < 0", response);
        }

        public string GetExpectedString(string name, decimal tax, decimal grossIncome, decimal netIncome)
        {
            return $"Monthly Payslip for: {name} \n" +
                       $"Gross Monthly Income: {grossIncome:C2} \n" +
                       $"Monthly Income Tax: {tax:C2} \n" +
                       $"Net Monthly Income: {netIncome:C2} \n";
        }
    }
}
