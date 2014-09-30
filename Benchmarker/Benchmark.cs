using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarker
{
    class Benchmark
    {
        public string Name { get; set; }

        public Action Action { get; set; }
    }
}
