using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    public class MonthlyRepaymentTestData
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(200_000m, 6.5m, 30, 1264.14m);
                yield return new TestCaseData(500_000m, 10.5m, 30, 4573.7m);
            }
        }

        public static IEnumerable TestCases2
        {
            get
            {
                yield return new TestCaseData(200_000m, 6.5m, 30).Returns(1264.14m);
                yield return new TestCaseData(500_000m, 10.5m, 30).Returns(4573.7m);
            }
        }

    }
}
