using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task1M
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Gopichand_Ulasala\\.NetPractice\\Dummy";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path, bool_FilterFileType);
            fileSystemVisitor.event_handler_Onstartsearch += (sender, e) => Console.WriteLine("Search Algorithm started");
            fileSystemVisitor.event_handler_Onendsearch += (sender, e) => Console.WriteLine("Algorithm is stoped");
            fileSystemVisitor.event_handler_with_arguments_FileFound += (sender, e) =>
            {
                Console.WriteLine("File found: " + e.str_Path);
                //excludes the .txt files
                if (e.str_Path.EndsWith(".txt"))
                {
                    e.bool_Exclude = true;
                }
               
            };
            fileSystemVisitor.event_handler_with_arguments_DirectoryFound += (sender, e) =>
            {
                Console.WriteLine("Directory found: " + e.str_Path);
                

            };
            foreach (var itemPath in fileSystemVisitor.TraverseItems())
            {
               
                
                Console.WriteLine("Deleted Item path: " + itemPath);
                File.Delete(itemPath);

            }
        }

        private static bool bool_FilterFileType(string path)
        {
            return true;
        }
       
    }
}
