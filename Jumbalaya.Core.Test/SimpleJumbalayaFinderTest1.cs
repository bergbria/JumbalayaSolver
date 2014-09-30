using System;
using System.Linq;
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
            var jumbalayas = finder.FindJumbalayas(board).Distinct().ToArray();

            //CollectionAssert.Contains(jumbalayas, "sprints");
            //CollectionAssert.AllItemsAreUnique(jumbalayas);
        }
    }
}
