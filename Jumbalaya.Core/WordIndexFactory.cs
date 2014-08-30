using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumbalaya.Core
{
    public static class WordIndexFactory
    {
        public static void FillWordIndex(IWordIndex index, string wordListPath)
        {
            foreach (var word in File.ReadLines(wordListPath))
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    index.AddWord(word);
                }
            }
        }
    }
}
