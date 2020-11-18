using Microsoft.Extensions.Options;
using MyobCodingChallenge.Payslip.Service.Interface;
using MyobCodingChallenge.Payslip.Service.Models;
using MyobCodingChallenge.Payslip.Service.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyobCodingChallenge.Payslip.Service.Test
{
    public class TaxServiceTest
    {
        private IOptions<List<TaxBand>> _options;
        private ITaxService _sut;
        public TaxServiceTest()
        {
            _options = Options.Create(GetMockTaxBands());
            _sut = new TaxService(_options);
        }

        [Theory]
        [InlineData(60000.00, 500.0)]
        [InlineData(40000.00, 166.667)]
        [InlineData(30000.00, 83.333)]
        [InlineData(80000.00, 833.333)]
        [InlineData(1200000.00, 28833.333)]
        [InlineData(0.00, 0.00)]
        public void GetPayslip_Success(decimal salary, decimal expected)
        {
            var response = _sut.GetMonthlyIncomeTax(salary);

            Assert.Equal(expected, Math.Round(response, 3));
        }

        #region Helpers
        private List<TaxBand> GetMockTaxBands()
        {
            return new List<TaxBand>() {
            new TaxBand()
            {
                UpperLimit = 20000,
                LowerLimit = 0,
                Rate = 0
            }, new TaxBand()
            {
                UpperLimit = 80000,
                LowerLimit = 40000,
                Rate = 0.2M
            },
            new TaxBand()
            {
                UpperLimit = 40000,
                LowerLimit = 20000,
                Rate = 0.1M
            },
                new TaxBand()
            {
                UpperLimit = 0,
                LowerLimit = 80000,
                Rate = 0.3M
            }};
        }
        #endregion
    }
}
