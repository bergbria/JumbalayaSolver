using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public class SimpleWordIndex : IWordIndex
    {
        private Dictionary<WordCharacterData, List<string>> anagramIndex;

        public SimpleWordIndex()
        {
            anagramIndex = new Dictionary<WordCharacterData, List<string>>();
        }

        public void AddWord(string word)
        {
            var charData = new WordCharacterData(word);
            List<string> wordMatches;
            if (anagramIndex.TryGetValue(charData, out wordMatches))
            {
                wordMatches.Add(word);
            }
            else
            {
                anagramIndex.Add(charData, new List<string> { word });
            }
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

            RemoveMultiCharacterTileViolations(word, wordOptions, tray);
            return wordOptions.ToList();
        }

        private void RemoveMultiCharacterTileViolations(string originalWord, HashSet<string> wordOptions, TileTray tray)
        {
            var multiCharTiles = tray.Tiles.Where(tile => tile.Text.Length > 1);

        }

        private static ISet<WordCharacterData> FindAnagramOptions(string word, TileTray tray)
        {
            var optionAnagrams = new HashSet<WordCharacterData>();
            optionAnagrams.Add(new WordCharacterData(word));

            foreach (var tile in tray.Tiles)
            {
                //add 1
                optionAnagrams.Add(new WordCharacterData(word + tile.Text));

                //swap 1
                for (int i = 0; i < word.Length; i++)
                {
                    optionAnagrams.Add(new WordCharacterData(word.Remove(i, 1) + tile.Text));
                }
            }

            var tilePairStrs = tray.DistinctTilePairStrings;
            foreach (var pairText in tilePairStrs)
            {
                //add 2
                optionAnagrams.Add(new WordCharacterData(word + pairText));
            }

            for (int i = 0; i < word.Length; i++)
            {
                string sansLetter1 = word.Remove(i, 1);

                foreach (var pairText in tilePairStrs)
                {
                    //add 1, swap 1 (=remove 1, add 2)
                    optionAnagrams.Add(new WordCharacterData(sansLetter1 + pairText));
                }

                for (int j = i; j < sansLetter1.Length; j++)
                {
                    string sansBothLetters = sansLetter1.Remove(j, 1);
                    foreach (var pairText in tilePairStrs)
                    {
                        //swap 2
                        optionAnagrams.Add(new WordCharacterData(sansBothLetters + pairText));
                    }
                }
            }
            return optionAnagrams;
        }
    }
}
