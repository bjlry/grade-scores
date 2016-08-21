using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace grade_scores
{
    [TestClass]
    public class Test
    {
        const string Path = @"file\";
        const string FileName = "test.txt";
        const string FilePath = Path + FileName;

        [TestMethod]
        public void ProcessTest()
        {
            SetupData(Path, FileName);

            // Run
            var args = new string[1];
            args[0] = FilePath;
            Program.Main(args);

            // Test result
            var result = File.ReadAllText(Path + "Test-graded.txt");
            var expected = "LastNameA,FirstNameA,1" + Environment.NewLine
               + "LastNameB,FirstNameA,1" + Environment.NewLine
               + "LastNameB,FirstNameB,1" + Environment.NewLine
               + "LastNameA,FirstNameA,2" + Environment.NewLine;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ProcessFailTest()
        {
            try
            {
                var args = new string[1];
                args[0] = @"InvalidPath\File.csv";
                Program.Main(args);
                Assert.Fail("Process should have failed");
            }
            catch (Exception ex)
            {
                // Failed correctly
            }
        }

        [TestMethod]
        public void LinesReadPathTest()
        {
            CreatFile(Path, FileName);
            try
            {
                var lines = new Lines();
                lines.ReadPath(FilePath);
                Assert.AreEqual(FileName, lines.InputFileName);
                Assert.AreEqual("file", lines.InputPath);
            }
            catch (Exception ex)
            {
                Assert.Fail("Read path fails");
            }
        }

        [TestMethod]
        public void LineWriteLineTest()
        {
            var data = new Line { FirstName = "A", LastName = "B", Score = 1 };
            Assert.AreEqual("B,A,1", Line.WriteLine(data));
        }

        [TestMethod]
        public void LineReadLineTest()
        {
            var data = Line.ReadLine("B,A,1");
            Assert.AreEqual("B", data.LastName);
            Assert.AreEqual("A", data.FirstName);
            Assert.AreEqual(1, data.Score);
        }

        private static void SetupData(string path, string fileName)
        {
            var filePath = path + fileName;
            CreatFile(path, fileName);

            // Setup data
            var inputContent = "LastNameA,FirstNameA,1" + Environment.NewLine
               + "LastNameB,FirstNameB,1" + Environment.NewLine
               + "LastNameA,FirstNameA,2" + Environment.NewLine
               + "LastNameB,FirstNameA,1" + Environment.NewLine;
            File.WriteAllText(filePath, inputContent);
        }

        private static void CreatFile(string path, string fileName)
        {
            // Create test file
            var filePath = path + fileName;
            Directory.CreateDirectory(path);
            var file = File.Create(filePath);
            file.Close();
        }
    }
}
