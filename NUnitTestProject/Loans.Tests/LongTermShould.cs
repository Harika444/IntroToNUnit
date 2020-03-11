using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LongTermShould
    {
        [Test]
        public void ReturnTermInMonths()
        {
            //Arrange
            var sut = new LoanTerm(1);

            //Act
            var result = sut.ToMonths();

            //Assert
            Assert.That(result, Is.EqualTo(12), "Months should be 12 * number of years");
        }

        [Test]
        public void StoreYears()
        {
            //Arrage
            var sut = new LoanTerm(1);

            //Act
            var result = sut.Years;

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            //Arrange
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            //Act

            //Assert
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            //Arrange
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);
            //Act

            //Assert
            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(2);
            //Same checks references, not values.
            Assert.That(a, Is.SameAs(b)); //Passes
            //Assert.That(a, Is.SameAs(c)); //Fails
            Assert.That(a, Is.Not.SameAs(c));
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.004)); //Tolerance of amount
            Assert.That(a, Is.EqualTo(0.33).Within(5).Percent); // Tolerance of percentage
        }

    }
}
