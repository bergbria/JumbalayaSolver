using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    [TestClass]
    public class SimpleWordIndexTest1
    {
        private SimpleWordIndex index;

        [TestInitialize]
        public void Setup()
        {
            index = new SimpleWordIndex();
            index.AddWord("test");
            index.AddWord("testa");
            index.AddWord("atest");
            index.AddWord("testb");
            index.AddWord("testc");

            index.AddWord("testaa");
            index.AddWord("atesta");
            index.AddWord("testaaa");
            index.AddWord("testbb");

            index.AddWord("eteste");
        }

        [TestMethod]
        public void TestAdd1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = index.FindAvailableMoves("test", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

        [TestMethod]
        public void TestAdd2()
        {
            TileTray tray = new TileTray("a", "a", "b", "d");
            var options = index.FindAvailableMoves("test", tray);

            Assert.AreEqual(5, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");

            CollectionAssert.Contains(options, "testaa");
            CollectionAssert.Contains(options, "atesta");
        }

        [TestMethod]
        public void TestRearrange()
        {
            var tray = new TileTray("x");
            var options = index.FindAvailableMoves("tset", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "test");
        }

        [TestMethod]
        public void TestSwap1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

        [TestMethod]
        public void TestAdd1Swap1()
        {
            TileTray tray = new TileTray("a", "a");
            var options = index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(4, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "atesta");
            CollectionAssert.Contains(options, "testaa");
        }

        [TestMethod]
        public void TestSwap2()
        {
            TileTray tray = new TileTray("e", "e");
            var options = index.FindAvailableMoves("xtestx", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "eteste");
        }

        [TestMethod]
        //verifies that, using a real word list, the index can provide the answers in the jumbalaya instruction book
        public void InstructionBookTest()
        {
            SimpleWordIndex dexter = new SimpleWordIndex();
            WordIndexFactory.FillWordIndex(dexter, @"..\..\..\Resources\wordsEn_filtered.txt");

            TileTray tray = new TileTray("f", "s", "r", "k");
            var options = dexter.FindAvailableMoves("act", tray);
            CollectionAssert.Contains(options, "cat");
            CollectionAssert.Contains(options, "cat");
            CollectionAssert.Contains(options, "facts");
            CollectionAssert.Contains(options, "track");

            tray = new TileTray("s", "t");
            options = dexter.FindAvailableMoves("bear", tray);
            CollectionAssert.Contains(options, "star");

            tray = new TileTray("o", "e", "c", "h");
            options = dexter.FindAvailableMoves("star", tray);
            CollectionAssert.Contains(options, "rose");
            CollectionAssert.Contains(options, "chats");
        }
    }
}
