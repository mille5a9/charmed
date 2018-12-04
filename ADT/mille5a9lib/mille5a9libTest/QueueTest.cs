using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class ArrayQueueTest
    {
        [TestMethod]
        public void TestArrayQueueEnqueue()
        {
            ArrayQueue<int> x = new ArrayQueue<int>(1);
            Assert.AreEqual(x.Enqueue(5), true);
            Assert.AreEqual(x.Enqueue(10), false);
        }
        [TestMethod]
        public void TestArrayQueueDequeue()
        {
            ArrayQueue<int> x = new ArrayQueue<int>(1);
            Assert.ThrowsException<StackOverflowException>(() => x.Dequeue());
            bool success = x.Enqueue(5);
            Assert.AreEqual(5, x.Dequeue());
        }
        [TestMethod]
        public void TestArrayQueuePeek()
        {
            ArrayQueue<int> x = new ArrayQueue<int>(1);
            Assert.ThrowsException<StackOverflowException>(() => x.Peek());
            bool success = x.Enqueue(5);
            Assert.AreEqual(5, x.Peek());
        }
        [TestMethod]
        public void TestArrayQueueSize()
        {
            ArrayQueue<int> x = new ArrayQueue<int>(1);
            bool success = x.Enqueue(5);
            Assert.AreEqual(1, x.Size());
        }
    }

    [TestClass]
    public class LinkedQueueTest
    {
        [TestMethod]
        public void TestLinkedQueueEnqueue()
        {
            LinkedQueue<int> x = new LinkedQueue<int>();
            Assert.AreEqual(x.Enqueue(5), true);
        }
        [TestMethod]
        public void TestLinkedQueueDequeue()
        {
            LinkedQueue<int> x = new LinkedQueue<int>();
            Assert.ThrowsException<StackOverflowException>(() => x.Dequeue());
            bool success = x.Enqueue(5);
            Assert.AreEqual(5, x.Dequeue());
        }
        [TestMethod]
        public void TestLinkedQueuePeek()
        {
            LinkedQueue<int> x = new LinkedQueue<int>();
            Assert.ThrowsException<StackOverflowException>(() => x.Peek());
            bool success = x.Enqueue(5);
            Assert.AreEqual(5, x.Peek());
        }
        [TestMethod]
        public void TestLinkedQueueSize()
        {
            LinkedQueue<int> x = new LinkedQueue<int>();
            bool success = x.Enqueue(5);
            Assert.AreEqual(1, x.Size());
        }
    }
}
