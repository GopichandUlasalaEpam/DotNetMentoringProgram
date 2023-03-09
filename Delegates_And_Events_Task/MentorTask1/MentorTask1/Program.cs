using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MentorTask1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //protecteditems list consists of names of files and folders that to be protected from the deletion
            List<string>protecteditems = new List<string>();
            //protecteditems.Add("Gopi.txt");
            //protecteditems.Add("Keypairs");
            //protecteditems.Add("OurGac.exe");
            FileSystem filesystem = new FileSystem(@"C:\Users\Gopichand_Ulasala\.NetPractice\Dummy");
            filesystem.deleteItemsEventHandler += new itemDelete(Notify);
            //if value is set to true,it aborts the process
            filesystem.DeleteFiles(false,protecteditems);
            filesystem.DeleteFolders(false,protecteditems);
           
        }

        public static void Notify(FileSystemInfo files)
        {
          
            Console.WriteLine(" The item with following details are deleted \n item name is {0},created at {1}, and last edit at {2}, item extension  {3}", files.Name, files.CreationTime, files.LastWriteTime,files.Extension);
               
            
        }
    }
}
