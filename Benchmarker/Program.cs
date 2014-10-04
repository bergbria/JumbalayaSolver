using System;
using Jumbalaya.Core;

namespace Benchmarker
{
    class Program
    {
        private static SimpleWordIndex _simpleWordIndex;
        private static FancyWordIndex _fancyWordIndex;
        private static SlightlyLessFancyWordIndex _slightlyLessFancyWordIndex;

        static void Main(string[] args)
        {
            string wordListPath = @"..\..\..\Resources\wordsEn_filtered.txt";
            BenchmarkManager benchmarker = new BenchmarkManager(50);

            //benchmarker.AddBenchmark("Initialize SimpleWordIndex", () =>
            //{
            //    _simpleWordIndex = new SimpleWordIndex();
            //    WordIndexFactory.FillWordIndex(_simpleWordIndex, wordListPath);
            //});

            //benchmarker.AddBenchmark("SimpleWordIndex - Find Moves", () =>
            //{
            //    string word = "aeprs";
            //    TileTray tray = new TileTray("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
            //    _simpleWordIndex.FindAvailableMoves(word, tray);
            //});

            benchmarker.AddBenchmark("Initialize FancyWordIndex", () =>
            {
                _fancyWordIndex = new FancyWordIndex();
                WordIndexFactory.FillWordIndex(_fancyWordIndex, wordListPath);
            });

            benchmarker.AddBenchmark("FancyWordIndex - Find Moves", () =>
            {
                string word = "aeprs";
                TileTray tray = new TileTray("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
                for (int i = 0; i < 100; i++)
                {
                    _fancyWordIndex.FindAvailableMoves(word, tray);
                }
            });

            //benchmarker.AddBenchmark("Initialize SlightlyLessFancyWordIndex", () =>
            //{
            //    _slightlyLessFancyWordIndex = new SlightlyLessFancyWordIndex();
            //    WordIndexFactory.FillWordIndex(_slightlyLessFancyWordIndex, wordListPath);
            //});

            //benchmarker.AddBenchmark("SlightlyLessFancyWordIndex - Find Moves", () =>
            //{
            //    string word = "aeprs";
            //    TileTray tray = new TileTray("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
            //    for (int i = 0; i < 100; i++)
            //    {
            //        _slightlyLessFancyWordIndex.FindAvailableMoves(word, tray);
            //    }
            //});

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
