using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Benchmarker
{
    class BenchmarkManager
    {
        public uint RepetitionCount { get; set; }
        private List<Benchmark> benchmarks;

        public BenchmarkManager(uint repetitionCount)
        {
            RepetitionCount = repetitionCount;
            benchmarks = new List<Benchmark>();
        }

        public void AddBenchmark(string name, Action action)
        {
            benchmarks.Add(new Benchmark { Name = name, Action = action });
        }

        public IEnumerable<BenchmarkResult> RunBenchmarks()
        {
            Console.WriteLine("Benchmarking...");
            var results = new List<BenchmarkResult>();
            Stopwatch stopwatch = new Stopwatch();
            foreach (var benchmark in benchmarks)
            {
                Console.WriteLine("{0}...", benchmark.Name);
                stopwatch.Restart();
                for (int i = 0; i < RepetitionCount; i++)
                {
                    benchmark.Action();
                }
                long averageExecutionTimeTicks = stopwatch.Elapsed.Ticks / RepetitionCount;
                results.Add(new BenchmarkResult { Benchmark = benchmark, ExecutionTime = TimeSpan.FromTicks(averageExecutionTimeTicks) });
            }
            return results;
        }
    }
}
