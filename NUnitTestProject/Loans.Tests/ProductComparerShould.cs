using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    [TestFixture]
    public class ProductComparerShould
    {
        private List<LoanProduct> _products;
        private ProductComparer _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Console.Out.WriteLine("One time executed");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            Console.Out.WriteLine("One time executed");
        }

        [SetUp]
        public void Setup()
        {
            _products = new List<LoanProduct>
            {
                new LoanProduct(1,"a",1),
                new LoanProduct(2,"b",2),
                new LoanProduct(3,"c",3),
                new LoanProduct(4,"d",4)
            };

            _sut = new ProductComparer(new LoanAmount("USD", 200_000m), _products);
        }

        [TearDown]
        public void Teardown()
        {
            _products.Clear();
            _sut = null;
        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            //Arrange
            //Code moved into Setup method            

            // Act
            List<MonthlyRepaymentComparison> comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert
            Assert.That(comparisons, Has.Exactly(4).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            //Arrange
           

            // Act
            List<MonthlyRepaymentComparison> comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert
            Assert.That(comparisons, Is.Unique); //Check for no duplicated items
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            //Arrange
           

            // Act
            List<MonthlyRepaymentComparison> comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            //Assert
            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            //Arrange
           
            // Act
            List<MonthlyRepaymentComparison> comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Don't care about the expected monthly repayment, only that the product is there 
            //Assert
            Assert.That(comparisons, Has.Exactly(1)
                .Property("ProductName").EqualTo("a")
                .And.Property("MonthlyRepayment").GreaterThan(0));

            //Same Assert but typed
            Assert.That(comparisons, Has.Exactly(1)
                .Matches<MonthlyRepaymentComparison>(
                    item => item.ProductName == "a" &&
                    item.InterestRate == 1 &&
                    item.MonthlyRepayment > 0
                ));
        }
    }
}