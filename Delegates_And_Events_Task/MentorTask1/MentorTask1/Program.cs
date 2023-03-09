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
        public delegate void itemDelete(FileSystemInfo item);
        public class FileSystem
        {
            private string path;
            private DirectoryInfo dif;
            public event itemDelete deleteItemsEventHandler;
            public FileSystem(string path) 
            {
                this.path = path;
                dif=new DirectoryInfo(path);
            }

            public void DeleteFolders(bool flag, List<string> list)
            {
                if (flag == true)
                {
                    Console.WriteLine("Deleting folders is aborted");
                }
                else
                {


                    foreach (var folders in dif.EnumerateDirectories())
                    {
                        if (!(list.Contains(folders.Name)))
                        {
                            folders.Delete(true);
                            
                            this.deleteItemsEventHandler(folders);
                        }
                       
                            else
                            {
                                Console.WriteLine("Folder name {0} is not deleted because of its protection given", folders.Name);
                            }
                        


                    }
                }
                
            }
            public void Deletefiles(bool flag,List<string> list)
            {
                if (flag == true)
                {
                    Console.WriteLine("Deleteion of files is aborted");
                }
                else
                {
                    foreach (var files in dif.EnumerateFiles())
                    {

                        if (!(list.Contains(files.Name.ToString())))
                        {
                            files.Delete();

                            this.deleteItemsEventHandler(files);
                        }
                        else
                        {
                            Console.WriteLine("File name {0} is not deleted because of its protection given", files.Name);
                        }



                    }
                }
            }



        }
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
            filesystem.Deletefiles(false,protecteditems);
            filesystem.DeleteFolders(false,protecteditems);
           
        }

        private static void Notify(FileSystemInfo files)
        {
          
            Console.WriteLine(" The item with following details are deleted \n item name is {0},created at {1}, and last edit at {2}, item extension  {3}", files.Name, files.CreationTime, files.LastWriteTime,files.Extension);
               
            
        }
    }
}
