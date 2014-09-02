using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public class SimpleJumabalayaFinder : IJumbalayaFinder
    {
        private const int minLettersForJumbalaya = 7;

        private List<string> validWords;

        public SimpleJumabalayaFinder(string wordListPath)
        {
            validWords = new List<string>();
            foreach (var word in File.ReadLines(wordListPath))
            {
                if (wordListPath.Length >= minLettersForJumbalaya)
                {
                    validWords.Add(word);
                }
            }
        }

        public List<string> FindJumbalayas(Board board)
        {
            List<IEnumerable<char>> distinctifiedWords = board.Words.Select(word => word.Distinct()).ToList();
            List<string> foundWords = new List<string>();
            FindJumbalayas(distinctifiedWords, 0, "", foundWords);
            return foundWords;
        }

        private void FindJumbalayas(List<IEnumerable<char>> boardWords, int currentWordIndex, string inProgressJumbalaya, List<string> foundWords)
        {
            if (currentWordIndex >= boardWords.Count || (inProgressJumbalaya.Length + boardWords.Count - currentWordIndex) < minLettersForJumbalaya)
            {
                return;
            }
            //find jumbalayas without this row in it
            FindJumbalayas(boardWords, currentWordIndex + 1, inProgressJumbalaya, foundWords);

            //find jumbalayas with each of my letters in it
            var startWord = boardWords[currentWordIndex];
            foreach (char c in startWord)
            {
                var prefix = inProgressJumbalaya + c;
                int binarySearchResult = validWords.BinarySearch(prefix);
                if (prefix.Length >= minLettersForJumbalaya && isValidWord(prefix, binarySearchResult))
                {
                    foundWords.Add(prefix);
                }
                if (isValidPrefix(prefix, binarySearchResult))
                {
                    FindJumbalayas(boardWords, currentWordIndex + 1, prefix, foundWords);
                }
            }
        }

        private bool isValidPrefix(string partialWord, int binarySearchResult)
        {
            int nextWordIndex = binarySearchResult >= 0 ? binarySearchResult + 1 : ~binarySearchResult;
            return nextWordIndex < validWords.Count && validWords[nextWordIndex].StartsWith(partialWord);
        }

        private bool isValidWord(string partialWord, int binarySearchResult)
        {
            return binarySearchResult >= 0;
        }

    }
}
