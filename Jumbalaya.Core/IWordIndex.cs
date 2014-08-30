using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public interface IWordIndex
    {
        List<string> FindAvailableMoves(string word, TileTray tray);

        void AddWord(string word);
    }
}
