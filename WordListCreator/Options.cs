using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace WordListCreator
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to the word list to process and filter", MetaValue = "<file path>")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Where to put the processed file", MetaValue = "<file path>")]
        public string OutputFile { get; set; }
    }
}
