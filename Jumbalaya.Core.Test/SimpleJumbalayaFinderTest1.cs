using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    [TestClass]
    public class SimpleJumbalayaFinderTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Board board = new Board
            {
                Words = new[]
                {
                    "sun",
                    "perfectly",
                    "aquifer",
                    "climb",
                    "thank",
                    "joyous",
                    "extra",
                    "changed",
                    "wise"
                }
            };
            SimpleJumabalayaFinder finder = new SimpleJumabalayaFinder(@"..\..\..\Resources\wordsEn_filtered.txt");
            var jumbalayas = finder.FindJumbalayas(board);

            CollectionAssert.Contains(jumbalayas, "sprints");
        }
    }
}
