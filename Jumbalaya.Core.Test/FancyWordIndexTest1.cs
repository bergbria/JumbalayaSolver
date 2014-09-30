using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    [TestClass]
    public class FancyWordIndexTest1
    {
        private GenericWordIndexTests<FancyWordIndex> _tester;

        [TestInitialize]
        public void Setup()
        {
            _tester = new GenericWordIndexTests<FancyWordIndex>();
            _tester.Setup();
        }

        [TestMethod]
        public void TestAdd1()
        {
            _tester.TestAdd1();
        }

        [TestMethod]
        public void TestAdd2()
        {
            _tester.TestAdd2();
        }

        [TestMethod]
        public void TestRearrange()
        {
            _tester.TestRearrange();
        }

        [TestMethod]
        public void TestSwap1()
        {
            _tester.TestSwap1();
        }

        [TestMethod]
        public void TestAdd1Swap1()
        {
            _tester.TestAdd1Swap1();
        }

        [TestMethod]
        public void TestSwap2()
        {
           _tester.TestSwap2();
        }

        [TestMethod]
        //verifies that, using a real word list, the index can provide the answers in the jumbalaya instruction book
        public void InstructionBookTest()
        {
            _tester.InstructionBookTest();
        }

        [TestMethod]
        public void TestMultiCharTile()
        {
            _tester.TestMultiCharTile();
        }
    }
}
