using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jumbalaya.Core.Test
{
    [TestClass]
    public class WordCharacterDataUnitTest
    {
        [TestMethod]
        public void EqualityTest1()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("test");

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void EqualityTest2()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("ttes");

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void EqualityTest3()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("tes");

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void EqualityTest4()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("testt");

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void EqualityTest5()
        {
            WordCharacterData a = new WordCharacterData("jill");
            WordCharacterData b = new WordCharacterData("bob");

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void DictionaryEqualityTest1()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("test");

            var dict = new Dictionary<WordCharacterData, int>
            {
                {a, 1}
            };
            Assert.IsTrue(dict.ContainsKey(b));
        }

        [TestMethod]
        public void DictionaryEqualityTest2()
        {
            WordCharacterData a = new WordCharacterData("test");
            WordCharacterData b = new WordCharacterData("tst");

            var dict = new Dictionary<WordCharacterData, int>
            {
                {a, 1}
            };
            Assert.IsFalse(dict.ContainsKey(b));
        }
    }
}
