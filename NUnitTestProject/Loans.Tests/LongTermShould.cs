using Loans.Domain.Applications;
using NUnit.Framework;
using System;

namespace Loans.Tests
{
    [TestFixture]
    public class LongTermShould
    {
        [Test]
        [Ignore("Need to be completed")]
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

        [Test]
        public void NotAllowZeroYears()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            //Or
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("Message")
                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));

            //Or
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Message // <-- Property
                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));

            //Or
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(
                    ex => ex.ParamName == "years"
                ));

        }

        [Test]
        public void NotNull()
        {
            var name = "Raul";

            Assert.That(name, Is.Not.Null);
        }

        [Test]
        public void NotEmpty()
        {
            var name = "Raul";
            Assert.That(name, Is.Not.Empty);
        }

        [Test]
        public void IsNameEqual()
        {
            string name = "Raul";
            Assert.That(name, Is.EqualTo("Raul").IgnoreCase);
        }

        [Test]
        public void ContainsInName()
        {
            string name = "Raul";
            Assert.That(name, Does.StartWith("R").IgnoreCase);
            Assert.That(name, Does.EndWith("l").IgnoreCase);
            Assert.That(name, Does.Contain("au").IgnoreCase);
        }

        [Test]
        public void IsTrue()
        {
            bool isNew = true;
            Assert.That(isNew);
            Assert.That(isNew, Is.True);
            Assert.That(!isNew, Is.False);
            Assert.That(!isNew, Is.Not.True);
        }

        [Test]
        public void CheckValueRange()
        {
            int i = 42;
            Assert.That(i, Is.GreaterThan(41));
            Assert.That(i, Is.GreaterThanOrEqualTo(42));
            Assert.That(i, Is.LessThan(43));
            Assert.That(i, Is.LessThanOrEqualTo(42));
            Assert.That(i, Is.InRange(40, 50));
        }

    }
}