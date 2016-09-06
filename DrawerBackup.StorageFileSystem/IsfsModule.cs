using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem
{
    /// <summary>
    /// Storage File System Module Interface
    /// </summary>
    public interface IsfsModule
    {
        /// <summary>
        /// Open a Repository
        /// </summary>
        /// <param name="repositoryPath"></param>
        /// <returns></returns>
        Repository Open(string repositoryPath);
    }
}
