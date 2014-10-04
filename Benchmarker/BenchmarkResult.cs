using System;

namespace Benchmarker
{
    class BenchmarkResult
    {
        public Benchmark Benchmark { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public object ReturnValue { get; set; }
    }
}
