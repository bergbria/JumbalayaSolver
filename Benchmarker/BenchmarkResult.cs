using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarker
{
    class BenchmarkResult
    {
        public Benchmark Benchmark { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public object ReturnValue { get; set; }
    }
}
