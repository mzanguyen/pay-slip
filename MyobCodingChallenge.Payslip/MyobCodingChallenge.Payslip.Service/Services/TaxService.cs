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
        private readonly List<TaxBand> _taxBands;

        public TaxService(IOptions<List<TaxBand>> options) => 
            _taxBands = options.Value.OrderBy(band => band.LowerLimit).ToList();

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
            var tax = 0M;

            for (var i = 0; i < _taxBands.Count; i++)
            {
                var taxBand = _taxBands[i];

                // reach highest bracket for this income
                if ((income > taxBand.LowerLimit && income <= taxBand.UpperLimit) || taxBand.UpperLimit == 0)
                {
                    tax += (income - taxBand.LowerLimit) * taxBand.Rate;
                    break;
                }

                tax += (taxBand.UpperLimit - taxBand.LowerLimit) * taxBand.Rate;
            }

            return tax;
        }

        // another way to calculate tax, using recursion
        private decimal GetIncomeTaxV2(decimal income)
        {
            for(var i = 0; i < _taxBands.Count; i++)
            {
                if(income > _taxBands[i].LowerLimit && income <= _taxBands[i].UpperLimit)
                    return CalculateIncomeTax(income, i);
            }

            return CalculateIncomeTax(income, _taxBands.Count - 1);
        }

        private decimal CalculateIncomeTax(decimal income, int taxBandIndex)
        {
            if (taxBandIndex >= _taxBands.Count || income <= 0)
                return 0;

            var taxBand = _taxBands[taxBandIndex];
            var currentBandTax = (income - taxBand.LowerLimit) * taxBand.Rate;

            if (taxBandIndex == 0)
                return currentBandTax;

            return currentBandTax + CalculateIncomeTax(taxBand.LowerLimit, taxBandIndex - 1);
        }
    }
}
