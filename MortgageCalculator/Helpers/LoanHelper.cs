using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MortgageCalculator.Models;

namespace MortgageCalculator.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            // calculate the monthly payments

            //create loop for 1 to the term

            // calculate a payment schedule


            //push payments in the loan

            // return loan to the view

            return loan;
        }

        private decimal CalcPayment(decimal amount, decimal rate, int term)
        {
            rate = CalcMonthlyRate(rate);

            var rateD = Convert.ToDouble(rate);
            var amountD = Convert.ToDouble(amount);

            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));

            return Convert.ToDecimal(paymentD);
        }

        private decimal CalcMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }
    }
}
