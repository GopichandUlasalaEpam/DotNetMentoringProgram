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
           

            string path = "C:\\Users\\Gopichand_Ulasala\\.NetPractice\\Dummy";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path);
            NotifiyEventHandlers notifiyEventHandlers = new NotifiyEventHandlers(fileSystemVisitor);
            notifiyEventHandlers.NotifyTraversalHandlers();

            foreach (var itempath in fileSystemVisitor.TraverseItems())
            {
                Console.WriteLine("Searched item");
            }

            FileSystemVisitor fileSystemVisitor1 = new FileSystemVisitor(path, DeletionAlgorithm);
            NotifiyEventHandlers notifiyEventHandlers1 = new NotifiyEventHandlers(fileSystemVisitor1);
            notifiyEventHandlers1.NotifyDeleteHandlers();

            foreach (var itempath in fileSystemVisitor1.DeleteItems())
            {
                Console.WriteLine("----------------");
            }
        }
        public static bool DeletionAlgorithm(string path, FileSystemVisitor fileSystemVisitor)
        {
            return true;
        }
    }
}

