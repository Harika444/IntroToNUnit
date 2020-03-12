using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Loans.Domain.Applications;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanRepaymentCalculatorShould
    {
        //Original approach was executing the method setting up the amounts explicitly 
        [Test]
        public void CalculateCorrectMonthlyRepayment()
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", 200_000), 6.5m, new LoanTerm(30));

            Assert.That(monthlyPayment, Is.EqualTo(1264.14));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_10Percent()
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", 200_000), 10m, new LoanTerm(30));

            Assert.That(monthlyPayment, Is.EqualTo(1755.14));
        }

        //Optimized to have one single Test with serveral inputs
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyPayment_Optimized(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        //Another optimized sintax
        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyPayment_Optimized(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            
        }

        //Data Source from another location
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        //Data Source from another location v2
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases2")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }


        // CSV
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal,
                                     decimal interestRate,
                                     int termInYears,
                                     decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                     new LoanAmount("USD", principal),
                                     interestRate,
                                     new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

    }
}

