using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    [TestClass]
    public class SimpleWordIndexTest1
    {
        private SimpleWordIndex index;
        private GenericWordIndexTests<SimpleWordIndex> tester;

        [TestInitialize]
        public void Setup()
        {
            tester = new GenericWordIndexTests<SimpleWordIndex>();
            tester.Setup();
            index = new SimpleWordIndex();
        }

        [TestMethod]
        public void TestAdd1()
        {
            tester.TestAdd1();
        }

        [TestMethod]
        public void TestAdd2()
        {
            tester.TestAdd2();
        }

        [TestMethod]
        public void TestRearrange()
        {
            tester.TestRearrange();
        }

        [TestMethod]
        public void TestSwap1()
        {
            tester.TestSwap1();
        }

        [TestMethod]
        public void TestAdd1Swap1()
        {
            tester.TestAdd1Swap1();
        }

        [TestMethod]
        public void TestSwap2()
        {
           tester.TestSwap2();
        }

        [TestMethod]
        //verifies that, using a real word list, the index can provide the answers in the jumbalaya instruction book
        public void InstructionBookTest()
        {
            tester.InstructionBookTest();
        }

        [TestMethod]
        public void TestMultiCharTile()
        {
            tester.TestMultiCharTile();
        }
    }
}
