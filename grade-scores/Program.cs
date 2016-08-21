using System;

namespace grade_scores
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    ShowHelp();
                    return 1;
                }

                // Get the command line args
                var processor = new Lines();
                processor.ReadPath(args[0]);

                if (string.IsNullOrEmpty(processor.InputFileName))
                {
                    // all required arguments not provided -> show helper
                    ShowHelp();
                    return 1;
                }

                var start = DateTime.Now;

                // Run
                if (processor.Process(args[0]))
                {
                    Console.WriteLine("File " + args[0] + " processed successfully");
                }
                else
                {
                    Console.WriteLine("Fail to process file " + args[0]);
                }

                var duration = DateTime.Now - start;
                Console.WriteLine();
                Console.WriteLine("Time taken: {0:g}", duration);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();

                return -1;
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("Please enter grade-scores [File]. The file should start in the same directory with the .exe file.");
            Console.WriteLine();
            Console.WriteLine("e.g. grade-scores file\\names.txt");
            Console.WriteLine();
        }
    }
}
