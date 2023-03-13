using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_delegates_events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fileCount = 0;
            int folderCount = 0;
            string location = "C:\\Users\\Gopichand_Ulasala\\.NetPractice\\Dummy";
            List<string> files = new List<string>();
            files.Add(location + "p2.exe");
            List<string> dirs = new List<string>();
            dirs.Add(location + "Keypairs");

            string path = "C:\\Users\\Gopichand_Ulasala\\.NetPractice\\Dummy";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path);

            fileSystemVisitor.TraversalStarted += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Traversal Algorithm started");
            };
            fileSystemVisitor.TraversalEnded += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Traversal Algorithm Ended");
            };
            fileSystemVisitor.DirectoryFound += (sender, e) =>
            {
                Console.WriteLine("Directory found at path {0}", e.Path);
            };
            fileSystemVisitor.FileFound += (sender, e) =>
            {
                if (files.Contains(e.Path))
                {
                    Console.WriteLine("Path found");
                    e.Exclude = true;
                }

                Console.WriteLine("File found at path {0}", e.Path);
            };

            foreach (var itempath in fileSystemVisitor.TraverseItems())
            {
                Console.WriteLine("Searched item");
            }

            FileSystemVisitor fileSystemVisitor1 = new FileSystemVisitor(path, DeletionAlgorithm);

            fileSystemVisitor1.DeletionStarted += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Deletion Algortihm started");

            };
            fileSystemVisitor1.FileFound += (sender, e) =>
            {
                if (files.Contains(e.Path))
                {
                    e.Exclude = true;
                }

                if (e.Path.EndsWith(".txt"))
                {
                    e.Exclude = true;
                }

            };
            fileSystemVisitor1.DirectoryFound += (sender, e) =>
            {
                if (dirs.Contains(e.Path))
                {
                    e.Exclude = true;
                }
            };
            fileSystemVisitor1.DeletionEnded += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Deletion Algorithm ended");
            };
            fileSystemVisitor1.DeletedFile += (sender, e) =>
            {
                if (files.Contains(e.Path))
                {
                    e.Exclude = true;
                }

                if (e.Path.EndsWith(".txt"))
                {
                    e.Exclude = true;
                }
                else if (fileCount > 3)
                {
                    e.Abort = true;
                    Console.WriteLine("Process is terminated");
                }
                else
                {
                    fileCount++;
                    Console.WriteLine("File at the path {0} is deleted", e.Path);
                }

            };
            fileSystemVisitor1.DeletedFolder += (sender, e) =>
            {
                if (dirs.Contains(e.Path))
                {
                    e.Exclude = true;
                }
                else if (folderCount > 3)
                {
                    e.Abort = true;
                    Console.WriteLine("Process is terminated");
                }
                else
                {
                    folderCount++;
                    Console.WriteLine("Folder at the path {0} is deleted", e.Path);
                }
            };

            foreach (var itempath in fileSystemVisitor1.DeleteItems())
            {
                Console.WriteLine("----------------");
            }
        }
        private static bool DeletionAlgorithm(string path, FileSystemVisitor fileSystemVisitor)
        {
            return true;
        }
    }
}

