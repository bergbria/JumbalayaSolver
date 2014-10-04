using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumbalaya.Core
{
    public class SlightlyLessFancyWordIndex : IWordIndex
    {
        private Dictionary<string, List<string>> anagramIndex;
        private List<string> validAnagrams;

        public SlightlyLessFancyWordIndex()
        {
            anagramIndex = new Dictionary<string, List<string>>();
            validAnagrams = new List<string>();
        }

        public List<string> FindAvailableMoves(string word, TileTray tray)
        {
            var optionAnagrams = FindAnagramOptions(word, tray);

            var wordOptions = new HashSet<string>();
            foreach (var optionAnagram in optionAnagrams)
            {
                List<string> options;
                if (anagramIndex.TryGetValue(optionAnagram, out options))
                {
                    wordOptions.UnionWith(options);
                }
            }
            //leaving the word intact doesn't count as a move
            wordOptions.Remove(word);
            return wordOptions.ToList();
        }

        private static string MergeAndSort(string str1, string str2)
        {
            string unsorted = str1 + str2;
            char[] arr = unsorted.ToCharArray();
            Array.Sort(arr);

            return new string(arr);
        }

        private static ISet<string> FindAnagramOptions(string word, TileTray tray)
        {
            var optionAnagrams = new HashSet<string>();
            word = GetSortedWord(word);
            optionAnagrams.Add(word);

            foreach (var tile in tray.Tiles)
            {
                //add 1
                optionAnagrams.Add(MergeAndSort(word, tile.Text));

                //swap 1
                for (int i = 0; i < word.Length; i++)
                {
                    optionAnagrams.Add(MergeAndSort(word.Remove(i, 1), tile.Text));
                }
            }

            var tilePairStrs = tray.DistinctTilePairStrings;
            foreach (var pairText in tilePairStrs)
            {
                //add 2
                optionAnagrams.Add(MergeAndSort(word, pairText));
            }

            for (int i = 0; i < word.Length; i++)
            {
                string sansLetter1 = word.Remove(i, 1);

                foreach (var pairText in tilePairStrs)
                {
                    //add 1, swap 1 (=remove 1, add 2)
                    optionAnagrams.Add(MergeAndSort(sansLetter1, pairText));
                }

                for (int j = i; j < sansLetter1.Length; j++)
                {
                    string sansBothLetters = sansLetter1.Remove(j, 1);
                    foreach (var pairText in tilePairStrs)
                    {
                        //swap 2
                        optionAnagrams.Add(MergeAndSort(sansBothLetters, pairText));
                    }
                }
            }
            return optionAnagrams;
        }

        public void AddWord(string word)
        {
            var sortedWord = GetSortedWord(word);
            List<string> wordMatches;
            if (anagramIndex.TryGetValue(sortedWord, out wordMatches))
            {
                wordMatches.Add(word);
            }
            else
            {
                validAnagrams.Add(sortedWord);
                anagramIndex.Add(sortedWord, new List<string> { word });
            }
        }

        private static string GetSortedWord(string word)
        {
            var chars = word.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        private static LinkedList<char> GetSortedLinkedList(string str)
        {
            var chars = str.ToCharArray();
            Array.Sort(chars);
            return new LinkedList<char>(chars);
        }

    }
}