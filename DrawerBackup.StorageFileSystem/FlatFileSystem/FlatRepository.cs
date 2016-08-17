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
        private ImageDb imagedb;

        public FlatRepository(string repositoryPath) : base(repositoryPath)
        {
            this.imagedb = new ImageDb(repositoryPath);
        }
        
        public override string Create(string name, string clientName)
        {
            string imagePath = GetImagePath(name, clientName);
            if (Directory.Exists(imagePath) == false)
                Directory.CreateDirectory(imagePath);
            
            FlatImageInfo info = new FlatImageInfo( ) {
                Name = name,
                Relativepath = imagePath.Replace(RepositoryPath, "")
            };

            imagedb.Add(info);

            return info.Id;
        }

        public override void Delete(string id)
        {
            var search = this.imagedb.Where(p => p.Id == id);
            if (search.Any( ))
            {
                string imagePath = GetImagePath(id);
                if (Directory.Exists(imagePath))
                {
                    Directory.Delete(imagePath, true);
                }

                imagedb.Remove(search.First( ));
            }
        }

        public override bool Exists(string id)
        {
            return Directory.Exists(GetImagePath(id));
        }

        public override IEnumerable<string> ListImages( )
        {
            foreach (FlatImageInfo info in imagedb)
            {
                string path = Path.Combine(RepositoryPath, info.Relativepath);
                if(Directory.Exists(path))
                {
                    yield return info.Id;
                }
            }
        }

        public override Image Open(string id)
        {
            string imagePath = GetImagePath(id);
            var search = this.imagedb.Where(p => p.Id == id);

            if (Directory.Exists(imagePath) && search.Any())
            {
                return new FlatImage(search.First(), imagePath);
            }
            else
            {
                throw new DirectoryNotFoundException("Cant find the image: " + id);
            }
        }

        public override long Size( )
        {
            return ListImages( ).Select(p => Open(p))
                .Sum(p => p.Size( ));
        }

        private string GetImageName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            return dir.Name.Replace("FIMG-", "");
        }
        private string GetImagePath(string id)
        {
            var search = this.imagedb.Where(p => p.Id == id);
            string path = "";

            if(search.Any())
            {
                path = Path.Combine(RepositoryPath, search.First( ).Relativepath);
            }

            return path;
        }

        private string GetImagePath(string name, string clientName)
        {
            return Path.Combine(RepositoryPath, clientName + "\\FIMG-" + name);
        }

    }
}
