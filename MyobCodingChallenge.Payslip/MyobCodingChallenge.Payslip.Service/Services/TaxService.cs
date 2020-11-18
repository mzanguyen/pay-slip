using Microsoft.Extensions.Options;
using MyobCodingChallenge.Payslip.Service.Interface;
using MyobCodingChallenge.Payslip.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyobCodingChallenge.Payslip.Service.Services
{
    public class TaxService: ITaxService
    {
        private readonly List<TaxBand> _taxBand;

        public TaxService(IOptions<List<TaxBand>> options) => 
            _taxBand = options.Value.OrderBy(band => band.LowerLimit).ToList();

        public decimal GetMonthlyIncomeTax(decimal salary)
        {
            if (salary < 0)
                throw new Exception("Invalid input, income cannot be < 0");

            if (salary == 0)
                return 0;

            return GetIncomeTax(salary)/12;
        }

        private decimal GetIncomeTax(decimal income)
        {
            for(var i = 0; i < _taxBand.Count; i++)
            {
                if(income > _taxBand[i].LowerLimit && income <= _taxBand[i].UpperLimit)
                    return CalculateIncomeTax(income, i);
            }

            return CalculateIncomeTax(income, _taxBand.Count - 1);
        }

        private decimal CalculateIncomeTax(decimal income, int taxBandIndex)
        {
            if (taxBandIndex >= _taxBand.Count || income <= 0)
                return 0;

            var taxBand = _taxBand[taxBandIndex];
            var currentBandTax = (income - taxBand.LowerLimit) * taxBand.Rate;

            if (taxBandIndex == 0)
                return currentBandTax;

            return currentBandTax + CalculateIncomeTax(taxBand.LowerLimit, taxBandIndex - 1);
        }
    }
}
