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
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);
            //create loop for 1 to the term
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyRate(loan.Rate);
            // calculate a payment schedule
            for (int month = 1; month <= loan.Term; month++)
            {
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrinciple = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                loan.Payments.Add(loanPayment);

            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

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

        private decimal CalcMonthlyInterest(decimal balance, decimal montlyRate)
        {
            return balance * montlyRate;
        }
    }
}
