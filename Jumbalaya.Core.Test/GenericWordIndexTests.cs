using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    public class GenericWordIndexTests<T> where T : IWordIndex
    {
        private IWordIndex _index;

        public void Setup()
        {
            _index = InstantiateWordIndex();

            PopulateWordIndex();
        }

        private void PopulateWordIndex()
        {
            _index.AddWord("test");
            _index.AddWord("testa");
            _index.AddWord("atest");
            _index.AddWord("testb");
            _index.AddWord("testc");

            _index.AddWord("testaa");
            _index.AddWord("atesta");
            _index.AddWord("testaaa");
            _index.AddWord("testbb");

            _index.AddWord("eteste");
        }

        private IWordIndex InstantiateWordIndex()
        {
            Type wordIndexType = typeof (T);
            ConstructorInfo defaultConstructor = wordIndexType.GetConstructor(Type.EmptyTypes);
            return (IWordIndex) defaultConstructor.Invoke(Type.EmptyTypes);
        }

        public void TestAdd1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = _index.FindAvailableMoves("test", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

        public void TestAdd2()
        {
            TileTray tray = new TileTray("a", "a", "b", "d");
            var options = _index.FindAvailableMoves("test", tray);

            Assert.AreEqual(5, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");

            CollectionAssert.Contains(options, "testaa");
            CollectionAssert.Contains(options, "atesta");
        }

        public void TestRearrange()
        {
            var tray = new TileTray("x");
            var options = _index.FindAvailableMoves("tset", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "test");
        }

        public void TestSwap1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = _index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

        public void TestAdd1Swap1()
        {
            TileTray tray = new TileTray("a", "a");
            var options = _index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(4, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "atesta");
            CollectionAssert.Contains(options, "testaa");
        }

        public void TestSwap2()
        {
            TileTray tray = new TileTray("e", "e");
            var options = _index.FindAvailableMoves("xtestx", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "eteste");
        }

        //verifies that, using a real word list, the index can provide the answers in the jumbalaya instruction book
        public void InstructionBookTest()
        {
            IWordIndex dexter = InstantiateWordIndex();
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

        public void TestMultiCharTile()
        {
            _index.AddWord("bat");
            _index.AddWord("tab");
            TileTray tray = new TileTray("b", "at");
            var options = _index.FindAvailableMoves("", tray);
            CollectionAssert.Contains(options, "bat");
            CollectionAssert.DoesNotContain(options, "tab");
        }
    }
}
