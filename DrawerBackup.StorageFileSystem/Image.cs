using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem
{
    /// <summary>
    /// Contract to implement image controllers
    /// </summary>
    public abstract class Image : IDisposable
    {
        /// <summary>
        /// Name of the Image
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Id of the Image
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// Open a stream for the entry
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <returns></returns>
        public abstract Stream Open(string name, FileAccess access, FileMode mode);

        /// <summary>
        /// Enumerate all entries in the image
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> EnumerateEntries( );

        /// <summary>
        /// Check if the entry exists in the image
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract bool Exists(string name);
        
        /// <summary>
        /// Delete a entry from the image
        /// </summary>
        /// <param name="name"></param>
        public abstract void Delete(string name);

        /// <summary>
        /// Get the size of the image
        /// </summary>
        /// <returns></returns>
        public abstract long Size( );

        /// <summary>
        /// Dispose all resources for the current image
        /// </summary>
        public abstract void Dispose( );
    }
}
