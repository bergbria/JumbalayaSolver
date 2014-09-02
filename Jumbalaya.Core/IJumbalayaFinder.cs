using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public interface IJumbalayaFinder
    {
        List<string> FindJumbalayas(Board board);
    }
}
