using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_delegates_events
{
    public delegate void FileSearchEventHandler(object sender, FileSearchEventArgs e);
    public delegate bool DeleteFiles(string path, FileSystemVisitor fileSystemVisitor);

    public class FileSystemVisitor
    {
        public event EventHandler TraversalStarted;
        public event EventHandler TraversalEnded;
        public event EventHandler<FileSearchEventArgs> FileFound;
        public event EventHandler<FileSearchEventArgs> DirectoryFound;
        public event EventHandler<FileSearchEventArgs> DeletionStarted;
        public event EventHandler<FileSearchEventArgs> DeletionEnded;
        public event EventHandler<FileSearchEventArgs> DeletedFile;
        public event EventHandler<FileSearchEventArgs> DeletedFolder;
        public string mainPath;
        public DeleteFiles deleteFiles;

        public FileSystemVisitor(string mainPath, DeleteFiles deleteFiles = null)
        {
            this.mainPath = mainPath;
            this.deleteFiles = deleteFiles;
        }
        public IEnumerable<string> TraverseItems()
        {
            OnStarted();

            foreach (var path in TraverseDirectory(mainPath))
            {

                yield return path;
            }

            OnEnded();
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            foreach (var path in Directory.GetFiles(directory))
            {
                FileSearchEventArgs args = new FileSearchEventArgs(path);
                OnFileFound(args);

                if (!(args.Exclude))
                {
                    yield return path;
                }

            }
            foreach (var path in Directory.GetDirectories(directory))
            {
                var args = new FileSearchEventArgs(path);
                OnDirectoryFound(args);

                if (!args.Exclude)
                {
                    foreach (var innerPath in TraverseDirectory(path))
                    {
                        yield return innerPath;
                    }

                }

            }

        }
        public IEnumerable<string> DeleteItems()
        {
            var arg1 = new FileSearchEventArgs(mainPath);
            OnStartDeleted(arg1);
            foreach (var path in DeleteDirectory(mainPath))
            {
                FileSearchEventArgs args = new FileSearchEventArgs(path);
                OnFileFound(args);

                if (!(args.Exclude))
                {
                    yield return path;
                }

                if (args.Abort)
                {
                    yield break;
                }

            }

            OnEndDelted(arg1);
        }
        private IEnumerable<string> DeleteDirectory(string directory)
        {
            foreach (var path in Directory.GetFiles(directory))
            {
                FileSearchEventArgs args = new FileSearchEventArgs(path);
                OnDeleteFile(args);

                if (args.Abort)
                {
                    yield break;
                }

                if (!(args.Exclude))
                {
                    File.Delete(path);
                    yield return path;
                }

            }

            foreach (var path in Directory.GetDirectories(directory))
            {
                var args = new FileSearchEventArgs(path);
                OnDeleteFolder(args);

                if (!args.Exclude)
                {
                    foreach (var innerPath in DeleteDirectory(path))
                    {
                        yield return innerPath;
                    }

                }

                if (args.Abort)
                {
                    yield break;
                }
                else
                {
                    Directory.Delete(path, true);
                }

            }
        }
        protected virtual void OnDeleteFile(FileSearchEventArgs e)
        {
            DeletedFile?.Invoke(null, e);
        }
        protected virtual void OnDeleteFolder(FileSearchEventArgs e)
        {
            DeletedFolder?.Invoke(null, e);
        }
        protected virtual void OnFileFound(FileSearchEventArgs e)
        {
            FileFound?.Invoke(null, e);
        }

        protected virtual void OnDirectoryFound(FileSearchEventArgs e)
        {
            DirectoryFound?.Invoke(null, e);
        }

        protected virtual void OnStarted()
        {
            TraversalStarted?.Invoke(null, EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            TraversalEnded?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEndDelted(FileSearchEventArgs e)
        {
            DeletionEnded?.Invoke(this, e);
        }

        protected virtual void OnStartDeleted(FileSearchEventArgs e)
        {
            DeletionStarted?.Invoke(this, e);
        }

    }
}
