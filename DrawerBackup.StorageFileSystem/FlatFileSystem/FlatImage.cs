using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem.FlatFileSystem
{
    public class FlatImage : Image
    {
        private DirectoryInfo imageDir;
        private string _Name;

        public FlatImage(string name, string imagePath)
        {
            this._Name = name;
            this.imageDir = new DirectoryInfo(imagePath);
        }
        public override string Name
        {
            get
            {
                return _Name;
            }
        }

        public override void Delete(string name)
        {
            string entryPath = GetEntryPath(name);
            File.Delete(entryPath);
        }
        public override void Dispose( )
        {
            this.imageDir = null;
        }

        public override IEnumerable<string> EnumerateEntries( )
        {
            return imageDir.EnumerateFiles("*.ffs").Select(p => p.Name);
        }

        public override bool Exists(string name)
        {
            return File.Exists(GetEntryPath(name));
        }

        public override Stream Open(string path, FileAccess access, FileMode mode)
        {
            return File.Open(GetEntryPath(path), mode, access);
        }

        public override long Size( )
        {
            return imageDir.EnumerateFiles("*.ffs").Sum(p => p.Length);
        }

        private string GetEntryPath(string name)
        {
            return Path.Combine(this.imageDir.FullName, name + ".ffs");
        }

    }
}
