using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorTask1
{
    public delegate void itemDelete(FileSystemInfo item);

    public class FileSystem
    {
        private string path;
        private DirectoryInfo directoryInfo;
        public event itemDelete deleteItemsEventHandler;
        public FileSystem(string path)
        {
            this.path = path;
            directoryInfo = new DirectoryInfo(path);
        }

        public void DeleteFolders(bool flag, List<string> list)
        {
            if (flag == true)
            {
                Console.WriteLine("Deleting folders is aborted");
            }
            else
            {


                foreach (var folders in directoryInfo.EnumerateDirectories())
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
        public void DeleteFiles(bool flag, List<string> list)
        {
            if (flag == true)
            {
                Console.WriteLine("Deleteion of files is aborted");
            }
            else
            {
                foreach (var files in directoryInfo.EnumerateFiles())
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
}
