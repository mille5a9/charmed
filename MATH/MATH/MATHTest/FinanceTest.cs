using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATH;
using System;

namespace MATHTest
{
    [TestClass]
    public class FinanceTest
    {
        [TestMethod]
        public void TestCompoundInterest()
        {
            double? ans = Finance.CompoundInterest(1000, .05, 10, 1);
            Assert.AreEqual((double)Math.Round((decimal)ans, 2), 1628.89);
        }
        [TestMethod]
        public void TestPresentToFutureValue()
        {
            double? ans = Finance.PresentToFutureValue(1000, .05, 5);
            Assert.AreEqual((double)Math.Round((decimal)ans, 2), 1276.28);
        }
        [TestMethod]
        public void TestEffectiveAnnualRate()
        {
            double? ans = Finance.EffectiveAnnualRate(.05, 12);
            Assert.AreEqual((double)Math.Round((decimal)ans, 4), 5.1162);
        }
        [TestMethod]
        public void TestSeventyTwoRule()
        {
            double? ans = Finance.SeventyTwoRule(.05);
            Assert.AreEqual((double)Math.Round((decimal)ans, 2), 14.4);
        }
        [TestMethod]
        public void TestCompoundedAnnualGrowthRate()
        {
            double? ans = Finance.CompoundedAnnualGrowthRate(2000, 1000, 10);
            Assert.AreEqual((double)Math.Round((decimal)ans, 4), 0.0718);
        }
        [TestMethod]
        public void TestEquatedMonthlyInstallments()
        {
            double? ans = Finance.EquatedMonthlyInstallments(25000, 0.02, 180);
            Assert.AreEqual((double)Math.Round((decimal)ans, 2), 160.88);
        }
        [TestMethod]
        public void TestFuturePlanValue()
        {
            double? ans = Finance.FuturePlanValue(30, 0.02, 360);
            Assert.AreEqual((double)Math.Round((decimal)ans, 2), 14781.76);
        }
    }
}
