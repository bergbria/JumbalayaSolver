using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jumbalaya.Core;

namespace Benchmarker
{
    class Program
    {
        private static SimpleWordIndex simpleWordIndex;
        private static FancyWordIndex fancyWordIndex;

        static void Main(string[] args)
        {
            string wordListPath = @"..\..\..\Resources\wordsEn_filtered.txt";
            BenchmarkManager benchmarker = new BenchmarkManager(2);

            //Console.WriteLine("Initializing benchmark resources...");
            //var simpleWordIndex = new SimpleWordIndex();

            benchmarker.AddBenchmark("Initialize SimpleWordIndex", () =>
            {
                simpleWordIndex = new SimpleWordIndex();
                WordIndexFactory.FillWordIndex(simpleWordIndex, wordListPath);
                //GenericWordIndexTests<SimpleWordIndex> tester = new GenericWordIndexTests<SimpleWordIndex>();
                //tester.InstructionBookTest();
            });

            benchmarker.AddBenchmark("SimpleWordIndex - Find Moves", () =>
            {
                string word = "aeprs";
                TileTray tray = new TileTray("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
                simpleWordIndex.FindAvailableMoves(word, tray);
            });

            benchmarker.AddBenchmark("Initialize FancyWordIndex", () =>
            {
                fancyWordIndex = new FancyWordIndex();
                WordIndexFactory.FillWordIndex(fancyWordIndex, wordListPath);
                //GenericWordIndexTests<SimpleWordIndex> tester = new GenericWordIndexTests<SimpleWordIndex>();
                //tester.InstructionBookTest();
            });

            benchmarker.AddBenchmark("FancyWordIndex - Find Moves", () =>
            {
                string word = "aeprs";
                TileTray tray = new TileTray("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
                fancyWordIndex.FindAvailableMoves(word, tray);
            });

            var benchmarkResults = benchmarker.RunBenchmarks();

            Console.WriteLine("\n-----Benchmark Results-----");
            foreach (var benchmarkResult in benchmarkResults)
            {
                Console.WriteLine("{0}: {1} seconds", benchmarkResult.Benchmark.Name, benchmarkResult.ExecutionTime.TotalSeconds);
            }
            Console.WriteLine();
        }
    }
}
