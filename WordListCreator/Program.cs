using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandLine.Text;

namespace WordListCreator
{
    class Program
    {
        //don't allow any spaces, punctuation, or uppercase words
        private static readonly Regex ValidWordLineRegex = new Regex(@"^[a-z]+$");

        //Usage <exe name> <input word list> <destination file>
        static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine(HelpText.AutoBuild(options));
            }

            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine("Could not find input file");
            }


            using (var outputStream = new StreamWriter(options.OutputFile, false))
            {
                foreach (var line in File.ReadLines(options.InputFile))
                {
                    if (ValidWordLineRegex.IsMatch(line))
                    {
                        outputStream.WriteLine(line);
                    }
                }
            }
        }
    }
}
