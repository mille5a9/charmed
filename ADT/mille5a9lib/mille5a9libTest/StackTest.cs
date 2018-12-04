using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class ArrayStackTest
    {
        [TestMethod]
        public void TestArrayStackPush()
        {
            ArrayStack<int> x = new ArrayStack<int>(3);
            bool success = x.Push(5);
            Assert.AreEqual(success, true);
            success = x.Push(10);
            success = x.Push(15);
            success = x.Push(20);
            Assert.AreEqual(success, false);
        }
        [TestMethod]
        public void TestArrayStackPop()
        {
            ArrayStack<int> x = new ArrayStack<int>(3);
            int output;
            x.Push(1);
            output = x.Pop();
            Assert.AreEqual(1, output);
            Assert.ThrowsException<StackOverflowException>(() => output = x.Pop());
        }
        [TestMethod]
        public void TestArrayStackPeek()
        {
            ArrayStack<int> x = new ArrayStack<int>(3);
            Assert.ThrowsException<StackOverflowException>(() => x.Peek());
            bool success = x.Push(5);
            Assert.AreEqual(5, x.Peek());
        }
        [TestMethod]
        public void TestArrayStackSize()
        {
            ArrayStack<int> x = new ArrayStack<int>(3);
            bool success = x.Push(5);
            Assert.AreEqual(1, x.Size());
        }
    }

    [TestClass]
    public class LinkedStackTest
    {
        [TestMethod]
        public void TestLinkedStackPush()
        {
            LinkedStack<int> x = new LinkedStack<int>();
            bool success = x.Push(5);
            Assert.AreEqual(success, true);
        }
        [TestMethod]
        public void TestLinkedStackPop()
        {
            LinkedStack<int> x = new LinkedStack<int>();
            int output;
            x.Push(1);
            output = x.Pop();
            Assert.AreEqual(1, output);
            Assert.ThrowsException<StackOverflowException>(() => output = x.Pop());
        }
        [TestMethod]
        public void TestLinkedStackPeek()
        {
            LinkedStack<int> x = new LinkedStack<int>();
            Assert.ThrowsException<StackOverflowException>(() => x.Peek());
            bool success = x.Push(5);
            Assert.AreEqual(5, x.Peek());
        }
        [TestMethod]
        public void TestLinkedStackSize()
        {
            LinkedStack<int> x = new LinkedStack<int>();
            bool success = x.Push(5);
            Assert.AreEqual(1, x.Size());
        }
    }
}
