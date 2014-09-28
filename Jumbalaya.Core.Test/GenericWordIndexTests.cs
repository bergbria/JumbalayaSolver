using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    public class GenericWordIndexTests<T> where T : IWordIndex
    {
        private IWordIndex index;

        public void Setup()
        {
            InstantiateWordIndex();

            PopulateWordIndex();
        }

        private void PopulateWordIndex()
        {
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

        private void InstantiateWordIndex()
        {
            Type wordIndexType = typeof (T);
            ConstructorInfo defaultConstructor = wordIndexType.GetConstructor(Type.EmptyTypes);
            index = (IWordIndex) defaultConstructor.Invoke(Type.EmptyTypes);
        }

        public void TestAdd1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = index.FindAvailableMoves("test", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

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

        public void TestRearrange()
        {
            var tray = new TileTray("x");
            var options = index.FindAvailableMoves("tset", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "test");
        }

        public void TestSwap1()
        {
            TileTray tray = new TileTray("a", "b", "d");
            var options = index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(3, options.Count);
            CollectionAssert.Contains(options, "testa");
            CollectionAssert.Contains(options, "atest");
            CollectionAssert.Contains(options, "testb");
        }

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

        public void TestSwap2()
        {
            TileTray tray = new TileTray("e", "e");
            var options = index.FindAvailableMoves("xtestx", tray);

            Assert.AreEqual(1, options.Count);
            CollectionAssert.Contains(options, "eteste");
        }

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

        public void TestMultiCharTile()
        {
            index.AddWord("bat");
            index.AddWord("tab");
            TileTray tray = new TileTray("b", "at");
            var options = index.FindAvailableMoves("", tray);
            CollectionAssert.Contains(options, "bat");
            CollectionAssert.DoesNotContain(options, "tab");
        }
    }
}
