using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1M
{
   
    public class FileSystemArgs : EventArgs
    {
        public string str_Path;
        public bool bool_Abort { get; set; }
        public bool bool_Exclude { get; set; }

        public FileSystemArgs(string path,bool exclude)
        {
            str_Path = path;
            bool_Exclude = exclude;
            this.bool_Abort = false;
        }

        
    }
    public delegate bool Filterfiles(string path);
    public class FileSystemVisitor
    {
        private string str_mainpath;
        private Filterfiles filterfiles;
    
        public event EventHandler event_handler_Onstartsearch;
        public event EventHandler event_handler_Onendsearch;
        public event EventHandler<FileSystemArgs> event_handler_with_arguments_FileFound;
        public event EventHandler<FileSystemArgs> event_handler_with_arguments_DirectoryFound;

        public FileSystemVisitor(string mainpath,Filterfiles filterfiles=null) 
        { 
            str_mainpath = mainpath;
            this.filterfiles = filterfiles;
          

        }

        public IEnumerable<string> TraverseItems()
        {
            OnStarted();

            foreach(var path in TraverseDirectory(str_mainpath)){
                yield return path;
            }

            OnEnded();
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            foreach (var path in Directory.GetFiles(directory))
            {
                FileSystemArgs args = new FileSystemArgs(path,false);
                OnFileFound(args);

                if (!(args.bool_Exclude))
                {
                    yield return path;
                }
                if (args.bool_Abort)
                {
                    yield break;
                }


            }

            foreach (var path in Directory.GetDirectories(directory))
            {
                var args = new FileSystemArgs(path,false);
                OnDirectoryFound(args);

                if (!args.bool_Exclude)
                {
                    foreach (var innerPath in TraverseDirectory(path))
                    {
                        yield return innerPath;
                    }
                    
                }

                Directory.Delete(args.str_Path);


            }
            
        }
        protected virtual void OnFileFound(FileSystemArgs e)
        {
            event_handler_with_arguments_FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemArgs e)
        {
            event_handler_with_arguments_DirectoryFound?.Invoke(this, e);
        }

        protected virtual void OnStarted()
        {
            event_handler_Onstartsearch.Invoke(this,EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            event_handler_Onendsearch?.Invoke(this, EventArgs.Empty);
        }
    }

}

