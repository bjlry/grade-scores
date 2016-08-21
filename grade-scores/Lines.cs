using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace grade_scores
{
    public class Lines
    {
        public string InputPath { get; private set; }
        public string InputFileName { get; private set; }
        private List<Line> _data = new List<Line>();

        public bool Process(string input)
        {
            try
            {
                if (!ReadFile(input)) return false;
                SortList();
                if (!WriteFile(InputPath + @"\" + GetOuputFileName())) return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                return false;
            }
            return true;
        }

        public bool ReadPath(string inputValue)
        {
            try
            {
                InputFileName = Path.GetFileName(inputValue);
                InputPath = Path.GetDirectoryName(inputValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to read file " + inputValue);
                Program.ShowHelp();
                return false;
            }
            return true;
        }

        private bool ReadFile(string file)
        {
            try
            {
                if (!file.EndsWith(".txt"))
                {
                    Console.WriteLine("Text file " + file + " is not .txt file");
                    return false;
                }
                var lines = File.ReadAllLines(file);
                foreach (var lineItem in lines.Select(Line.ReadLine))
                {
                    _data.Add(lineItem);
                }
                Console.WriteLine("Read file " + file + " successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to read file " + file);
                Program.ShowHelp();
                return false;
            }
        }

        private void SortList()
        {
            _data = _data.OrderBy(x => x.Score)
                          .ThenBy(x => x.LastName)
                          .ThenBy(x => x.FirstName)
                          .ToList();
        }

        private bool WriteFile(string outputFile)
        {
            var result = _data.Aggregate("", (current, dataItem) => current + (Line.WriteLine(dataItem) + Environment.NewLine));
            try
            {
                File.WriteAllText(outputFile, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine("Fail to write file " + outputFile);
                return false;
            }
            return true;
        }

        private string GetOuputFileName()
        {
            var index = InputFileName.IndexOf(".txt", StringComparison.CurrentCultureIgnoreCase);
            if (index < 0) return "graded.txt";

            var fileName = InputFileName.Substring(0, index);
            return fileName + "-graded.txt";
        }
    }
}
