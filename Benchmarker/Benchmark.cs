using System;

namespace Benchmarker
{
    class Benchmark
    {
        public string Name { get; set; }

        public Action Action { get; set; }
    }
}
