using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void TestGraphAddVertex()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            Assert.AreEqual(yes, true);
        }
        [TestMethod]
        public void TestGraphHasVertex()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            Assert.AreEqual(x.HasVertex(5), true);
            Assert.AreEqual(x.HasVertex(4), false);
        }
        [TestMethod]
        public void TestGraphRemoveVertex()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            Assert.AreEqual(x.RemoveVertex(5), true);
            Assert.AreEqual(x.RemoveVertex(4), false);
        }
        [TestMethod]
        public void TestGraphAddEdge()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            yes = x.AddVertex(4);
            Assert.AreEqual(x.AddEdge(4, 5), true);
        }
        [TestMethod]
        public void TestGraphHasEdge()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            yes = x.AddVertex(4);
            yes = x.AddEdge(4, 5);
            Assert.AreEqual(x.HasEdge(4, 5), true);
            Assert.AreEqual(x.HasEdge(5, 4), false);
        }
        [TestMethod]
        public void TestGraphRemoveEdge()
        {
            Graph<int> x = new Graph<int>();
            bool yes = x.AddVertex(5);
            yes = x.AddVertex(4);
            yes = x.AddEdge(4, 5);
            Assert.AreEqual(x.RemoveEdge(4, 5), true);
            Assert.AreEqual(x.RemoveEdge(5, 4), false);
        }
    }
}
