using MyobCodingChallenge.Payslip.Service.Interface;
using MyobCodingChallenge.Payslip.Service.Services;
using System;
using Xunit;

namespace MyobCodingChallenge.Payslip.Service.Test
{
    public class IncomeServiceTest
    {
        private IIncomeService _sut;
        public IncomeServiceTest()
        {
            _sut = new IncomeService();
        }

        [Theory]
        [InlineData(60000.00, 5000.00)]
        [InlineData(90000.00, 7500.00)]
        [InlineData(120000.00, 10000.00)]
        public void GetGrossMonthlyIncome_Success(decimal income, decimal expected)
        {
            var response = _sut.GetGrossMonthlyIncome(income);

            Assert.Equal(expected, response);
        }

        [Fact]
        public void GetGrossMonthlyIncome_ThrowException()
        {

            var ex = Assert.Throws<Exception>(() => _sut.GetGrossMonthlyIncome(-45));

            Assert.Equal("Invalid input, income cannot be < 0", ex.Message);
        }

        [Theory]
        [InlineData(60000.00, 5000.00, 55000.00)]
        [InlineData(90000.00, 7000.00, 83000.00)]
        public void GetNetIncome_Success(decimal income, decimal tax, decimal expected)
        {
            var response = _sut.GetNetMonthlyIncome(income, tax);

            Assert.Equal(expected, response);
        }

        [Fact]
        public void GetNetMonthlyIncome_ThrowException()
        {
            var ex = Assert.Throws<Exception>(() => _sut.GetNetMonthlyIncome(-45, 7));

            Assert.Equal("Invalid input, income and tax cannot be < 0", ex.Message);
        }
    }
}
