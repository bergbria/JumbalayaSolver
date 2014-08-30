using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jumbalaya.Core;

namespace JumbalayaSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Usage: <exe name> <word on board> <list of tiles you have>
            //Example: JumbalayaSolver star a b qu r r
            //I currently hardcode a path to the word list because I'm lazy.
            
            //I'm too lazy for decent error handling right now
            if (args.Length < 2)
            {
                Console.WriteLine("Shenanigans! I want at least 1 word and 1 tile");
                return;
            }

            IWordIndex index = new SimpleWordIndex();
            WordIndexFactory.FillWordIndex(index, @"..\..\..\Resources\wordsEn_filtered.txt");
            string wordOnBoard = args[0];
            var tiles = args.ToList().GetRange(1, args.Length - 1);
            TileTray tray = new TileTray(tiles.ToArray());

            var options = index.FindAvailableMoves(wordOnBoard, tray);

            Console.WriteLine("Total Possible Moves: " + options.Count);
            var groups = options.GroupBy(option => option.Length).OrderByDescending(group => group.Key);
            
            foreach (var group in groups)
            {
                Console.WriteLine("\n-------Options with {0} letters", group.Key);
                foreach (var word in group)
                {
                    Console.WriteLine("\t" + word);
                }
            }
        }
    }
}
