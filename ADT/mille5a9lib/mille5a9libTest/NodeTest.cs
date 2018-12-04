using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class SingleNodeTest
    {
        [TestMethod]
        public void TestSingleNodeGet()
        {
            SingleNode<int> x = new SingleNode<int>(5, null);
            SingleNode<int> y = new SingleNode<int>(10, x);
            Assert.AreEqual(10, y.Get());
        }
        [TestMethod]
        public void TestSingleNodeGetNext()
        {
            SingleNode<int> x = new SingleNode<int>(5, null);
            SingleNode<int> y = new SingleNode<int>(10, x);
            Assert.AreEqual(x, y.GetNext());
        }
    }

    [TestClass]
    public class DoubleNodeTest
    {
        [TestMethod]
        public void TestDoubleNodeGet()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            Assert.AreEqual(5, x.Get());
        }
        [TestMethod]
        public void TestDoubleNodeGetNext()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            DoubleNode<int> y = new DoubleNode<int>(10, x, null);
            Assert.AreEqual(x, y.GetNext());
        }
        [TestMethod]
        public void TestDoubleNodeGetPrev()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            DoubleNode<int> y = new DoubleNode<int>(10, null, x);
            Assert.AreEqual(x, y.GetPrev());
        }
        [TestMethod]
        public void TestDoubleNodeSetItem()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            x.SetItem(10);
            Assert.AreEqual(10, x.Get());
        }
        [TestMethod]
        public void TestDoubleNodeSetNext()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            DoubleNode<int> y = new DoubleNode<int>(10, null, null);
            y.SetNext(x);
            Assert.AreEqual(x, y.GetNext());
        }
        [TestMethod]
        public void TestDoubleNodeSetPrev()
        {
            DoubleNode<int> x = new DoubleNode<int>(5, null, null);
            DoubleNode<int> y = new DoubleNode<int>(10, null, null);
            y.SetPrev(x);
            Assert.AreEqual(x, y.GetPrev());
        }
    }
    [TestClass]
    public class BinaryNodeTest
    {
        [TestMethod]
        public void TestBinaryNodeGet()
        {
            BinaryNode<int> x = new BinaryNode<int>(5);
            Assert.AreEqual(5, x.Get());
        }
        [TestMethod]
        public void TestBinaryNodeGetSetLeft()
        {
            BinaryNode<int> x = new BinaryNode<int>(5);
            BinaryNode<int> y = new BinaryNode<int>(10);
            x.SetLeft(y);
            Assert.AreEqual(y, x.GetLeft());
        }
        [TestMethod]
        public void TestBinaryNodeGetSetRight()
        {
            BinaryNode<int> x = new BinaryNode<int>(5);
            BinaryNode<int> y = new BinaryNode<int>(10);
            x.SetRight(y);
            Assert.AreEqual(y, x.GetRight());
        }
        [TestMethod]
        public void TestBinaryNodeSet()
        {
            BinaryNode<int> x = new BinaryNode<int>(5);
            x.Set(10);
            Assert.AreEqual(10, x.Get());
        }
    }
}
