using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void TestArrayListInsert()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            Assert.AreEqual(x.Insert(2, 5), false);
            Assert.AreEqual(x.Insert(0, 5), true);
        }
        [TestMethod]
        public void TestArrayListRemove()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            Assert.AreEqual(x.Insert(2, 5), false);
            bool success = x.Insert(0, 5);
            Assert.AreEqual(x.Remove(0), true);
        }
        [TestMethod]
        public void TestArrayListSet()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            Assert.AreEqual(x.SetItem(0, 5), false);
            x.Insert(0, 1);
            Assert.AreEqual(x.SetItem(0, 5), true);
        }
        [TestMethod]
        public void TestArrayListGet()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            Assert.ThrowsException<InvalidPositionException>(() => x.GetItem(0));
            x.Insert(0, 1);
            Assert.AreEqual(x.GetItem(0), 1);
        }
        [TestMethod]
        public void TestArrayListSize()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            Assert.AreEqual(x.Size(), 0);
            x.Insert(0, 5);
            Assert.AreEqual(x.Size(), 1);
        }
        [TestMethod]
        public void TestArrayListClear()
        {
            ArrayList<int> x = new ArrayList<int>(1);
            x.Insert(0, 5);
            x.Clear();
            Assert.AreEqual(x.Size(), 0);
        }
    }

    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void TestLinkedListInsert()
        {
            LinkedList<int> x = new LinkedList<int>();
            Assert.AreEqual(x.Insert(2, 5), false);
            Assert.AreEqual(x.Insert(0, 5), true);
        }
        [TestMethod]
        public void TestLinkedListRemove()
        {
            LinkedList<int> x = new LinkedList<int>();
            Assert.AreEqual(x.Insert(2, 5), false);
            bool success = x.Insert(0, 5);
            Assert.AreEqual(x.Remove(0), true);
        }
        [TestMethod]
        public void TestLinkedListSet()
        {
            LinkedList<int> x = new LinkedList<int>();
            Assert.AreEqual(x.SetItem(0, 5), false);
            x.Insert(0, 1);
            Assert.AreEqual(x.SetItem(0, 5), true);
        }
        [TestMethod]
        public void TestLinkedListGet()
        {
            LinkedList<int> x = new LinkedList<int>();
            Assert.ThrowsException<InvalidPositionException>(() => x.GetItem(0));
            x.Insert(0, 1);
            Assert.AreEqual(x.GetItem(0), 1);
        }
        [TestMethod]
        public void TestLinkedListSize()
        {
            LinkedList<int> x = new LinkedList<int>();
            Assert.AreEqual(x.Size(), 0);
            x.Insert(0, 5);
            Assert.AreEqual(x.Size(), 1);
        }
        [TestMethod]
        public void TestLinkedListClear()
        {
            LinkedList<int> x = new LinkedList<int>();
            x.Insert(0, 5);
            x.Clear();
            Assert.AreEqual(x.Size(), 0);
        }
    }
}
