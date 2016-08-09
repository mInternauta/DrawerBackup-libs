using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem.FlatFileSystem
{
    /// <summary>
    /// Manage the Flat File System Repository
    /// </summary>
    public class FlatRepository : Repository
    {
        public FlatRepository(string repositoryPath) : base(repositoryPath)
        {
        }

        public override void Create(string name)
        {
            string imagePath = GetImagePath(name);
            if (Directory.Exists(imagePath) == false)
                Directory.CreateDirectory(imagePath);
        }
        public override void Delete(string name)
        {
            string imagePath = GetImagePath(name);
            if(Directory.Exists(imagePath))
            {
                Directory.Delete(imagePath, true);
            }
        }

        public override bool Exists(string name)
        {
            return Directory.Exists(GetImagePath(name));
        }

        public override IEnumerable<string> ListImages( )
        {
            return Directory.EnumerateDirectories(RepositoryPath, "FIMG-*")
                .Select(p => GetImageName(p));
        }

        public override Image Open(string name)
        {
            string imagePath = GetImagePath(name);
            if (Directory.Exists(imagePath))
            {
                return new FlatImage(name, imagePath);
            }
            else
            {
                throw new DirectoryNotFoundException("Cant find the image: " + name);
            }
        }

        private string GetImageName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            return dir.Name.Replace("FIMG-", "");
        }

        private string GetImagePath(string name)
        {
            return Path.Combine(RepositoryPath, "FIMG-" + name);
        }

    }
}
