using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class HashTableTest
    {
        [TestMethod]
        public void TestTableAdd()
        {
            HashTable<int> x = new HashTable<int>(500);
            Assert.AreEqual(x.Add("a", 5), true);
        }
        [TestMethod]
        public void TestTableGet()
        {
            HashTable<int> x = new HashTable<int>(500);
            bool yes = x.Add("a", 5);
            Assert.AreEqual(x.Get("a"), 5);
        }
        [TestMethod]
        public void TestTableRemove()
        {
            HashTable<int> x = new HashTable<int>(500);
            bool yes = x.Add("a", 5);
            Assert.AreEqual(x.Remove("a"), 5);
        }
        [TestMethod]
        public void TestTableGetLength()
        {
            HashTable<int> x = new HashTable<int>(500);
            bool yes = x.Add("a", 5);
            Assert.AreEqual(x.GetLength(), 1);
        }
    }
}
