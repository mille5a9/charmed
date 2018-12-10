using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void TestMinHeapInsert()
        {
            MinHeap<int> x = new MinHeap<int>();
            bool yes = x.Insert(5, x.GetRoot());
            Assert.AreEqual(yes, true);
        }
        [TestMethod]
        public void TestMinHeapContains()
        {
            MinHeap<int> x = new MinHeap<int>();
            bool yes = x.Insert(5, x.GetRoot());
            Assert.AreEqual(x.Contains(5, x.GetRoot()), true);
            Assert.AreEqual(x.Contains(4, x.GetRoot()), false);
        }
        [TestMethod]
        public void TestMinHeapExtract()
        {
            MinHeap<int> x = new MinHeap<int>();
            bool yes = x.Insert(5, x.GetRoot());
            yes = x.Insert(2, x.GetRoot());
            Assert.AreEqual(2, x.Extract(x.GetRoot()));
            Assert.AreEqual(5, x.Extract(x.GetRoot()));
        }
    }

    [TestClass]
    public class MaxHeapTest
    {
        [TestMethod]
        public void TestMaxHeapInsert()
        {
            MaxHeap<int> x = new MaxHeap<int>();
            bool yes = x.Insert(5, x.GetRoot());
            Assert.AreEqual(yes, true);
        }
        [TestMethod]
        public void TestMaxHeapContains()
        {
            MaxHeap<int> x = new MaxHeap<int>();
            bool yes = x.Insert(5, x.GetRoot());
            Assert.AreEqual(x.Contains(5, x.GetRoot()), true);
            Assert.AreEqual(x.Contains(4, x.GetRoot()), false);
        }
        [TestMethod]
        public void TestMaxHeapExtract()
        {
            MaxHeap<int> x = new MaxHeap<int>();
            bool yes = x.Insert(2, x.GetRoot());
            yes = x.Insert(5, x.GetRoot());
            Assert.AreEqual(5, x.Extract(x.GetRoot()));
            Assert.AreEqual(2, x.Extract(x.GetRoot()));
        }
    }
}
