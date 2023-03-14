using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_delegates_events
{

    public class NotifiyEventHandlers
    {
        FileSystemVisitor fileSystemVisitor;
        static int fileCount = 0;
        static int folderCount = 0;
        static string location = "C:\\Users\\Gopichand_Ulasala\\.NetPractice\\Dummy";
        public List<string> files;
        List<string> dirs;
        public NotifiyEventHandlers(FileSystemVisitor fileSystemVisitor)
        {
            this.fileSystemVisitor = fileSystemVisitor;
            files = new List<string>();
            dirs= new List<string>();

        }
       
        public void NotifyTraversalHandlers()
        {
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
                Console.WriteLine("File found at path {0}", e.Path);
            };
        }
        public void NotifyDeleteHandlers()
        {
            files.Add(location + "p2.exe");
            dirs.Add(location + "Keypairs");

            fileSystemVisitor.DeletionStarted += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Deletion Algortihm started");

            };
            fileSystemVisitor.FileFound += (sender, e) =>
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
            fileSystemVisitor.DirectoryFound += (sender, e) =>
            {
                if (dirs.Contains(e.Path))
                {
                    e.Exclude = true;
                }
            };
            fileSystemVisitor.DeletionEnded += (sender, e) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Deletion Algorithm ended");
            };
            fileSystemVisitor.DeletedFile += (sender, e) =>
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
            fileSystemVisitor.DeletedFolder += (sender, e) =>
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
        }
    }
}
