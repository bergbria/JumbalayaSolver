using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumbalaya.Core
{
    public class FancyWordIndex : IWordIndex
    {
        private Dictionary<string, List<string>> anagramIndex;
        private List<string> validAnagrams;

        public FancyWordIndex()
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

        private static string Merge(LinkedList<char> list1, LinkedList<char> list2)
        {
            List<LinkedListNode<char>> insertedNodes = new List<LinkedListNode<char>>();

            LinkedListNode<char> node1 = list1.First;
            LinkedListNode<char> node2 = list2.First;

            while (node1 != null && node2 != null)
            {
                if (node2.Value < node1.Value)
                {
                    var insertedNode = list1.AddBefore(node1, node2.Value);
                    insertedNodes.Add(insertedNode);
                    node2 = node2.Next;
                }
                else
                {
                    node1 = node1.Next;
                }
            }

            if (node2 != null)
            {
                while (node2 != null)
                {
                    insertedNodes.Add(list1.AddLast(node2.Value));
                    node2 = node2.Next;
                }
            }

            StringBuilder strBuilder = new StringBuilder(list1.Count);
            foreach (char c in list1)
            {
                strBuilder.Append(c);
            }

            foreach (var node in insertedNodes)
            {
                list1.Remove(node);
            }
            return strBuilder.ToString();
        }

        private static ISet<string> FindAnagramOptions(string word, TileTray tray)
        {
            var optionAnagrams = new List<string>();
            word = GetSortedWord(word);
            optionAnagrams.Add(word);

            var charList = new LinkedList<char>(word.ToCharArray());
            var sortedTiles = tray.Tiles.Select(tile => GetSortedLinkedList(tile.Text));
            LinkedListNode<char> currentNode;
            LinkedListNode<char> precedingNode;

            foreach (var tile in sortedTiles)
            {
                //add 1
                optionAnagrams.Add(Merge(charList, tile));

                //swap 1
                currentNode = charList.First;
                precedingNode = null;
                while (currentNode != null)
                {
                    if (precedingNode == null || precedingNode.Value != currentNode.Value)
                    {
                        charList.Remove(currentNode);
                        string anagram = Merge(charList, tile);
                        optionAnagrams.Add(anagram);

                        if (precedingNode == null)
                        {
                            charList.AddFirst(currentNode);
                        }
                        else
                        {
                            charList.AddAfter(precedingNode, currentNode);
                        }
                    }
                    precedingNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }

            if (tray.SortedDistinctLetterPairs.Any())
            {
                foreach (var pairText in tray.SortedDistinctLetterPairs)
                {
                    //add 2
                    optionAnagrams.Add(Merge(charList, pairText));
                }

                currentNode = charList.First;
                precedingNode = null;

                while (currentNode != null)
                {
                    LinkedListNode<char> fancyNode = currentNode.Next;
                    LinkedListNode<char> fancyPrecedingNode = precedingNode;
                    charList.Remove(currentNode);

                    //add 1, swap 1 (=remove 1, add 2)
                    foreach (var pairText in tray.SortedDistinctLetterPairs)
                    {
                        //add 1, swap 1 (=remove 1, add 2)
                        optionAnagrams.Add(Merge(charList, pairText));
                    }

                    while (fancyNode != null)
                    {
                        charList.Remove(fancyNode);
                        foreach (var pairText in tray.SortedDistinctLetterPairs)
                        {
                            //swap 2
                            optionAnagrams.Add(Merge(charList, pairText));
                        }

                        if (fancyPrecedingNode == null)
                        {
                            charList.AddFirst(fancyNode);
                        }
                        else
                        {
                            charList.AddAfter(fancyPrecedingNode, fancyNode);
                        }
                        fancyPrecedingNode = fancyNode;
                        fancyNode = fancyNode.Next;
                    }

                    if (precedingNode == null)
                    {
                        charList.AddFirst(currentNode);
                    }
                    else
                    {
                        charList.AddAfter(precedingNode, currentNode);
                    }

                    precedingNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }
            return new HashSet<string>(optionAnagrams);
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
            string sortedWord = new string(chars);
            return sortedWord;
        }

        private static LinkedList<char> GetSortedLinkedList(string str)
        {
            var chars = str.ToCharArray();
            Array.Sort(chars);
            return new LinkedList<char>(chars);
        }
    }
}