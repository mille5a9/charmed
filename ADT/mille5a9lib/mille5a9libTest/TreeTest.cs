using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mille5a9lib;

namespace mille5a9libTest
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestTreeInsert()
        {
            AVLTree<int> x = new AVLTree<int>();
            bool yes = x.Insert(2);
            Assert.AreEqual(yes, true);
        }
        [TestMethod]
        public void TestTreeContains()
        {
            AVLTree<int> x = new AVLTree<int>();
            bool yes = x.Insert(2);
            Assert.AreEqual(x.Contains(2), true);
        }
        [TestMethod]
        public void TestTreeRemove()
        {
            AVLTree<int> x = new AVLTree<int>();
            bool yes = x.Insert(2);
            Assert.AreEqual(x.Remove(2), true);
        }
        [TestMethod]
        public void TestTreeSize()
        {
            AVLTree<int> x = new AVLTree<int>();
            bool yes = x.Insert(2);
            yes = x.Insert(4);
            yes = x.Insert(6);
            yes = x.Insert(8);
            Assert.AreEqual(x.Size(x.GetRoot()), 4);
        }
    }
}
