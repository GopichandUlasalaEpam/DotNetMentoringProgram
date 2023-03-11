using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1D
{
    public class FileSearchEventArgs
    {
        private string path;
        private bool exclude;
        private bool abort;

        public string Path { get => path; set => path = value; }
        public bool Exclude { get => exclude; set => exclude = value; }
        public bool Abort { get => abort; set => abort = value; }

        public FileSearchEventArgs(string path)
        {
            Path = path;
            Exclude = false;
            Abort = false;
        }
    }
}
