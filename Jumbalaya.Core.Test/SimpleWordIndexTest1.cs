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
            TileTray tray = new TileTray
            {
                Tiles = new[]
                {
                    new Tile("a"),
                    new Tile("b"),
                    new Tile("d"),
                }
            };
            var options = index.FindAvailableMoves("test", tray);

            Assert.AreEqual(3, options.Count);
            Assert.IsTrue(options.Contains("testa"));
            Assert.IsTrue(options.Contains("atest"));
            Assert.IsTrue(options.Contains("testb"));
        }

        [TestMethod]
        public void TestAdd2()
        {
            TileTray tray = new TileTray
            {
                Tiles = new[]
                {
                    new Tile("a"),
                    new Tile("a"),
                    new Tile("b"),
                    new Tile("d"),
                }
            };
            var options = index.FindAvailableMoves("test", tray);

            Assert.AreEqual(5, options.Count);
            Assert.IsTrue(options.Contains("testa"));
            Assert.IsTrue(options.Contains("atest"));
            Assert.IsTrue(options.Contains("testb"));
            
            Assert.IsTrue(options.Contains("testaa"));
            Assert.IsTrue(options.Contains("atesta"));
        }

        [TestMethod]
        public void TestSwap1()
        {
            TileTray tray = new TileTray
            {
                Tiles = new[]
                {
                    new Tile("a"),
                    new Tile("b"),
                    new Tile("d"),
                }
            };
            var options = index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(3, options.Count);
            Assert.IsTrue(options.Contains("testa"));
            Assert.IsTrue(options.Contains("atest"));
            Assert.IsTrue(options.Contains("testb"));
        }
    
        [TestMethod]
        public void TestAdd1Swap1()
        {
            TileTray tray = new TileTray
            {
                Tiles = new[]
                {
                    new Tile("a"),
                    new Tile("a"),
                }
            };
            var options = index.FindAvailableMoves("testx", tray);

            Assert.AreEqual(2, options.Count);
            Assert.IsTrue(options.Contains("testa"));
            Assert.IsTrue(options.Contains("atest"));
        }
    
        [TestMethod]
        public void TestSwap2()
        {
            TileTray tray = new TileTray
            {
                Tiles = new[]
                {
                    new Tile("e"),
                    new Tile("e"),
                }
            };
            var options = index.FindAvailableMoves("xtestx", tray);

            Assert.AreEqual(1, options.Count);
            Assert.IsTrue(options.Contains("eteste"));
        }
    
    }
}
