using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATH;

namespace MATHTest
{
    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void TestUnaryPlus()
        {
            Expression x = new Expression("+1");
            Assert.AreEqual(x.Solve().Value, 1);
        }
        [TestMethod]
        public void TestUnaryMinus()
        {
            Expression x = new Expression("-1");
            Assert.AreEqual(x.Solve().Value, -1);
        }
        //Unfortunately the technology just isn't here yet
        //[TestMethod]
        //public void TestUnaryNot()
        //{
        //    Expression x = new Expression("!1");
        //    Assert.AreEqual(x.Solve().Value, false);
        //}
        [TestMethod]
        public void TestUnaryComplement()
        {
            Expression x = new Expression("~5");
            Assert.AreEqual(x.Solve().Value, ~5);
        }
        [TestMethod]
        public void TestUnaryFactorial()
        {
            Expression x = new Expression("!!5");
            Assert.AreEqual(x.Solve().Value, 120);
        }
        [TestMethod]
        public void TestUnaryIncrement()
        {
            Expression x = new Expression("++5");
            Assert.AreEqual(x.Solve().Value, 6);
        }
        [TestMethod]
        public void TestUnaryDecrement()
        {
            Expression x = new Expression("--5");
            Assert.AreEqual(x.Solve().Value, 4);
        }
        [TestMethod]
        public void TestExp()
        {
            Expression x = new Expression("2^^5");
            Assert.AreEqual(x.Solve().Value, 32);
        }
        [TestMethod]
        public void TestBinaryMultiplication()
        {
            Expression x = new Expression("2*5");
            Assert.AreEqual(x.Solve().Value, 10);
        }
        [TestMethod]
        public void TestBinaryDivision()
        {
            Expression x = new Expression("10/8");
            Assert.AreEqual(x.Solve().Value, 1.25);
        }
        [TestMethod]
        public void TestBinaryModulo()
        {
            Expression x = new Expression("10 % 8.5");
            Assert.AreEqual(x.Solve().Value, 1.5);
        }
        [TestMethod]
        public void TestBinaryAddition()
        {
            Expression x = new Expression("10 + 8.5");
            Assert.AreEqual(x.Solve().Value, 18.5);
        }
        [TestMethod]
        public void TestBinarySubtraction()
        {
            Expression x = new Expression("20 - 8.5");
            Assert.AreEqual(x.Solve().Value, 11.5);
        }
        //AValue type currently does not support boolean values :(
        //[TestMethod]
        //public void TestBinaryLessThan()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryLessThanEqualTo()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryGreaterThan()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryGreaterThanEqualTo()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryEqualTo()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryNotEqualTo()
        //{
        //}
        [TestMethod]
        public void TestBinaryBitwiseAND()
        {
            Expression x = new Expression("35&12");
            Assert.AreEqual(x.Solve().Value, 35 & 12);
        }
        [TestMethod]
        public void TestBinaryBitwiseXOR()
        {
            Expression x = new Expression("35^12");
            Assert.AreEqual(x.Solve().Value, 35 ^ 12);
        }
        [TestMethod]
        public void TestBinaryBitwiseOR()
        {
            Expression x = new Expression("35|12");
            Assert.AreEqual(x.Solve().Value, 35 | 12);
        }
        //Boolean problem again 
        //[TestMethod]
        //public void TestBinaryBooleanAND()
        //{
        //}
        //[TestMethod]
        //public void TestBinaryBooleanOR()
        //{
        //}
        [TestMethod]
        public void TestParenthesisHandling()
        {
            Expression x = new Expression("((5 + 2) + (2 + (3 + 7) + 8) + 5 + ( 9 + ( 9 + 8 + ( 5 + 2) + 3)) + 1)");
            Assert.AreEqual(x.Solve().Value, 69);
        }
    }
}
