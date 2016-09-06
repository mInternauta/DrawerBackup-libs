using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem.FlatFileSystem
{
    public class FlatFileSystemModule : IsfsModule
    {
        public Repository Open(string repositoryPath)
        {
            return new FlatRepository(repositoryPath);
        }
    }
}
